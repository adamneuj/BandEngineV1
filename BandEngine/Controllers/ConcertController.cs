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
        [Authorize]
        public ActionResult Index()
        {
            Artist artist = GetCurrentArtist();
            List<Concert> concerts = context.Concerts.Where(c => c.ArtistId == artist.ArtistId).ToList();
            List<ConcertViewModel> allConcerts = new List<ConcertViewModel>();
            foreach(Concert concert in concerts)
            {
                ConcertViewModel concertInfo = new ConcertViewModel();
                concertInfo.Concert = concert;
                concertInfo.Address = context.Addresses.FirstOrDefault(a => a.AddressId == concert.AddressId);
                concertInfo.FullAddress = ConcatAddress(concertInfo.Address);
                allConcerts.Add(concertInfo);
            }
            return View(allConcerts);
        }

        // GET: Concert/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Concert/Create
        [Authorize]
        public ActionResult Create()
        {
            ConcertViewModel concert = new ConcertViewModel();
            return View(concert);
        }

        // POST: Concert/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {


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
