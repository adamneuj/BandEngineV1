using BandEngine.Models;
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
                // TODO: Add insert logic here

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
