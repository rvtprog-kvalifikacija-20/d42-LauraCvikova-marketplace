using MarketplaceV1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarketplaceV1.Controllers
{


    public class IGUserController : Controller
    {
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public IGUserController()
        {
        }
        public IGUserController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }
        // GET: IGUser
        public ActionResult Index()
        {
            string name = "";
            IGUser iGUser;
            try
            {
                name = Request.Url.Query.Replace("?user=", string.Empty);
                iGUser = new IGUser(name);

                
                ViewBag.Image = iGUser.GetImage();
                ViewBag.Name = iGUser.GetName();
                ViewBag.Sub = iGUser.GetSubscribers();
                ViewBag.Bio = iGUser.GetBio();
                ViewBag.Images = iGUser.GetImages();
                ViewBag.IsBlogger = false;
                ViewBag.MyCalendar = false;
                var user = UserManager.Users.FirstOrDefault(n => n.Nickname == name);
                if (user != null)
                {
                    var id = HttpContext.User.Identity.GetUserId();

                    ViewBag.Email = user.Email; 
                    using (var db = new ApplicationDbContext())
                    {
                      var bookings = db.Bookings.Where(b => b.BloggerNickname.Equals(name)).Select(b=>b.Date).ToList();
                        ViewBag.Bookings = bookings;
                       
                        if (db.Users.Where(u => u.Nickname == name && u.IsBlogger).Any()) {
                            ViewBag.IsBlogger = true; 
                        }
                            
                        if (db.Users.Where(u => u.Nickname == name && u.Id == id).Any())
                        {
                           
                            ViewBag.MyCalendar = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.StackTrace;
                return View("NoUser");
            }

            return View();
        }
        public JsonResult Book(string date)
        {
            if(date == null) {
                return Json(
                           new { status = "error" },
                       JsonRequestBehavior.AllowGet
                       );
            }
            using (var db = new ApplicationDbContext()) {
                var currentUserId = HttpContext.User.Identity.GetUserId();
                var nick = db.Users.Where(u => u.Id.Equals(currentUserId)).FirstOrDefault().Nickname;
                var newDate = Convert.ToDateTime(date);
                var booking = db.Bookings.Where(b => b.Date.Equals(newDate) && b.BloggerNickname.Equals(nick));
                if (!booking.Any())
                {
                    db.Bookings.Add(new Booking { Date = Convert.ToDateTime(date), BloggerNickname = nick });
                    db.SaveChanges();
                    return Json(
                        new { status = "added" },
                    JsonRequestBehavior.AllowGet
                    );
                }
                else
                {
                    db.Bookings.Remove(booking.FirstOrDefault());
                    db.SaveChanges();
                    return Json(
                       new { status = "removed" },
                   JsonRequestBehavior.AllowGet
                   );
                }
                
            }
        }
        public JsonResult GetBookings(string nickname)
        {
            using (var db = new ApplicationDbContext())
            {
                var bookings = db.Bookings.Where(b => b.Date.Date.Equals(b.BloggerNickname.Equals(nickname))).FirstOrDefault();

                return Json(
                    new { status = "success", data = bookings },
                JsonRequestBehavior.AllowGet
                );
            }
        }

        public ActionResult NoUser()
        {
            return View();
        }
    }
}