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
            
            var Mmerber = DBM.getAccounts().Where(Mum => Mum.UserId == Muser.UserId).FirstOrDefault();
            var MPmerber = DBM.getAccounts().Where(Mpm => Mpm.Passwd == Muser.Passwd).FirstOrDefault();

            if (Mmerber != null)
            {
                if (MPmerber != null)
                {
                    HttpContext.Session.SetString("MName", Mmerber.UName.ToString());
                    ViewBag.session = HttpContext.Session.GetString("MName");//在view 顯示使用者名稱
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Mmessage = "密碼錯誤";
                    return View();
                }
            }
            else
            {
                ViewBag.Mmessage = "賬號錯誤";
                return View();
            }
        }
        public IActionResult Privacy()
        {
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
            var Mmerber = DBM.getAccounts().Where(Mum => Mum.UserId == Muser.UserId).FirstOrDefault();
            if (Mmerber == null)
            {
                ViewBag.MErr = "";
                DBM.setAccounts(Muser);
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
