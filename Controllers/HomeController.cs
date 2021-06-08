using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Models;
using System.Web;
using System.Text.RegularExpressions;

namespace WebApplication5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
       // private DBuser Dbuser = new DBuser();
        public IActionResult Index()
        {
            if (HttpContext.Session.Keys.Contains("Name"))
            {
                DBuser Dbuser = new DBuser();
                 List<account> accounts = Dbuser.getAccounts();
                ViewBag.session = HttpContext.Session.GetString("Name");
                ViewBag.accounts = accounts;
            }
            return View();
        }
        [HttpPost]
        public IActionResult Index(account user)
        {
            if(user.UserId!=null || user.Passwd != null)
            { 
                DBuser Dbuser = new DBuser();
                if (Dbuser.Login(user) == "No_UserId")
                {
                    ViewBag.message = "賬號錯誤";
                    return View();
                }
                else if (Dbuser.Login(user) == "No_Passwd")
                {
                    ViewBag.message = "密碼錯誤";
                    return View();
                }
                else
                {
                    ViewBag.message = "";
                    HttpContext.Session.SetString("Name", Dbuser.Login(user));
                    HttpContext.Session.SetString("UserId", user.UserId);
                    ViewBag.session = HttpContext.Session.GetString("Name");//在view 顯示使用者名稱
                    ViewBag.UserId = HttpContext.Session.GetString("UserId");
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public IActionResult LOGOUT()
        {
            if (HttpContext.Session.Keys.Contains("Name"))
            {
                if (TempData["message"] != null)
                {
                    TempData["message"] = null;
                }
                else
                {
                    HttpContext.Session.Clear();
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult Rgistered()
        {
            ViewBag.session = HttpContext.Session.GetString("Name");
            return View();
        }
        [HttpPost]
        public IActionResult Rgistered(account user)
        {
            bool id1 = Regex.IsMatch(user.UserId, "^[a-z0-9A-Z]{6,}$");
            bool id2 = Regex.IsMatch(user.Passwd, "^[a-zA-Z]{1,}[0-9]{4,}$");
            if (id1 && id2)
            {
                DBuser Dbuser = new DBuser();
                if (Dbuser.setAccounts(user))
                {
                    ViewBag.Err = "";
                    Dbuser.setAccounts(user);
                    HttpContext.Session.SetString("Name", user.UName);
                    HttpContext.Session.SetString("Name", user.UserId);
                    ViewBag.session = HttpContext.Session.GetString("Name");
                    ViewBag.UserId = HttpContext.Session.GetString("UserId");
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Err = "賬號重複";
                    return View();
                }
            }
            else
            {
                ViewBag.Err = "賬號或密碼格式錯誤";
                return View();
            }
        }
        public IActionResult modify_passwd()//修改密碼沒有登錄時候返回登錄界面
        {
            if (HttpContext.Session.Keys.Contains("Name"))
            {
                ViewBag.session = HttpContext.Session.GetString("Name");
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult modify_passwd(Modify user)
        {
                ViewBag.session = HttpContext.Session.GetString("UserId");
                user.UserId= HttpContext.Session.GetString("UserId");
                DBuser m_passwd = new DBuser();
                if (user.Oldpasswd != user.Passwd)
                {
                    if (user.NewPasswd == user.Passwd)
                    {
                        if (m_passwd.modify_passwd(user) == "Pass")
                        {
                            TempData["message"] = "修改成功";
                            return RedirectToAction("Logout");
                        }
                        else
                        {

                            TempData["message"] = "舊密碼錯誤";
                            return View();
                        }
                    }
                    else
                    {
                        TempData["message"] = "新密碼輸入不一致";
                        return View();
                    }
                }
                else
                {
                    TempData["message"] = "不可與原密碼相同";
                }
            return View();
        }
        public IActionResult Personal()
        {
            if(HttpContext.Session.Keys.Contains("Name"))
            {
                DB_person myself = new DB_person();
                ViewBag.session = HttpContext.Session.GetString("Name");
                List<Person> information = myself.getinformation();
                ViewBag.information = information;
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult Personal(Person newself)
        {
            
            newself.userid=HttpContext.Session.GetString("UserId");
            ViewBag.session = HttpContext.Session.GetString("Name");
            DB_person myself = new DB_person();
            myself.SetUserid(newself.userid);//確保userid存在
            myself.Setinf(newself);//更新資料

            List<Person> information = myself.getinformation();
            ViewBag.information = information;//顯示資料
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
