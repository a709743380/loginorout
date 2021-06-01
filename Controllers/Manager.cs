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
    public class Manager : Controller
    {
        DBmanger DBM = new DBmanger();
        public IActionResult Index()
        {
            List<account> Maccounts = DBM.getAccounts();
            if (HttpContext.Session.Keys.Contains("MName"))
            {
                ViewBag.Msession = HttpContext.Session.GetString("MName");
                ViewBag.Maccounts = Maccounts;
            }
            return View();
        }
    [HttpPost]
        public IActionResult Index(account Muser)
        {
            var merber = DBM.getAccounts().Where(m => m.UserId == Muser.UserId && m.Passwd == Muser.Passwd).FirstOrDefault();
                
            if (merber != null)
            {
                HttpContext.Session.SetString("MName", merber.UName.ToString());
                ViewBag.session = HttpContext.Session.GetString("MName");//在view 顯示使用者名稱
                return RedirectToAction("Index","Manager");
            }
            else
            {
                ViewBag.Mmanager = "賬號錯誤 重新登陸";
            }
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Logout()
        {
            return View();
        }
        public IActionResult Rgistered()
        {
            return View();
        }
    }
    
}
