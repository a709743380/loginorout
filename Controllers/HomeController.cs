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
                    ViewBag.session = HttpContext.Session.GetString("Name");//在view 顯示使用者名稱
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
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
        public IActionResult Rgistered()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Rgistered(account user)
        {
            DBuser Dbuser = new DBuser();
            if (Dbuser.setAccounts(user))
            {
                ViewBag.Err = "";
                Dbuser.setAccounts(user);
                HttpContext.Session.SetString("Name",user.UName);
                ViewBag.session = HttpContext.Session.GetString("Name");
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Err = "賬號重複";
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
