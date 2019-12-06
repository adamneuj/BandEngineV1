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
        ApplicationDbContext context;
        public ArtistController()
        {
            context = new ApplicationDbContext();
        }
        // GET: Artist
        public ActionResult Index()
        {
            Artist artist = GetCurrentArtist();
            List<ArtistTask> mailingTasks = GetMailingListArtistTask(artist);
            List<ArtistTask> concertTasks = GetConcertArtistTasks(artist);
            List<ArtistTask> allTasks = new List<ArtistTask>;
            allTasks.AddRange(mailingTasks);
            allTasks.AddRange(concertTasks);
            ArtistIndexViewModel artistInfo = new ArtistIndexViewModel()
            {
                RecommendedTasks = allTasks
            };
            return View(artistInfo);
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
            ArtistCreationViewModel artistInfo = new ArtistCreationViewModel();
            return View(artistInfo);
        }

        // POST: Artist/Create
        [HttpPost]
        public ActionResult Create(ArtistCreationViewModel artistInfo)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                Artist artist = artistInfo.Artist;
                Address address = artistInfo.Address;
                artist.ApplicationId = userId;
                context.Addresses.Add(address);
                context.SaveChanges();
                var addressFromDb = context.Addresses.FirstOrDefault(a => a.AddressLine1 == artistInfo.Address.AddressLine1 && a.AddressLine2 == artistInfo.Address.AddressLine2 && a.City == artistInfo.Address.City && a.State == artistInfo.Address.State && a.ZipCode == artistInfo.Address.ZipCode);
                artist.AddressId = addressFromDb.AddressId;
                context.Artists.Add(artist);
                context.SaveChanges();
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
        private Artist GetCurrentArtist()
        {
            var userId = User.Identity.GetUserId();
            Artist currentArtist = context.Artists.FirstOrDefault(a => a.ApplicationId == userId);
            return currentArtist;
        }
        private List<ArtistTask> GetMailingListArtistTask(Artist artist)
        {
            List<ArtistTask> mailingListTasks = new List<ArtistTask>();
            List<MailingList> mailingList = context.MailingLists.Where(m => m.ArtistId == artist.ArtistId).ToList();
            if(mailingList.Count < 25 || mailingList == null)
            {
                ArtistTask mailingTask1 = new ArtistTask()
                {
                    Description = "Ask friends to sign up for your mailing list."
                };
                mailingListTasks.Add(mailingTask1);
                ArtistTask mailingTask2 = new ArtistTask()
                {
                    Description = "Find a solution to get fan emails before or after a show"
                };
                mailingListTasks.Add(mailingTask2);
                ArtistTask mailingTask3 = new ArtistTask()
                {
                    Description = "Make a list of artists that are similar to you, and find bloggers that will write about you.  Find more information here: http://blog.sonicbids.com/5-strategies-to-get-your-music-featured-on-blogs"
                };
                mailingListTasks.Add(mailingTask3);
            }
            else
            {
                ArtistTask mailingTask4 = new ArtistTask()
                {
                    Description = "Send NewsLetter Out"
                };
                mailingListTasks.Add(mailingTask4);
                ArtistTask mailingTask5 = new ArtistTask()
                {
                    Description = "Send an email to get to know a random fan"
                };
                mailingListTasks.Add(mailingTask5);
            }
            return mailingListTasks;
        }
        private List<ArtistTask> GetConcertArtistTasks(Artist artist)
        {
            List<ArtistTask> concertTasks = new List<ArtistTask>();
            List<Concert> upcomingConcerts = context.Concerts.Where(c => c.ArtistId == artist.ArtistId).ToList();
            foreach(Concert concert in upcomingConcerts)
            {
                ArtistTask upcomingConcertTask1 = new ArtistTask()
                {
                    Description = "Find 3 businesses that you can put a flyer up for the concert at " + concert.VenueName
                };
                ArtistTask upcomingConcertTask2 = new ArtistTask()
                {
                    Description = "Send a show reminder to the people on your mailing list for the concert at " + concert.VenueName
                };
                ArtistTask upcomingConcertTask3 = new ArtistTask()
                {
                    Description = "Find an activity that you can network at to tell people about the concert at " + concert.VenueName
                };
                concertTasks.Add(upcomingConcertTask1);
                concertTasks.Add(upcomingConcertTask2);
                concertTasks.Add(upcomingConcertTask3);
            }
            return concertTasks;
        }
    }
}
