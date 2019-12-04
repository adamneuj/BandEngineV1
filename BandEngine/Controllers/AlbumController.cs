using BandEngine.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BandEngine.Controllers
{
    public class AlbumController : Controller
    {
        ApplicationDbContext context;

        public AlbumController()
        {
            context = new ApplicationDbContext();
        }
        // GET: Album
        [Authorize]
        public ActionResult Index()
        {
            Artist artist = GetCurrentArtist();
            List<Album> albums = context.Albums.Where(a => a.ArtistId == artist.ArtistId).ToList();
            return View(albums);
        }

        // GET: Album/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Album/Create
        [Authorize]
        public ActionResult Create()
        {
            Album album = new Album();
            return View(album);
        }

        // POST: Album/Create
        [HttpPost]
        public ActionResult Create(Album album)
        {
            try
            {
                Artist artist = GetCurrentArtist();
                album.ArtistId = artist.ArtistId;
                context.Albums.Add(album);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Authorize]
        public ActionResult Songs(int id)
        {
            Artist artist = GetCurrentArtist();
            Album album = context.Albums.FirstOrDefault(a => a.AlbumId == id);
            AlbumSongsViewModel albumInfo = new AlbumSongsViewModel()
            {
                CurrentAlbum = album,
                AllSongs = context.Songs.Where(s => s.ArtistId == artist.ArtistId && s.AlbumId != id).ToList(),
                SongsOnAlbum = context.Songs.Where(s => s.ArtistId == artist.ArtistId && s.AlbumId == id).ToList()
            };
            return View(albumInfo);
        }

        [Authorize]
        public ActionResult AddToAlbum(int albumId, int songId)
        {
            Song song = context.Songs.FirstOrDefault(s => s.SongId == songId);
            song.AlbumId = albumId;
            context.SaveChanges();
            return RedirectToAction("Songs", new { id = albumId });
        }

        [Authorize]
        public ActionResult RemoveFromAlbum(int albumId, int songId)
        {
            Song song = context.Songs.FirstOrDefault(s => s.SongId == songId);
            song.AlbumId = null;
            context.SaveChanges();
            return RedirectToAction("Songs", new { id = albumId });
        }

        // GET: Album/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Album/Edit/5
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

        // GET: Album/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Album/Delete/5
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
