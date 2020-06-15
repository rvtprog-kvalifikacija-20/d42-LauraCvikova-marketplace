using MarketplaceV1.Models;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MarketplaceV1.Controllers
{
    public class HomeController : Controller
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
        public ActionResult Index()
        {
            //get all registred bloggers
            List<ApplicationUser> users = UserManager.Users.Where(u => u.IsBlogger == true).OrderByDescending(u => u.DataTime).ToList();
            ViewBag.Users = users;
          
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        public ActionResult TestPage()
        {
            
            return View();
        }
    }
}