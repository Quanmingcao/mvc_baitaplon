using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc_baitaplon.Models;

namespace mvc_baitaplon.Controllers
{
    public class HomeController : Controller
    {
        private musicweb db = new musicweb();

        public ActionResult Index()
        {
            // Có thể lấy dữ liệu Trending, TopArtist, TopAlbum ở đây hoặc từng partial
            return View();
        }

        public ActionResult TrendingPartial()
        {
            var trendingSong = db.Songs.OrderByDescending(s => s.UploadDate).FirstOrDefault();
            return PartialView("_TrendingPartial", trendingSong);
        }

        public ActionResult TopArtistsPartial()
        {
            var topArtists = db.Artists.Take(4).ToList();
            return PartialView("_TopArtistsPartial", topArtists);
        }

        public ActionResult TopAlbumPartial()
        {
            var topAlbum = db.Albums.OrderByDescending(a => a.ReleaseDate).FirstOrDefault();
            return PartialView("_TopAlbumPartial", topAlbum);
        }

        public ActionResult TopChartPartial()
        {
            var topSongs = db.Songs
                    .OrderByDescending(s => s.Views) // Ưu tiên bài nhiều lượt xem
                    .Take(4)
                    .ToList();
            return PartialView("_TopChartPartial", topSongs);
        }
    }
}