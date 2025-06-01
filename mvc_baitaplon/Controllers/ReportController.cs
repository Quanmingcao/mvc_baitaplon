using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.Mvc;
using mvc_baitaplon.Models;

namespace mvc_baitaplon.Controllers
{
    public class ReportController : Controller
    {   
        private Model_Music db = new Model_Music();
        // GET: Reportforuser
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Submit(int? songId, int? accountId, int? collectionId)
        {
            var report = new Report
            {
                ReportedSongID = songId,
                ReportedAccountID = accountId,
                ReportedCollectionID = collectionId
            };

            return View(report);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize]
        public ActionResult ReportUser(int reportedUserId, string reason)
        {
            int? reporterId = (int?)Session["accountID"];
            if (reporterId == null)
                return RedirectToAction("Login", "Account");

            if (string.IsNullOrWhiteSpace(reason))
            {
                TempData["ErrorMessage"] = "Vui lòng nhập lý do.";
                return RedirectToAction("UserProfile", "User", new { id = reportedUserId });
            }
            var user = db.Accounts.FirstOrDefault(c => c.AccountID == reportedUserId);

            var report = new Report
            {
                ReporterID = reporterId.Value,
                ReportedAccountID = reportedUserId,
                Reason = reason,
                CreatedAt = DateTime.Now,
                Status = "Chưa xem xét"
            };
            user.reportcount = (user.reportcount ?? 0) + 1;

            db.Reports.Add(report);
            db.SaveChanges();

            TempData["SuccessMessage"] = "Báo cáo người dùng đã được gửi.";
            return RedirectToAction("Index", "UserProfile", new { id = reportedUserId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize]
        public ActionResult ReportSong(int songId, string reason)
        {
            int? reporterId = (int?)Session["accountID"];
            if (reporterId == null)
                return RedirectToAction("Login", "Account");

            if (string.IsNullOrWhiteSpace(reason))
            {
                TempData["ErrorMessage"] = "Vui lòng nhập lý do.";
                return RedirectToAction("Detail", "Song", new { id = songId });
            }
            var song = db.Songs.FirstOrDefault(c => c.SongID == songId);

            var report = new Report
            {
                ReporterID = reporterId.Value,
                ReportedSongID = songId,
                Reason = reason,
                CreatedAt = DateTime.Now,
                Status = "Chưa xem xét"
            };
            song.reportcount = (song.reportcount ?? 0) + 1;

            db.Reports.Add(report);
            db.SaveChanges();

            TempData["SuccessMessage"] = "Báo cáo bài hát đã được gửi.";
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize]
        public ActionResult ReportCollection(int collectionId, string reason)
        {
            int? reporterId = (int?)Session["accountID"];
            if (reporterId == null)
                return RedirectToAction("Login", "Account");

            if (string.IsNullOrWhiteSpace(reason))
            {
                TempData["ErrorMessage"] = "Vui lòng nhập lý do.";
                return RedirectToAction("Detail", "Collection", new { id = collectionId });
            }
            var collection = db.Collections.FirstOrDefault(c => c.CollectionID == collectionId);

            var report = new Report
            {
                ReporterID = reporterId.Value,
                ReportedCollectionID = collectionId,
                Reason = reason,
                CreatedAt = DateTime.Now,
                Status = "Chưa xem xét"
            };
            collection.reportcount = (collection.reportcount ?? 0) + 1;


            db.Reports.Add(report);
            db.SaveChanges();

            TempData["SuccessMessage"] = "Báo cáo danh sách phát đã được gửi.";
            return RedirectToAction("Detail", "Collection", new { id = collectionId });
        }

    }
}