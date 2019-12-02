using BandEngine.Models;
using Microsoft.AspNet.Identity;
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

            return View();
        }

        // POST: MailingList/Create
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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MailingList/Delete/5
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
