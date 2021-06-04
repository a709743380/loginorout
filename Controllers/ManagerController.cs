﻿using Microsoft.AspNetCore.Mvc;
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
                ViewBag.Msession = HttpContext.Session.GetString("MName");//在view 顯示使用者名稱
                return RedirectToAction("Index");
            }

        }
        public IActionResult Privacy()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Privacy(Modify modify)
        {
            if (HttpContext.Session.Keys.Contains("MName"))
            {
                M_Passwd m_passwd = new M_Passwd();
                try
                {
                    m_passwd.modify_passwd(modify);
                }
                catch (Exception e)
                {
                    ViewBag.Message = e.Message;
                }
            }
            return View();
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
