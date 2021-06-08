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
    public class ManagerController: Controller
    {
        DBmanger DBM = new DBmanger();
        public IActionResult Index()
        {
            List<account> Maccounts = DBM.GetAccounts();
            if (HttpContext.Session.Keys.Contains("MName"))
            {
                ViewBag.Msession = HttpContext.Session.GetString("MName");
                ViewBag.MUserId = HttpContext.Session.GetString("MUserId");
                ViewBag.Maccounts = Maccounts;
            }
            return View();
        }
    [HttpPost]
        public IActionResult Index(account Muser)
        {
            if (DBM.Login(Muser) == "No_UserId")
            {
                ViewBag.Mmessage = "賬號錯誤";
                return View();
            }
            else if(DBM.Login(Muser) == "No_Passwd")
            {
                ViewBag.Mmessage = "密碼錯誤";
                return View();
            }
            else
            {
                ViewBag.Mmessage ="";
                HttpContext.Session.SetString("MName", DBM.Login(Muser));
                HttpContext.Session.SetString("MUserId", Muser.UserId);
                ViewBag.Msession = HttpContext.Session.GetString("MName");//在view 顯示使用者名稱
                ViewBag.MUserId = HttpContext.Session.GetString("MUserId");
                return RedirectToAction("Index");
            }

        }
        public IActionResult Privacy()
        {
            if (HttpContext.Session.Keys.Contains("MUserId"))
            {
                ViewBag.MUserId = HttpContext.Session.GetString("MUserId");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Privacy(Modify user)
        {
            if (HttpContext.Session.Keys.Contains("MUserId"))
            {
                ViewBag.MUserId = HttpContext.Session.GetString("MUserId");
                user.UserId = HttpContext.Session.GetString("MUserId");
                bool id2 = Regex.IsMatch(user.Passwd, "^[a-zA-Z]{1,}[0-9]{4,}$");

                if (user.Oldpasswd != user.Passwd)
                {
                    if (id2)
                    {
                        if (user.NewPasswd == user.Passwd)
                        {
                            if (DBM.Modify_passwd(user) == "Pass")
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
                        TempData["message"] = "密碼格式錯誤";
                    }
                }
                else
                {
                    TempData["message"] = "不可與原密碼相同";
                    return View();
                }
            }
            return View();
        }
        public IActionResult Logout()
        {
            if(TempData["message"] != null) { 
                TempData["message"] = null;
            }
            else
            {
                HttpContext.Session.Clear();
                
            }
            return RedirectToAction("Index");
        }
        public IActionResult Rgistered()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Rgistered(account Muser)
        {
            bool id1 = Regex.IsMatch(Muser.UserId, "^[a-z0-9A-Z]{6,}$");
            bool id2 = Regex.IsMatch(Muser.Passwd, "^[a-zA-Z]{1,}[0-9]{4,}$");
            if(id2 &&id1)
            {
                if (DBM.SetAccounts(Muser))
                {
                    ViewBag.Err = "";
                    DBM.SetAccounts(Muser);
                    HttpContext.Session.SetString("Name", Muser.UName);
                    ViewBag.Msession = HttpContext.Session.GetString("Name");
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.MErr = "賬號重複";
                    
                }
            }
            else
            {
                ViewBag.MErr = "賬號或密碼格式錯誤";
            }
            return View();
        }
    }
    
}
