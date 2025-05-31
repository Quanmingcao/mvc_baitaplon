using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc_baitaplon.Models;
using mvc_baitaplon.Models.Model_View;
using TagLib;

namespace mvc_baitaplon.Controllers
{
    public class SongsController : Controller
    {   
        private Model_Music db = new Model_Music();
        // GET: Songs
        [CustomAuthorize]
        public ActionResult Index(int id)
        {
            if (Session["accountId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            int accountId = (int)Session["accountId"];

            var collection = db.Collections
                .Include(c => c.Account)
                .FirstOrDefault(c => c.CollectionID == id);

            if (collection == null)
            {
                return HttpNotFound();
            }

            bool isOwner = collection.AccountID == accountId;

            var songsInCollection = db.CollectionSongs
                .Where(cs => cs.CollectionID == id)
                .Include(cs => cs.Song)
                .ToList();

            var genres = db.Genres.ToList();

            var songs = songsInCollection
                .Where(cs => isOwner || cs.Song.IsPublic == true)
                .Select(cs => new SongLikeViewModel
                {
                    Song = cs.Song,
                    IsLiked = db.Likes.Any(l => l.AccountID == accountId && l.SongID == cs.Song.SongID),
                }).ToList();

            var viewModel = new CollectionDetailViewModel
            {
                Collection = collection,
                Songs = songs
            };

            return View(viewModel);
        }





        public ActionResult AddSong(int Collectionid)
        {
            ViewBag.Collectionid = Collectionid;
            LoadDropdownData();
            return View(new Song());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSong(Song song, HttpPostedFileBase CoverImage, HttpPostedFileBase AudioFile, int Collectionid)
        {
            if (Session["accountId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            int accountId = (int)Session["accountId"];
            if (CoverImage != null && AudioFile != null)
            {
                var imageName = Path.GetFileName(CoverImage.FileName);
                var imagePath = Path.Combine(Server.MapPath("~/Content/Upload/Image/CoverImage/Song"), imageName);
                CoverImage.SaveAs(imagePath);
                song.CoverImage = imageName;

                var audioName = Path.GetFileName(AudioFile.FileName);
                var audioPath = Path.Combine(Server.MapPath("~/Content/Upload/Music_file"), audioName);
                AudioFile.SaveAs(audioPath);
                song.FilePath = audioName;
                song.UploadDate = DateTime.Now;
                song.Views = 0;
                song.IsDeleted = false;
                song.IsLocked = false;
                song.IsPublic = false;
                song.reportcount = 0;
                var file = TagLib.File.Create(audioPath);
                TimeSpan duration = file.Properties.Duration;
                song.Duration = (int)duration.TotalSeconds;
                song.AccountID = accountId;
                db.Songs.Add(song);
                db.SaveChanges();

                var collectionSong = new CollectionSong
                {
                    CollectionID = Collectionid,
                    SongID = song.SongID
                };
                db.CollectionSongs.Add(collectionSong);
                db.SaveChanges();

                return RedirectToAction("Index", "Songs", new { id = Collectionid });
            }

            LoadDropdownData();

            TempData["Error"] = "Vui lòng chọn đầy đủ tệp nhạc và ảnh bìa.";
            return RedirectToAction("Index","Songs", new {id = Collectionid });
        }

        private void LoadDropdownData(Song viewModel = null)
        {
            ViewBag.GenreID = new SelectList(db.Genres.ToList(), "GenreID", "GenreName", viewModel?.GenreID);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSong(Song updatedSong, HttpPostedFileBase CoverImage, HttpPostedFileBase AudioFile, int Collectionid)
        {
            if (Session["accountId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            int accountId = (int)Session["accountId"];

            var existingSong = db.Songs.Find(updatedSong.SongID);
            if (existingSong == null)
            {
                return HttpNotFound();
            }

            existingSong.Title = updatedSong.Title;
            existingSong.GenreID = updatedSong.GenreID;

            if (CoverImage != null && CoverImage.ContentLength > 0)
            {
                if (!string.IsNullOrEmpty(existingSong.CoverImage))
                {
                    var oldImagePath = Path.Combine(Server.MapPath("~/Content/Upload/Image/CoverImage/Song"), existingSong.CoverImage);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                var imageName = Path.GetFileName(CoverImage.FileName);
                var imagePath = Path.Combine(Server.MapPath("~/Content/Upload/Image/CoverImage/Song"), imageName);
                CoverImage.SaveAs(imagePath);
                existingSong.CoverImage = imageName;
            }

            if (AudioFile != null && AudioFile.ContentLength > 0)
            {
                if (!string.IsNullOrEmpty(existingSong.FilePath))
                {
                    var oldAudioPath = Path.Combine(Server.MapPath("~/Content/Upload/Music_file"), existingSong.FilePath);
                    if (System.IO.File.Exists(oldAudioPath))
                    {
                        System.IO.File.Delete(oldAudioPath);
                    }
                }

                var audioName = Path.GetFileName(AudioFile.FileName);
                var audioPath = Path.Combine(Server.MapPath("~/Content/Upload/Music_file"), audioName);
                AudioFile.SaveAs(audioPath);
                existingSong.FilePath = audioName;

                var file = TagLib.File.Create(audioPath);
                TimeSpan duration = file.Properties.Duration;
                existingSong.Duration = (int)duration.TotalSeconds;
            }

            existingSong.AccountID = accountId;
            db.Entry(existingSong).State = EntityState.Modified;
            db.SaveChanges();

            var existingLink = db.CollectionSongs.FirstOrDefault(cs => cs.SongID == existingSong.SongID && cs.CollectionID == Collectionid);
            if (existingLink == null)
            {
                var newLink = new CollectionSong
                {
                    SongID = existingSong.SongID,
                    CollectionID = Collectionid
                };
                db.CollectionSongs.Add(newLink);
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Songs", new { id = Collectionid });
        }

        [HttpPost]
        public ActionResult TogglePublic(int SongID, int CollectionID)
        {
            var song = db.Songs.Find(SongID);
            if (song != null)
            {
                song.IsPublic = !song.IsPublic;
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Songs", new { id = CollectionID });
        }

        [HttpPost]
        public ActionResult AddToView(int SongID)
        {

            var song = db.Songs.Find(SongID);
            if (song == null)
            {
                return HttpNotFound();
            }

            song.Views = (song.Views ?? 0) + 1;
            db.Entry(song).State = EntityState.Modified;
            db.SaveChanges();

            return new HttpStatusCodeResult(200);
        }

    }
}