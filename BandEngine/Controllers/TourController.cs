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
                AllConcerts = context.Concerts.Where(c => c.ArtistId == artist.ArtistId && c.ConcertDate > DateTime.Today && c.TourId == null).ToList(),
                AllStops = context.Concerts.Where(c => c.ArtistId == artist.ArtistId && c.TourId == id).ToList(),
                CurrentTour = context.Tours.FirstOrDefault(t => t.TourId == id)
            };
            return View(tourInfo);
        }

        [Authorize]
        public ActionResult AddStop(int id, int tourId)
        {
            Concert concert = context.Concerts.FirstOrDefault(c => c.ConcertId == id);
            concert.TourId = tourId;
            context.SaveChanges();
            return RedirectToAction("Stops", new { id = tourId });
        }

        [Authorize]
        public ActionResult RemoveStop(int id, int tourId)
        {
            Concert concert = context.Concerts.FirstOrDefault(c => c.ConcertId == id);
            concert.TourId = null;
            context.SaveChanges();
            return RedirectToAction("Stops", new { id = tourId });
        }

        [Authorize]
        public ActionResult Route(int id)
        {
            TourViewModel tourInfo = new TourViewModel();
            tourInfo.AllStops = context.Concerts.Where(c => c.TourId == id).ToList();
            tourInfo.AllStopInfo = new List<ConcertViewModel>();
            foreach(Concert concert in tourInfo.AllStops)
            {
                ConcertViewModel concertInfo = new ConcertViewModel();
                concertInfo.Concert = concert;
                concertInfo.Address = context.Addresses.FirstOrDefault(a => a.AddressId == concert.AddressId);
                concertInfo.FullAddress = ConcatAddress(concertInfo.Address);
                tourInfo.AllStopInfo.Add(concertInfo);
            }
            ConcertViewModel[] infoArray = tourInfo.AllStopInfo.ToArray();
            ViewBag.StopInfo = infoArray;
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

        private string ConcatAddress(Address address)
        {
            string zipCode = address.ZipCode.ToString();
            if (address.AddressLine2 != null)
            {
                string fullAddress = address.AddressLine1 + " " + address.AddressLine2 + ", " + address.City + ", " + address.State + " " + zipCode;
                return fullAddress;
            }
            else
            {
                string fullAddress = address.AddressLine1 + ", " + address.City + ", " + address.State + " " + zipCode;
                return fullAddress;
            }
        }
    }
}
