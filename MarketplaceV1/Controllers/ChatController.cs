using MarketplaceV1.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MarketplaceV1.Controllers
{
    public class ChatController : Controller
    {
        public ActionResult Index()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "");
            }

            var currentUser = HttpContext.User;

            using (var db = new ApplicationDbContext())
            {
                string id = currentUser.Identity.GetUserId();
                ViewBag.allUsers = db.Users.Where(u => u.Id != id  && u.IsBlogger).ToList();
                ViewBag.sentMessagesFromId = db.Chats.Where(c=> !c.Seen && c.ReciverId.Equals(id)).Select(c=>c.SenderId).ToList();
                ViewBag.currentUser = db.Users.Where(u=>u.Id == id).FirstOrDefault();
            }

            return View();
        }

        [HttpPost]
        public JsonResult SendMessage()
        {
            if (HttpContext.User == null)
            {
                return Json(new { status = "error", message = "User is not logged in" });
            }

            var currentUser = HttpContext.User;
            string userId = currentUser.Identity.GetUserId();
            using (var db = new ApplicationDbContext())
            {
                Chat chat = new Chat
                {
                    SenderId = userId,
                    Message = Request.Form["message"],
                    DateTime = DateTime.Now,
                    ReciverId = Request.Form["contact"],
                    Seen = false
                };
                db.Chats.Add(chat);
                db.SaveChanges();

                return Json(chat);
            }
        }

        public JsonResult ChatWithContact(string contact)
        {
            if (HttpContext.User == null)
            {
                return Json(new { status = "error", message = "User is not logged in" });
            }
            var currentUser = HttpContext.User;
            string userId = currentUser.Identity.GetUserId();

            var chats = new List<Chat>();

            using (var db = new ApplicationDbContext())
            {   
                chats = db.Chats.Select(c=>c).Where(c =>
                (c.ReciverId.Equals(userId) && c.SenderId.Equals(contact)) ||
                (c.ReciverId.Equals(contact) && c.SenderId.Equals(userId))
                ).OrderBy(c=> c.DateTime).ToList();
                chats.Where(c=> !c.SenderId.Equals(userId)).All(c => c.Seen = true);
                db.SaveChanges();
            }

            return Json(
                new { status = "success", data = chats, me = userId},
                JsonRequestBehavior.AllowGet
            );
        }

    }
}