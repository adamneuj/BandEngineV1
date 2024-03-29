﻿using BandEngine.Models;
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
            ArtistTaskViewModel artistTaskInfo = new ArtistTaskViewModel()
            {
                NotStarted = context.ArtistTasks.Where(a => a.ArtistId == artist.ArtistId && a.Progress == "Not Started").ToList(),
                InProgress = context.ArtistTasks.Where(a => a.ArtistId == artist.ArtistId && a.Progress == "In Progress").ToList(),
                Completed = context.ArtistTasks.Where(a => a.ArtistId == artist.ArtistId && a.Progress == "Completed").ToList()
            };
            //List<ArtistTask> currentTasks = context.ArtistTasks.Where(a => a.ArtistId == artist.ArtistId && a.Progress == "Not Started").ToList();
            return View(artistTaskInfo);
        }

        [Authorize]
        public ActionResult InProgress()
        {
            Artist artist = GetCurrentArtist();
            List<ArtistTask> inProgressTasks = context.ArtistTasks.Where(a => a.ArtistId == artist.ArtistId && a.Progress == "In Progress").ToList();
            return View(inProgressTasks);
        }

        [Authorize]
        public ActionResult Completed()
        {
            Artist artist = GetCurrentArtist();
            List<ArtistTask> completedTasks = context.ArtistTasks.Where(a => a.ArtistId == artist.ArtistId && a.Progress == "Completed").ToList();
            return View(completedTasks);
        }

        [Authorize]
        public ActionResult Move(int id)
        {
            ArtistTask task = context.ArtistTasks.FirstOrDefault(a => a.TaskId == id);
            if(task.Progress == "Not Started")
            {
                task.Progress = "In Progress";
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else if(task.Progress == "In Progress")
            {
                task.Progress = "Completed";
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: ArtistTask/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ArtistTask/Create
        [Authorize]
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
            ArtistTask artistTask = context.ArtistTasks.FirstOrDefault(a => a.TaskId == id);
            ViewBag.Progress = new SelectList(progress);
            return View(artistTask);
        }

        // POST: ArtistTask/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ArtistTask artistTask)
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

                var taskFromDb = context.ArtistTasks.FirstOrDefault(a => a.TaskId == id);
                taskFromDb.Description = artistTask.Description;
                taskFromDb.Progress = artistTask.Progress;
                context.SaveChanges();
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
            ArtistTask artistTask = context.ArtistTasks.FirstOrDefault(a => a.TaskId == id);
            return View(artistTask);
        }

        // POST: ArtistTask/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, ArtistTask artistTask)
        {
            try
            {
                ArtistTask artistTaskFromDb = context.ArtistTasks.FirstOrDefault(a => a.TaskId == id);
                context.ArtistTasks.Remove(artistTaskFromDb);
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
