using MarketplaceV1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarketplaceV1.Controllers
{
    public class IGUserController : Controller
    {
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
            }
            catch
            {
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