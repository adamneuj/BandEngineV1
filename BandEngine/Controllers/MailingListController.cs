using BandEngine.Models;
using MailKit;
using Microsoft.AspNet.Identity;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BandEngine.Controllers
{
    public class MailingListController : Controller
    {
        ApplicationDbContext context;

        public MailingListController()
        {
            context = new ApplicationDbContext();
        }

        // GET: MailingList
        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var currentArtist = context.Artists.FirstOrDefault(a => a.ApplicationId == userId);
            List<MailingList> mailingListFromDb = context.MailingLists.Where(m => m.ArtistId == currentArtist.ArtistId).ToList();
            List<Email> mailingList = new List<Email>();
            foreach (MailingList emailFromDb in mailingListFromDb)
            {
                Email email = context.Emails.FirstOrDefault(e => e.EmailId == emailFromDb.EmailId);
                mailingList.Add(email);
            }
            return View(mailingList);
        }

        // GET: MailingList/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MailingList/Create
        [Authorize]
        public ActionResult Create()
        {
            Email email = new Email();
            return View(email);
        }

        // POST: MailingList/Create
        [HttpPost]
        public ActionResult Create(Email email)
        {
            try
            {
                context.Emails.Add(email);
                context.SaveChanges();
                var userId = User.Identity.GetUserId();
                var currentArtist = context.Artists.FirstOrDefault(a => a.ApplicationId == userId);
                var emailFromDb = context.Emails.FirstOrDefault(e => e.EmailAddress == email.EmailAddress);
                MailingList mailingListEntry = new MailingList()
                {
                    EmailId = emailFromDb.EmailId,
                    ArtistId = currentArtist.ArtistId
                };
                context.MailingLists.Add(mailingListEntry);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MailingList/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MailingList/Edit/5
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

        // GET: MailingList/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            Email email = context.Emails.FirstOrDefault(e => e.EmailId == id);
            return View(email);
        }

        // POST: MailingList/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Email email)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                var currentArtist = context.Artists.FirstOrDefault(a => a.ApplicationId == userId);
                Email emailFromDb = context.Emails.FirstOrDefault(e => e.EmailId == id);
                MailingList mailingListFromDb = context.MailingLists.FirstOrDefault(m => m.EmailId == id && m.ArtistId == currentArtist.ArtistId);
                context.MailingLists.Remove(mailingListFromDb);
                context.Emails.Remove(emailFromDb);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return View();
            }
        }
    }
}
