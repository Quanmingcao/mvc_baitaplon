using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc_baitaplon.Models;
using mvc_baitaplon.Models.Model_View;

namespace mvc_baitaplon.Controllers
{
    public class CollectionsUserController : Controller
    {   

        private Model_Music db = new Model_Music();
        // GET: Collection
        public ActionResult Index(int page = 1)
        {
            int pageSize = 6;
            int totalSongs = db.Songs.Count();
            int totalPages = (int)Math.Ceiling((double)totalSongs / pageSize);

            var collections = db.Collections.OrderBy(c => c.CollectionID)
            .Include(a => a.Account)
            .Include(c => c.CollectionType)
            .Include(c => c.CollectionSongs)
            .ToList()
            .Select(c => new CollectionViewModel
            {
                CollectionID = c.CollectionID,
                Name = c.Name,
                Username = c.Account.Username,
                CoverImage = c.CoverImage,
                TypeName = c.CollectionType.TypeName,
                CreatedAt = c.CreatedAt,
                SongCount = c.CollectionSongs.Count()
            })
            .ToList();



            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(collections);
        }


        public ActionResult GetUserCollections (int id)
        {   
            var userCollection = db.Collections.Where(c => c.AccountID == id).OrderBy(c => c.CollectionID).AsQueryable(); 

            return View(userCollection);
        }

    }
}