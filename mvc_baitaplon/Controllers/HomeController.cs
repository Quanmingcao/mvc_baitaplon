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

        [CustomAuthorizeAttribute]
        public ActionResult Index()
        {
            int pageSize = 6;
            int totalSongs = db.Songs.Count();
            int totalPages = (int)Math.Ceiling((double)totalSongs / pageSize);

            var songs = db.Songs
                .OrderByDescending(s => s.UploadDate)
                .Include(a => a.Account)
                .AsQueryable();


            return View(songs);
        }




    }
}