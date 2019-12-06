using BandEngine.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BandEngine.Controllers
{
    public class TourController : Controller
    {
        ApplicationDbContext context;

        public TourController()
        {
            context = new ApplicationDbContext();
        }
        // GET: Tour
        [Authorize]
        public ActionResult Index()
        {
            Artist artist = GetCurrentArtist();
            List<Tour> allTours = context.Tours.Where(t => t.ArtistId == artist.ArtistId).ToList();
            return View(allTours);
        }

        [Authorize]
        public ActionResult Stops(int id)
        {
            Artist artist = GetCurrentArtist();
            TourViewModel tourInfo = new TourViewModel()
            {
                AllConcerts = context.Concerts.Where(c => c.ArtistId == artist.ArtistId && c.ConcertDate > DateTime.Today).ToList(),
                AllStops = context.Concerts.Where(c => c.ArtistId == artist.ArtistId && c.TourId == id).ToList(),
                CurrentTour = context.Tours.FirstOrDefault(t => t.TourId == id)
            };
            return View(tourInfo);
        }

        // GET: Tour/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Tour/Create
        [Authorize]
        public ActionResult Create()
        {
            Tour tour = new Tour();
            return View(tour);
        }

        // POST: Tour/Create
        [HttpPost]
        public ActionResult Create(Tour tour)
        {
            try
            {
                Artist artist = GetCurrentArtist();
                tour.ArtistId = artist.ArtistId;
                context.Tours.Add(tour);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Tour/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Tour/Edit/5
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

        // GET: Tour/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            Tour tour = context.Tours.FirstOrDefault(t => t.TourId == id);
            return View(tour);
        }

        // POST: Tour/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Tour tour)
        {
            try
            {
                Tour tourFromDb = context.Tours.FirstOrDefault(t => t.TourId == id);
                context.Tours.Remove(tourFromDb);
                context.SaveChanges();
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
