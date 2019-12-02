using BandEngine.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BandEngine.Controllers
{
    public class ConcertController : Controller
    {
        ApplicationDbContext context;

        public ConcertController()
        {
            context = new ApplicationDbContext();
        }

        // GET: Concert
        public ActionResult Index()
        {
            Artist artist = GetCurrentArtist();
            List<Concert> concerts = context.Concerts.Where(c => c.ArtistId == artist.ArtistId).ToList();
            return View(concerts);
        }

        // GET: Concert/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Concert/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Concert/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Concert/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Concert/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Concert/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Concert/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private Artist GetCurrentArtist()
        {
            var userId = User.Identity.GetUserId();
            Artist currentArtist = context.Artists.FirstOrDefault(a => a.ApplicationId == userId);
            return currentArtist;
        }
    }
}
