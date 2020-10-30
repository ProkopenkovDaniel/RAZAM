using System;
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
            /*Getting the Id if current user*/
            RazamUserManager userManager = HttpContext.GetOwinContext()
                                            .GetUserManager<RazamUserManager>();
            RazamUser user = userManager.FindByName(User.Identity.Name);
            ViewBag.UserId = user.Id;
            /*Get the List of Notes from DataBase, there has to be condition*/
            var notes = db.Notes;
            /*Sort notes by Datatime*/
            return View(notes.ToList());
        }

        public ActionResult AddNote(Note note)
        {
            RazamUserManager userManager = HttpContext.GetOwinContext()
                                            .GetUserManager<RazamUserManager>();
            RazamUser user = userManager.FindByName(User.Identity.Name);
            RazamUser us = db.Users.Find(user.Id);
            RazamUser receiver = db.Users.Find("865f4ca1-dcbd-4a2d-a2af-f3ab771207f9");
            if (us == null || receiver == null)
            {
                return Redirect("/Home/Files");
            }
            note.Receiver = receiver;
            note.Sender = us;
            note.Date = DateTime.Now;
            note.Status = State.unread;
            db.Notes.Add(note);
            db.SaveChanges();
            return Redirect("/Home/Notes");

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
            Guid guid = Guid.NewGuid();
            string path = System.IO.Path.Combine(Server.MapPath("~/Files"),
                System.IO.Path.GetFileName(guid.ToString()) );
            file.SaveAs(path);
            File fi = new File();
            fi.Name = System.IO.Path.GetFileName(file.FileName);
            fi.Path = path;
            fi.ContentType = file.ContentType;
            fi.GuidName = guid.ToString();
            fi.User = us;
            fi.UserId = us.Id;
            fi.Date = DateTime.Now;
            db.Files.Add(fi);
            db.SaveChanges();

            //var files = db.Files;
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
                + fi.GuidName));
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
            string filePath = Server.MapPath("~/Files/" + fi.GuidName);
            return File(filePath, fi.ContentType, fi.Name);
        }
    }
}