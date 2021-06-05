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
    public class ManagerController: Controller
    {
        DBmanger DBM = new DBmanger();
        public IActionResult Index()
        {
            List<account> Maccounts = DBM.getAccounts();
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
                if (user.Oldpasswd != user.Passwd)
                {
                    if (user.NewPasswd == user.Passwd)
                    {
                        if (DBM.modify_passwd(user) == "Pass")
                        {
                            TempData["message"] = "修改成功";
                            return RedirectToAction("Index");
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
                         return View(); ;
                    }
                }
                else
                {
                    TempData["message"] = "不可與原密碼相同";
                }
            }
            return View(); ;
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index");
        }
        public IActionResult Rgistered()
        {


            return View();
        }
        [HttpPost]
        public IActionResult Rgistered(account Muser)
        {
            if (DBM.setAccounts(Muser))
            {
                ViewBag.Err = "";
                DBM.setAccounts(Muser);
                HttpContext.Session.SetString("Name", Muser.UName);
                ViewBag.Msession = HttpContext.Session.GetString("Name");
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.MErr = "賬號重複";
                return View();
            }
        }
    }
    
}
