using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RAZAM.Models;

namespace RAZAM.Controllers
{
    public class HomeController : Controller
    {
        RazamContext db = new RazamContext();
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
            //var events = db.Events;
            //return View("Events", events.ToList());
            return Redirect("/Home/Events");
        }

        [HttpGet]
        public ActionResult DelEvent(int? id)
        {
            var events = db.Events;
            Event ev = db.Events.Find(id);
            if (ev == null)
            {
                events = db.Events;
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
        public ActionResult AddFile(File fi)
        {
            //Код поддерживающий введение польщователя в БД (должен быть изменён)
            var files = db.Files;
            File fiFind = db.Files.Find(fi.User);
            if (fiFind != null)
            {
                fi.UserId = fiFind.Id;
                db.Files.Add(fi);
                db.SaveChanges();
                files = db.Files;
                return View("Files", files.ToList());
            }
            //files = db.Files;
            //return View("Files", files.ToList());
            return Redirect("/Home/Files");
        }
    }
}