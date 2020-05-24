using MarketplaceV1.Models;
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

                var user = UserManager.Users.FirstOrDefault(n => n.Nickname == name);
                if (user != null)
                {
                    ViewBag.Email = user.Email;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.StackTrace;
                return View("NoUser");
            }
            

            return View();
        }
        public ActionResult NoUser()
        {
            return View();
        }
    }
}