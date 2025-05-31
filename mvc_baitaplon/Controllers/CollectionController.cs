using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using mvc_baitaplon.Models;
using mvc_baitaplon.Models.Model_View;
using TagLib.Mpeg;

namespace mvc_baitaplon.Controllers
{
    public class CollectionController : Controller
    {   

        private Model_Music db = new Model_Music();
        // GET: Collection
        //public ActionResult Index(int page = 1)
        //{
        //    int pageSize = 6;
        //    int totalSongs = db.Songs.Count();
        //    int totalPages = (int)Math.Ceiling((double)totalSongs / pageSize);

        //    var collections = db.Collections.OrderBy(c => c.CollectionID)
        //    .Include(a => a.Account)
        //    .Include(c => c.CollectionType)
        //    .Include(c => c.CollectionSongs)
        //    .ToList()
        //    .Select(c => new CollectionViewModel
        //    {
        //        CollectionID = c.CollectionID,
        //        Name = c.Name,
        //        Username = c.Account.Username,
        //        CoverImage = c.CoverImage,
        //        TypeName = c.CollectionType.TypeName,
        //        CreatedAt = c.CreatedAt,
        //        SongCount = c.CollectionSongs.Count()
        //    })
        //    .ToList();



        //    ViewBag.CurrentPage = page;
        //    ViewBag.TotalPages = totalPages;

        //    return View(collections);
        //}


        public ActionResult GetUserCollections (int id)
        {   
            var userCollection = db.Collections.Where(c => c.AccountID == id).OrderBy(c => c.CollectionID).AsQueryable(); 

            return View(userCollection);
        }


        public ActionResult AddCollection()
        {
            LoadDropdownData();
            return View(new Collection());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCollection(Collection collection, HttpPostedFileBase CoverImageFile)
        {
            if (Session["accountId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            int accountId = (int)Session["accountId"];

            if (ModelState.IsValid)
            {
                if (CoverImageFile != null && CoverImageFile.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(CoverImageFile.FileName);
                    string path = Path.Combine(Server.MapPath("~/Content/Upload/Image/CoverImage/Collection"), fileName);
                    CoverImageFile.SaveAs(path);
                    collection.CoverImage = fileName;
                }

                collection.AccountID = accountId;
                collection.CreatedAt = DateTime.Now;
                collection.IsDeleted = false;
                collection.IsLocked = false;
                collection.IsPublic = false;
                collection.reportcount = 0;
                db.Collections.Add(collection);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            LoadDropdownData();
            return RedirectToAction("Index", "Songs", new { id = collection.CollectionID });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCollection(int CollectionID, string Name, string Description, HttpPostedFileBase CoverImageFile)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Detail", new { id = CollectionID });
            }

            var collection = db.Collections.Find(CollectionID);
            if (collection == null)
            {
                return HttpNotFound();
            }

            int currentUserId = (int)Session["accountId"];
            if (collection.AccountID != currentUserId)
            {
                return new HttpStatusCodeResult(403);
            }

            collection.Name = Name;
            collection.Description = Description;

            if (CoverImageFile != null && CoverImageFile.ContentLength > 0)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var ext = Path.GetExtension(CoverImageFile.FileName).ToLower();
                if (!allowedExtensions.Contains(ext))
                {
                    ModelState.AddModelError("", "Chỉ chấp nhận định dạng ảnh jpg, jpeg, png, gif.");
                    return RedirectToAction("Detail", new { id = CollectionID });
                }

                string fileName = $"collection_{CollectionID}_{DateTime.Now.Ticks}{ext}";
                string folderPath = Server.MapPath("~/Content/Upload/Image/CoverImage/Collection/");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string fullPath = Path.Combine(folderPath, fileName);
                CoverImageFile.SaveAs(fullPath);

                if (!string.IsNullOrEmpty(collection.CoverImage))
                {
                    string oldFilePath = Path.Combine(folderPath, collection.CoverImage);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                collection.CoverImage = fileName;
            }

            db.SaveChanges();

            return RedirectToAction("Index", "Songs", new { id = CollectionID });
        }

        private void LoadDropdownData(Collection viewModel = null)
        {
            ViewBag.TypeID = new SelectList(db.CollectionTypes.ToList(), "TypeID", "TypeName", viewModel?.TypeID);
        }

        [HttpPost]
        public ActionResult TogglePublic(int CollectionID)
        {
            var collection = db.Collections.Find(CollectionID);
            if (collection != null)
            {
                collection.IsPublic = !collection.IsPublic;
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Songs", new {id = CollectionID });
        }


    }
}