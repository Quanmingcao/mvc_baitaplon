using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc_baitaplon.Models;

namespace mvc_baitaplon.Controllers
{
    public class LibraryController : Controller
    {   

        private Model_Music db =  new Model_Music();
        [CustomAuthorizeAttribute]
        public ActionResult Index()
        {
            var accountId = Session["accountId"] as int?;

            var library = db.CollectionLibraries.Where(a => a.AccountID == accountId).OrderByDescending(a => a.SavedAt)
                .Include(c => c.Collection).AsQueryable();
                
            return View(library);
        }

        [HttpPost]
        public ActionResult Addlibrary (int Collectionid)
        {
            int accountId = (int)Session["accountId"];
            var collection = db.Collections.FirstOrDefault(c => c.CollectionID == Collectionid);
            if (collection == null)
            {
                return HttpNotFound("Bộ sưu tập không tồn tại.");
            }

            if (collection.AccountID == accountId)
            {
                TempData["Message"] = "Bạn không thể lưu bộ sưu tập của chính mình.";
                return RedirectToAction("Index");
            }

            bool alreadySaved = db.CollectionLibraries.Any(cl => cl.AccountID == accountId && cl.CollectionID == Collectionid);
            if (alreadySaved)
            {
                TempData["Message"] = "Bộ sưu tập này đã có trong thư viện của bạn.";
                return RedirectToAction("Index");
            }

            var newItemLibrary = new CollectionLibrary()
            {   
                AccountID = accountId,
                CollectionID = Collectionid,
                SavedAt = DateTime.Now,
            };

            db.CollectionLibraries.Add(newItemLibrary);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}