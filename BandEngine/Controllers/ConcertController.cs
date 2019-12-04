using BandEngine.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BandEngine.Controllers
{
    public class ConcertController : Controller
    {
        ApplicationDbContext context;

        public ConcertController()
        {
            context = new ApplicationDbContext();
        }

        // GET: Concert
        [Authorize]
        public ActionResult Index()
        {
            Artist artist = GetCurrentArtist();
            List<Concert> concerts = context.Concerts.Where(c => c.ArtistId == artist.ArtistId && c.ConcertDate >= DateTime.Today).ToList();
            List<ConcertViewModel> allConcerts = new List<ConcertViewModel>();
            foreach(Concert concert in concerts)
            {
                ConcertViewModel concertInfo = new ConcertViewModel();
                concertInfo.Concert = concert;
                concertInfo.Address = context.Addresses.FirstOrDefault(a => a.AddressId == concert.AddressId);
                concertInfo.FullAddress = ConcatAddress(concertInfo.Address);
                allConcerts.Add(concertInfo);
            }
            return View(allConcerts);
        }

        [Authorize]
        public ActionResult PastConcerts()
        {
            Artist artist = GetCurrentArtist();
            List<Concert> concerts = context.Concerts.Where(c => c.ArtistId == artist.ArtistId && c.ConcertDate < DateTime.Today).ToList();
            List<ConcertViewModel> pastConcerts = new List<ConcertViewModel>();
            foreach (Concert concert in concerts)
            {
                ConcertViewModel concertInfo = new ConcertViewModel();
                concertInfo.Concert = concert;
                concertInfo.Address = context.Addresses.FirstOrDefault(a => a.AddressId == concert.AddressId);
                concertInfo.FullAddress = ConcatAddress(concertInfo.Address);
                pastConcerts.Add(concertInfo);
            }
            return View(pastConcerts);
        }

        // GET: Concert/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Concert/Create
        [Authorize]
        public ActionResult Create()
        {
            ConcertViewModel concertInfo = new ConcertViewModel();
            return View(concertInfo);
        }

        // POST: Concert/Create
        [HttpPost]
        public ActionResult Create(ConcertViewModel concertInfo)
        {
            try
            {
                Artist artist = GetCurrentArtist();
                Concert concert = new Concert()
                {
                    ArtistId = artist.ArtistId,
                    ConcertDate = concertInfo.Concert.ConcertDate,
                    VenueName = concertInfo.Concert.VenueName,
                    VenueCapacity = concertInfo.Concert.VenueCapacity
                };
                Address addressToDb = concertInfo.Address;
                context.Addresses.Add(addressToDb);
                context.SaveChanges();
                
                Address addressFromDb = context.Addresses.FirstOrDefault(a => a.AddressLine1 == concertInfo.Address.AddressLine1 && a.AddressLine2 == concertInfo.Address.AddressLine2 && a.City == concertInfo.Address.City && a.State == concertInfo.Address.State && a.ZipCode == concertInfo.Address.ZipCode);
                concert.AddressId = addressFromDb.AddressId;
                context.Concerts.Add(concert);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Authorize]
        public ActionResult SetList(int id)
        {
            Artist artist = GetCurrentArtist();
            Concert currentConcert = context.Concerts.FirstOrDefault(c => c.ConcertId == id);
            SetListViewModel setListInfo = new SetListViewModel()
            {
                AllSongs = context.Songs.Where(s => s.ArtistId == artist.ArtistId).ToList(),
                SetList = context.SetLists.Where(s => s.ConcertId == id).OrderBy(s => s.Position).ToList(),
                CurrentConcert = currentConcert
            };
            return View(setListInfo);
        }

        [Authorize]
        public ActionResult AddToSetList(int id, int concertId)
        {
            Artist artist = GetCurrentArtist();
            SetList setList = new SetList() 
            {
                SongId = id,
                ConcertId = concertId,
            };
            List<SetList> allSetListSongs = context.SetLists.Where(s => s.ConcertId == concertId).ToList();
            allSetListSongs.Add(setList);
            setList.Position = allSetListSongs.Count();
            context.SetLists.Add(setList);
            context.SaveChanges();
            return RedirectToAction("SetList", new { id = concertId });
        }

        [Authorize]
        public ActionResult Remove(int id, int concertId)
        {
            SetList song = context.SetLists.FirstOrDefault(s => s.SongId == id);
            context.SetLists.Remove(song);
            context.SaveChanges();
            List<SetList> setList = context.SetLists.Where(s => s.ConcertId == concertId).OrderBy(s => s.Position).ToList();
            for(int i = 0; i < setList.Count; i++)
            {
                setList[i].Position = i + 1;
                context.SaveChanges();
            }
            return RedirectToAction("SetList", new { id = concertId });
        }

        [Authorize]
        public ActionResult MoveDown(int id, int concertId)
        {
            SetList song = context.SetLists.FirstOrDefault(s => s.SongId == id);
            SetList nextSong = context.SetLists.FirstOrDefault(s => s.Position == song.Position + 1);
            song.Position = nextSong.Position;
            nextSong.Position = song.Position - 1;
            context.SaveChanges();
            return RedirectToAction("SetList", new { id = concertId });
        }

        [Authorize]
        public ActionResult MoveUp(int id, int concertId)
        {
            SetList song = context.SetLists.FirstOrDefault(s => s.SongId == id);
            SetList previousSong = context.SetLists.FirstOrDefault(s => s.Position == song.Position - 1);
            song.Position = previousSong.Position;
            previousSong.Position = song.Position + 1;
            context.SaveChanges();
            return RedirectToAction("SetList", new { id = concertId });
        }

        // GET: Concert/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Concert/Edit/5
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

        // GET: Concert/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Concert/Delete/5
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

        private string ConcatAddress(Address address)
        {
            string zipCode = address.ZipCode.ToString();
            if (address.AddressLine2 != null)
            {
                string fullAddress = address.AddressLine1 + " " + address.AddressLine2 + ", " + address.City + ", " + address.State + " " + zipCode;
                return fullAddress;
            }
            else
            {
                string fullAddress = address.AddressLine1 + ", " + address.City + ", " + address.State + " " + zipCode;
                return fullAddress;
            }
        }
    }
}
