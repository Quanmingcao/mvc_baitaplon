using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc_baitaplon.Models;
using PagedList;
namespace mvc_baitaplon.Areas.Admin.Controllers
{
    public class GenresController : Controller
    {   

        private Model_Music db = new Model_Music();
        // GET: Admin/Genres
        public ActionResult Index(int? page,string searchString)
        {   
            var genres = db.Genres.OrderBy(g => g.GenreID).AsQueryable();
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            if (!String.IsNullOrEmpty(searchString))
            {
                genres = genres.Where(h => h.GenreName.Contains(searchString));
            }

            return View(genres.ToPagedList(pageNumber,pageSize));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Genre genre)
        {
            if (ModelState.IsValid)
            {
                db.Genres.Add(genre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(genre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Genre genre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(genre).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(genre);
        }

    }
}