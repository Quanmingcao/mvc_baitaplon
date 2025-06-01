using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using mvc_baitaplon.Models;
using mvc_baitaplon.Models.Model_View;

namespace mvc_baitaplon.Controllers
{
    public class HomeController : Controller
    {
        private Model_Music db = new Model_Music();

        public ActionResult Index()
        {
            var topSongs = db.Songs
                .Where(s => s.IsDeleted == false && s.IsLocked == false && s.IsPublic == true)
                .OrderByDescending(s => s.Views)
                .Take(6)
                .ToList();

            var newSongs = db.Songs
                .Where(s => s.IsDeleted == false && s.IsLocked == false && s.IsPublic == true)
                .OrderByDescending(s => s.UploadDate)
                .Take(6)
                .ToList();

            List<SongLikeViewModel> recentlyPlayed = new List<SongLikeViewModel>();

            if (Session["accountId"] != null)
            {
                int accountId = (int)Session["accountId"];

                var listeningHistory = db.ListeningHistories
                    .Where(l => l.AccountID == accountId &&
                                l.Song.IsDeleted == false&&
                                l.Song.IsLocked == false &&
                                l.Song.IsPublic == true)
                    .Include(l => l.Song)
                    .OrderByDescending(l => l.ListenDate)
                    .ToList();

                var likedSongIds = db.Likes
                    .Where(l => l.AccountID == accountId)
                    .Select(l => l.SongID)
                    .ToHashSet();

                Dictionary<int, SongLikeViewModel> uniqueSongs = new Dictionary<int, SongLikeViewModel>();

                foreach (var history in listeningHistory)
                {
                    int songId = history.Song.SongID;
                    if (!uniqueSongs.ContainsKey(songId))
                    {
                        uniqueSongs[songId] = new SongLikeViewModel
                        {
                            Song = history.Song,
                            IsLiked = likedSongIds.Contains(songId)
                        };
                    }
                }

                recentlyPlayed = uniqueSongs.Values.ToList();
            }

            var model = new HomePageViewModel
            {
                TopSongs = topSongs,
                NewSongs = newSongs,
                RecentlyPlayed = recentlyPlayed
            };

            return View(model);
        }







    }
}