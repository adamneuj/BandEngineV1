using BandEngine.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BandEngine.Controllers
{
    public class ArtistTaskController : Controller
    {
        ApplicationDbContext context;
        string[] progress;

        public ArtistTaskController()
        {
            context = new ApplicationDbContext();
            progress = new string[3] { "Not Started", "In Progress", "Completed" };
        }

        // GET: ArtistTask
        [Authorize]
        public ActionResult Index()
        {
            Artist artist = GetCurrentArtist();
            List<ArtistTask> currentTasks = context.ArtistTasks.Where(a => a.ArtistId == artist.ArtistId && a.Progress == "Not Started").ToList();
            return View(currentTasks);
        }

        // GET: ArtistTask/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ArtistTask/Create
        public ActionResult Create()
        {
            ArtistTask artistTask = new ArtistTask();
            ViewBag.Progress = new SelectList(progress);
            return View(artistTask);
        }

        // POST: ArtistTask/Create
        [HttpPost]
        public ActionResult Create(ArtistTask artistTask)
        {
            try
            {
                var progress = new SelectList(new[]
                {
                    new {value = 1, text = "Not Started"},
                    new {value = 2, text = "In Progress"},
                    new {value = 3, text = "Completed"}
                });
                ViewBag.Progress = progress;
                Artist currentArtist = GetCurrentArtist();
                artistTask.ArtistId = currentArtist.ArtistId;
                context.ArtistTasks.Add(artistTask);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ArtistTask/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ArtistTask/Edit/5
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

        // GET: ArtistTask/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ArtistTask/Delete/5
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
