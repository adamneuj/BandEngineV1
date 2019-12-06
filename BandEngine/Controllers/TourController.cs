﻿using BandEngine.Models;
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
        public ActionResult Index()
        {
            Artist artist = GetCurrentArtist();
            List<Tour> allTours = context.Tours.Where(t => t.ArtistId == artist.ArtistId).ToList();
            return View(allTours);
        }

        // GET: Tour/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Tour/Create
        public ActionResult Create()
        {
            Tour tour = new Tour();
            return View(tour);
        }

        // POST: Tour/Create
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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Tour/Delete/5
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
