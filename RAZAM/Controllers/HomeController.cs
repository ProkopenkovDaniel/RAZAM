﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RAZAM.Models;
using Microsoft.AspNet.Identity.Owin;

namespace RAZAM.Controllers
{
    public class HomeController : Controller
    {
        RazamContext db = new RazamContext();
        RazamUser user;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Notes()
        {
            return View();
        }

        public ActionResult Events()
        {
            var events = db.Events;
            return View(events.ToList());
        }

        [HttpPost]
        public ActionResult AddEvent(Event ev)
        {
            db.Events.Add(ev);
            db.SaveChanges();
            return Redirect("/Home/Events");
        }

        [HttpGet]
        public ActionResult DelEvent(int? id)
        {
            var events = db.Events;
            Event ev = db.Events.Find(id);
            if (ev == null)
            {
                return View("Events", events.ToList());
            }
            db.Events.Remove(ev);
            db.SaveChanges();
            events = db.Events;
            return View("Events", events.ToList());
        }

        public ActionResult Files()
        {
            ViewBag.Users = db.Users.ToList();
            var files = db.Files;
            return View(files.ToList());
        }

        [HttpPost]
        public ActionResult AddFile(HttpPostedFileBase file)
        {
            RazamUserManager userManager = HttpContext.GetOwinContext()
                                            .GetUserManager<RazamUserManager>();
            RazamUser user = userManager.FindByName(User.Identity.Name);
            RazamUser us = db.Users.Find(user.Id);
            if (user == null || file == null || file.ContentLength==0)
            {
                return Redirect("/Home/Files");
            }
            string path = System.IO.Path.Combine(Server.MapPath("~/Files"),
                System.IO.Path.GetFileName(file.FileName) );
            file.SaveAs(path);
            File fi = new File();
            fi.Name = System.IO.Path.GetFileName(file.FileName);
            fi.Path = path;
            fi.ContentType = file.ContentType;
            fi.User = us;
            fi.UserId = us.Id;
            fi.Date = DateTime.Now;
            db.Files.Add(fi);
            db.SaveChanges();

            var files = db.Files;
            return Redirect("/Home/Files");
        }

        [HttpGet]
        public ActionResult DelFile(int? id)
        {
            File fi = db.Files.Find(id);
            if (fi == null)
            {
                return Redirect("/Home/Files");
            }
            System.IO.File.Delete(Server.MapPath("~/Files/"
                + fi.Name));
            db.Files.Remove(fi);
            db.SaveChanges();
            return Redirect("/Home/Files");
        }

        [HttpGet]
        public FileResult DownloadFile(int? id)
        {
            File fi = db.Files.Find(id);
            if (fi == null)
            {
                Redirect("/Home/Files");
            }
            string filePath = Server.MapPath("~/Files/" + fi.Name);
            return File(filePath, fi.ContentType, fi.Name);
        }
    }
}