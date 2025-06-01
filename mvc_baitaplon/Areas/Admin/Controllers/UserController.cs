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
    public class UserController : Controller
    {   

        private Model_Music db = new Model_Music();
        [CustomAuthorize]
        public ActionResult Index(int? page,string searchString)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var users = db.Accounts
                .Where(a => a.IsDeleted == false || a.IsDeleted == null)
                .OrderBy(a => a.AccountID)
                .Select(a => new AccountViewModel
                {
                    AccountID = a.AccountID,
                    Username = a.Username,
                    Fullname = a.Fullname,
                    Email = a.Email,
                    CreatedAt = a.CreatedAt,
                    Role = a.Role,
                    IsLocked = a.IsLocked
                })
               .AsQueryable();
            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(h => h.Username.Contains(searchString) || h.Fullname.Contains(searchString));
            }

            return View(users.ToPagedList(pageNumber, pageSize));
        }


        public ActionResult Details(int id)
        {
            var user = db.Accounts.Find(id);
            if (user == null) return HttpNotFound();

            var history = db.ListeningHistories
                            .Where(h => h.AccountID == id)
                            .OrderByDescending(h => h.ListenDate)
                            .ToList();

            var uploads = db.Songs.Count(s => s.AccountID == id);
            var totalViews = db.Songs.Where(s => s.AccountID == id).Sum(s => (int?)s.Views) ?? 0;
            db.Entry(user).Collection(u => u.Accounts1).Load();

            var totalFollowers = user.Accounts1.Count;

            var songIds = db.Songs.Where(s => s.AccountID == id).Select(s => s.SongID).ToList();
            var totalLikes = db.Likes.Count(l => songIds.Contains(l.SongID));

            ViewBag.History = history;
            ViewBag.UploadCount = uploads;
            ViewBag.TotalViews = totalViews;
            ViewBag.TotalFollowers = totalFollowers;
            ViewBag.TotalLikes = totalLikes;

            return View(user);
        }


        [HttpPost]
        public ActionResult Lock(int id, int days)
        {
            var user = db.Accounts.Find(id);
            if (user != null)
            {
                user.IsLocked = true;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult Unlock(int id)
        {
            var user = db.Accounts.Find(id);
            if (user != null)
            {
                user.IsLocked = false;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ToggleLock(int id)
        {
            var user = db.Accounts.Find(id);
            if (user != null)
            {
                user.IsLocked = !(user.IsLocked ?? false);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ToggleRole(int id)
        {
            var user = db.Accounts.Find(id);
            if (user != null)
            {
                user.Role = !user.Role;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult SoftDelete(int id)
        {
            var user = db.Accounts.Find(id);
            if (user != null)
            {
                user.IsDeleted = true;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Deleted(int? page)
        {
            var deletedUsers = db.Accounts
                .Where(a => a.IsDeleted == true)
                .OrderByDescending(a => a.CreatedAt)
                .Select(a => new AccountViewModel
                {
                    AccountID = a.AccountID,
                    Username = a.Username,
                    Fullname = a.Fullname,
                    Email = a.Email,
                    CreatedAt = a.CreatedAt,
                    Role = a.Role,
                    IsLocked = a.IsLocked
                }).ToPagedList(page ?? 1, 10);

            return View(deletedUsers);
        }

        [HttpPost]
        public ActionResult Restore(int id)
        {
            var user = db.Accounts.Find(id);
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
            var users = db.Accounts.Find(id);
            if (users != null && users.IsDeleted == true)
            {
                try
                {
                    db.Accounts.Remove(users);
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Xóa vĩnh viễn người dùng thành công.";
                }
                catch
                {
                    TempData["ErrorMessage"] = "Không thể xóa.";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Không tìm thấy người dùng.";
            }

            return RedirectToAction("Deleted");
        }

    }
}