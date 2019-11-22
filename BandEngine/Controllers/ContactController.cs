using BandEngine.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BandEngine.Controllers
{
    public class ContactController : Controller
    {
        ApplicationDbContext context;

        public ContactController()
        {
            context = new ApplicationDbContext();
        }
        // GET: Contact
        [Authorize]
        public ActionResult Index()
        {
            int artistId = GetArtistId();
            List<Contact> contacts = context.Contacts.Where(c => c.ArtistId == artistId).ToList();
            return View(contacts);
        }

        [Authorize]
        public ActionResult Conversations(int id)
        {
            ConversationViewModel conversationInfo = GetConversationInfo(id);
            return View(conversationInfo);
        }

        [Authorize]
        public ActionResult CreateConversation(int id)
        {
            ConversationViewModel conversationInfo = GetConversationInfo(id);
            return View(conversationInfo);
        }

        [HttpPost]
        public ActionResult CreateConversation(ConversationViewModel conversationInfo, int id)
        {
            try
            {
                Conversation conversation = conversationInfo.Conversation;
                conversation.Date = DateTime.Today;
                conversation.ContactId = id;
                Contact contactFromDb = context.Contacts.FirstOrDefault(c => c.ContactId == id);
                contactFromDb.LastContact = conversation.Date;
                contactFromDb.NextContact = conversationInfo.Contact.NextContact;
                context.Conversations.Add(conversation);
                context.SaveChanges();
                return RedirectToAction("Conversations", id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View(id);
            }
        }

        // GET: Contact/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Contact/Create
        [Authorize]
        public ActionResult Create()
        {
            ContactCreationViewModel contactInfo = new ContactCreationViewModel();
            return View(contactInfo);
        }

        // POST: Contact/Create
        [HttpPost]
        public ActionResult Create(ContactCreationViewModel contactInfo)
        {
            try
            {
                int artistId = GetArtistId();
                Contact contact = contactInfo.Contact;
                Address address = contactInfo.Address;
                Email email = contactInfo.Email;
                AddEmail(email);
                AddAddress(address);
                var emailFromDb = context.Emails.FirstOrDefault(e => e.EmailAddress == contactInfo.Email.EmailAddress);
                var addressFromDb = context.Addresses.FirstOrDefault(a => a.AddressLine1 == contactInfo.Address.AddressLine1 && a.AddressLine2 == contactInfo.Address.AddressLine2 && a.City == contactInfo.Address.City && a.State == contactInfo.Address.State && a.ZipCode == contactInfo.Address.ZipCode);
                contact.EmailId = emailFromDb.EmailId;
                contact.AddressId = addressFromDb.AddressId;
                contact.ArtistId = artistId;
                context.Contacts.Add(contact);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View();
            }
        }


        // GET: Contact/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Contact/Edit/5
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

        // GET: Contact/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Contact/Delete/5
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

        private string GetArtistApplicationId()
        {
            string userId = User.Identity.GetUserId();
            var artist = context.Artists.FirstOrDefault(a => a.ApplicationId == userId);
            return artist.ApplicationId;
        }

        private void AddEmail(Email email)
        {
            context.Emails.Add(email);
            context.SaveChanges();
        }

        private void AddAddress(Address address)
        {
            context.Addresses.Add(address);
            context.SaveChanges();
        }

        private int GetArtistId()
        {
            string userId = GetArtistApplicationId();
            var artist = context.Artists.FirstOrDefault(a => a.ApplicationId == userId);
            return artist.ArtistId;
        }

        private ConversationViewModel GetConversationInfo(int id)
        {
            Contact contactFromDb = context.Contacts.FirstOrDefault(c => c.ContactId == id);
            Email emailFromDb = context.Emails.FirstOrDefault(e => e.EmailId == contactFromDb.EmailId);
            ConversationViewModel conversationInfo = new ConversationViewModel();
            conversationInfo.AllConversations = context.Conversations.Where(c => c.ContactId == contactFromDb.ContactId).ToList();
            conversationInfo.Contact = contactFromDb;
            conversationInfo.Email = emailFromDb;
            conversationInfo.ContactName = conversationInfo.Contact.FirstName + " " + conversationInfo.Contact.LastName;
            return conversationInfo;
        }
    }
}
