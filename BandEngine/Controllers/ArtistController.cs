using BandEngine.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BandEngine.Controllers
{
    public class ArtistController : Controller
    {
        ApplicationDbContext db;
        public ArtistController()
        {
            db = new ApplicationDbContext();
        }
        // GET: Artist
        public ActionResult Index()
        {
            return View();
        }

        // GET: Artist/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Artist/Create
        [Authorize]
        public ActionResult Create()
        {
            ArtistCreation artistInfo = new ArtistCreation();
            return View(artistInfo);
        }

        // POST: Artist/Create
        [HttpPost]
        public ActionResult Create(ArtistCreation artistInfo)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                Artist artist = artistInfo.Artist;
                Address address = artistInfo.Address;
                artist.ApplicationId = userId;
                db.Addresses.Add(address);
                db.SaveChanges();
                var addressFromDb = db.Addresses.FirstOrDefault(a => a.AddressLine1 == artistInfo.Address.AddressLine1 && a.AddressLine2 == artistInfo.Address.AddressLine2 && a.City == artistInfo.Address.City && a.State == artistInfo.Address.State && a.ZipCode == artistInfo.Address.ZipCode);
                artist.AddressId = addressFromDb.AddressId;
                db.Artists.Add(artist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Artist/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Artist/Edit/5
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

        // GET: Artist/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Artist/Delete/5
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
    }
}
