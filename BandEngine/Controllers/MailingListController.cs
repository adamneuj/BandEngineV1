using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BandEngine.Controllers
{
    public class MailingListController : Controller
    {
        // GET: MailingList
        public ActionResult Index()
        {
            return View();
        }

        // GET: MailingList/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MailingList/Create
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
