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
        DBuser Dbuser = new DBuser();
        public IActionResult Index()
        {
            List<account> accounts = Dbuser.getAccounts();
            if (HttpContext.Session.Keys.Contains("Name"))
            {
                ViewBag.session = HttpContext.Session.GetString("Name");
                ViewBag.accounts = accounts;
            }
            return View();


        }
        [HttpPost]
        public IActionResult Index(account user)
        {
           var merber = Dbuser.getAccounts().Where(m => m.UserId == user.UserId && m.Passwd == user.Passwd).FirstOrDefault();

            if (merber!= null)
            {
                HttpContext.Session.SetString("Name", merber.UName.ToString());
                ViewBag.session = HttpContext.Session.GetString("Name");//在view 顯示使用者名稱
                
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.message = "賬號錯誤 重新登陸";
            }
            return View();

        }

        public IActionResult LOGOUT()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
