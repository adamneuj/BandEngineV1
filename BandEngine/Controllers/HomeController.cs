using BandEngine.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BandEngine.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext context;

        public HomeController()
        {
            context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var artist = GetCurrentArtist();
            if(artist != null)
            {
                RedirectToAction("Index", "Artist");
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        private Artist GetCurrentArtist()
        {
            var userId = User.Identity.GetUserId();
            Artist currentArtist = context.Artists.FirstOrDefault(a => a.ApplicationId == userId);
            return currentArtist;
        }
    }
}