using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc_baitaplon.Models;
using mvc_baitaplon.Models.Model_View;
using PagedList;
namespace mvc_baitaplon.Areas.Admin.Controllers
{
    public class SongsController : Controller
    {   
        private Model_Music db = new Model_Music();
        // GET: Admin/Song
        public ActionResult Create()
        {
            return View(); 
        }


        [HttpPost]
        public ActionResult ToggleLock(int Songid)
        {
            var user = db.Songs.Find(Songid);
            if (user != null)
            {
                user.IsLocked = !(user.IsLocked ?? false);
                db.SaveChanges();
            }
            return RedirectToAction("Index","Collections");
        }


        [HttpPost]
        public ActionResult SoftDelete(int Songid, int Collectionid)
        {
            var song = db.Songs.Find(Songid);
            if (song != null)
            {
                song.IsDeleted = true;
                db.SaveChanges();
            }
            return RedirectToAction("Details","Collections", new { id = Collectionid });
        }

        public ActionResult Deleted(int? page)
        {
            var deletedUsers = db.Songs
                .Where(a => a.IsDeleted == true)
                .OrderByDescending(a => a.UploadDate)
                .Include(g => g.Genre)
                .ToPagedList(page ?? 1, 10);

            return View(deletedUsers);
        }
        [HttpPost]
        public ActionResult Restore(int id)
        {
            var user = db.Songs.Find(id);
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
            var song = db.Songs.Find(id);
            if (song != null && song.IsDeleted == true)
            {
                try
                {
                    db.Songs.Remove(song);
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Xóa vĩnh viễn bài hát thành công.";
                }
                catch
                {
                    TempData["ErrorMessage"] = "Không thể xóa vì bài hát đang được sử dụng trong thư viện.";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Không tìm thấy bài hát hoặc bài hát chưa bị xóa mềm.";
            }
            return RedirectToAction("Deleted");

        }
        [HttpPost]
        public ActionResult TogglePublic(int id)
        {
            var song = db.Songs.Find(id);
            if (song != null)
            {
                song.IsPublic = !song.IsPublic;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}