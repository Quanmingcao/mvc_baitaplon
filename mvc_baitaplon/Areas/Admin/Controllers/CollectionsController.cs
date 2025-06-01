using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using mvc_baitaplon.Models;
using PagedList;
namespace mvc_baitaplon.Areas.Admin.Controllers
{
    public class CollectionsController : Controller
    {
        private Model_Music db = new Model_Music();
        [CustomAuthorize]
        public ActionResult Index(string searchString, int? page)
        {
            var collections = db.Collections
            .Where(c => c.IsDeleted == false || c.IsDeleted == null)
            .Include(c => c.CollectionType)
            .Include(a => a.Account)
            .AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                collections = collections.Where(c => c.Name.Contains(searchString));
            }

            collections = collections.OrderBy(c => c.CollectionID);
            int pageSize = 10;
            int pageNumber = page ?? 1;

            return View(collections.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(int id, string search)
        {
            var collection = db.Collections.Find(id);
            if (collection == null)
            {
                return HttpNotFound();
            }

            var songsQuery = db.CollectionSongs
                .Where(cs => cs.CollectionID == id)
                .Select(cs => cs.Song)
                .Include(l => l.Likes)
                .Where(s => s.IsDeleted == false || s.IsDeleted == null)
                .Include(s => s.Genre);

            if (!String.IsNullOrEmpty(search))
            {
                songsQuery = songsQuery.Where(s => s.Title.Contains(search));
            }

            ViewBag.Songs = songsQuery.ToList();

            ViewBag.SearchString = search;

            return View(collection);
        }



        [HttpPost]
        public ActionResult SoftDelete(int Collectionid)
        {
            var song = db.Collections.Find(Collectionid);
            if (song != null)
            {
                song.IsDeleted = true;
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Collections");
        }

        public ActionResult Deleted(int? page)
        {
            var deletedUsers = db.Collections
                .Where(a => a.IsDeleted == true)
                .OrderByDescending(a => a.CreatedAt)
                .ToPagedList(page ?? 1, 10);

            return View(deletedUsers);
        }

        [HttpPost]
        public ActionResult Restore(int id)
        {
            var user = db.Collections.Find(id);
            if (user != null)
            {
                user.IsDeleted = false;
                db.SaveChanges();
            }

            return RedirectToAction("Deleted");
        }

        [HttpPost]
        public ActionResult HardDelete(int id)
        {
            var collection = db.Collections.Find(id);
            if (collection != null && collection.IsDeleted == true)
            {
                try
                {
                    db.Collections.Remove(collection);
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Xóa vĩnh viễn bộ sưu tập thành công.";
                }
                catch
                {
                    TempData["ErrorMessage"] = "Không thể xóa vì collection đang được sử dụng.";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Không tìm thấy bộ sưu tập hoặc bộ sưu tập chưa được đánh dấu xóa.";
            }

            return RedirectToAction("Deleted");
        }


        [HttpPost]
        public ActionResult TogglePublic(int id)
        {
            var collection = db.Collections.Find(id);
            if (collection != null)
            {
                collection.IsPublic = !collection.IsPublic;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult ToggleLock(int id)
        {
            var user = db.Collections.Find(id);
            if (user != null)
            {
                user.IsLocked = !(user.IsLocked ?? false);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Collections");
        }

    }
}
