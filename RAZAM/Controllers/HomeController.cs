using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RAZAM.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Ajax.Utilities;
using System.Runtime.Remoting.Channels;
using System.Net;
using System.Net.Http;
using System.Deployment.Internal;

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
            var userNotes = from note in db.Notes
                            join sender in db.Users on note.SenderId equals sender.Id
                            join receiver in db.Users on note.ReceiverId equals receiver.Id
                            select new UserNote
                            {
                                id = note.id,
                                Name = note.Name,
                                Description = note.Description,
                                Date = note.Date,
                                Status = note.Status,
                                SenderId = sender.Id,
                                SenderName = sender.UserName,
                                ReceiverId = receiver.Id,
                                ReceiverName = receiver.UserName
                            };
            /*Sort notes by Datatime*/
            List<UserNote> noteList = userNotes.ToList();
            noteList.Sort(delegate (UserNote x, UserNote y)
            {
                if (x.Date == null && y.Date == null) return 0;
                else if (x.Date == null) return -1;
                else if (y.Date == null) return 1;
                else return x.Date.CompareTo(y.Date);
            });
            noteList.Reverse();
            SelectList users = new SelectList(db.Users.Where(u=>u.Id != user.Id), "Id", "UserName");
            
            ViewBag.Users = users;

            return View(noteList);
        }

        public ActionResult AddNote(Note note)
        {
            RazamUserManager userManager = HttpContext.GetOwinContext()
                                            .GetUserManager<RazamUserManager>();
            RazamUser user = userManager.FindByName(User.Identity.Name);
            RazamUser us = db.Users.Find(user.Id);
            if (us == null || note.ReceiverId == null)
            {
                return Redirect("/Home/Files");
            }
            note.SenderId = us.Id;
            note.Date = DateTime.Now;
            note.Status = State.unread;
            db.Notes.Add(note);
            db.SaveChanges();
            return Redirect("/Home/Notes");
        }

        public ActionResult ChangeNoteStatus(int noteId, State status)
        {
            var notes = db.Notes;
            Note no = db.Notes.Find(noteId);
            try
            {
                if (no != null)
                {
                    no.Status = status;
                    db.SaveChanges();
                }
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotModified);
            }
        }

        public ActionResult DeleteNote(int Id)
        {
            var notes = db.Notes;
            Note no = db.Notes.Find(Id);
            if (no == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotModified);
            }
            db.Notes.Remove(no);
            db.SaveChanges();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult Events()
        {
            var events = db.Events;
            List<Event> evList = events.ToList();
            evList.Sort(delegate (Event x, Event y)
            {
                if (x.DateTime == null && y.DateTime == null) return 0;
                else if (x.DateTime == null) return -1;
                else if (y.DateTime == null) return 1;
                else return x.DateTime.CompareTo(y.DateTime);
            });
            List<Event> eventList = new List<Event>();
            evList.ForEach(delegate (Event ev)
            {
                if(ev.DateTime >= DateTime.Now)
                {
                    eventList.Add(ev);
                }
            });
            return View(eventList);
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
            return Redirect("/Home/Events");
        }

        public ActionResult Files()
        {
            ViewBag.Users = db.Users.ToList();
            var files = db.Files;
            List<File> fiList = files.ToList();
            fiList.Sort(delegate (File x, File y)
            {
                if (x.Date == null && y.Date == null) return 0;
                else if (x.Date == null) return -1;
                else if (y.Date == null) return 1;
                else return x.Date.CompareTo(y.Date);
            });
            fiList.Reverse();
            return View(fiList);
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