using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc_baitaplon.Models;

namespace mvc_baitaplon.Controllers
{
    public class SongsController : Controller
    {   
        private Model_Music db = new Model_Music();
        // GET: Songs
        public ActionResult Index(int id)
        {
            var SongbyCollection = db.CollectionSongs.Where(c => c.CollectionID == id)
                .Include(c => c.Song)
                .Include(c => c.Collection)
                .AsQueryable();   

            return View(SongbyCollection);
        }
    }
}