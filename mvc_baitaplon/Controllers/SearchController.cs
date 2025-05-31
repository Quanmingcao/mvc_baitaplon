using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc_baitaplon.Models.Model_View;
using mvc_baitaplon.Models;

namespace mvc_baitaplon.Controllers
{
    public class SearchController : Controller
    {
        private Model_Music db = new Model_Music();

        // GET: Search
        public ActionResult Index(string search)
        {
            var model = new SearchResultViewModel
            {
                Search = search,
                Songs = new List<Song>(),
                Collections = new List<Collection>(),
                Accounts = new List<Account>()
            };

            if (!string.IsNullOrWhiteSpace(search))
            {
                model.Songs = db.Songs
                    .Where(s => s.Title.Contains(search)
                                && s.IsDeleted == false
                                && s.IsLocked == false
                                && s.IsPublic == true)
                    .ToList();

                model.Collections = db.Collections
                    .Where(c => c.Name.Contains(search)
                                && c.IsDeleted == false
                                && c.IsLocked == false
                                && c.IsPublic == true)
                    .ToList();

                model.Accounts = db.Accounts
                    .Where(a => (a.Username.Contains(search) || a.Email.Contains(search))
                                && a.IsDeleted == false
                                && a.IsLocked == false)
                    .ToList();
            }

            return View(model);
        }

        [HttpGet]
        public JsonResult Suggest(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                return Json(new
                {
                    users = new string[0],
                    songs = new string[0],
                    collections = new string[0]
                }, JsonRequestBehavior.AllowGet);
            }

            var users = db.Accounts
                .Where(a => a.Username.Contains(term)
                            && a.IsDeleted == false
                            && a.IsLocked == false)
                .Select(a => new { id = a.AccountID, username = a.Username })
                .Take(5)
                .ToList();

            var songs = db.Songs
                .Where(s => s.Title.Contains(term)
                            && s.IsDeleted == false
                            && s.IsLocked == false
                            && s.IsPublic == true)
                .Select(s => new { id = s.SongID, title = s.Title })
                .Take(5)
                .ToList();

            var collections = db.Collections
                .Where(c => c.Name.Contains(term)
                            && c.IsDeleted == false
                            && c.IsLocked == false
                            && c.IsPublic == true)
                .Select(c => new { id = c.CollectionID, name = c.Name })
                .Take(5)
                .ToList();

            return Json(new
            {
                users,
                songs,
                collections
            }, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }

}