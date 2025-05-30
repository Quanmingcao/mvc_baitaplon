using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc_baitaplon.Models;

namespace mvc_baitaplon.Controllers
{
    public class ReportforuserController : Controller
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
        public ActionResult Submit(Report report)
        {
            int? reporterId = Session["AccountID"] as int?;
            if (reporterId == null)
            {
                TempData["ErrorMessage"] = "Bạn cần đăng nhập để báo cáo.";
                return RedirectToAction("Login", "Account"); 
            }

            if (string.IsNullOrWhiteSpace(report.Reason))
            {
                ModelState.AddModelError("Reason", "Vui lòng nhập lý do.");
                return View(report);
            }

            report.ReporterID = reporterId.Value;
            report.CreatedAt = DateTime.Now;
            report.Status = "Chưa xem xét";

            db.Reports.Add(report);
            db.SaveChanges();

            TempData["SuccessMessage"] = "Báo cáo của bạn đã được gửi. Cảm ơn bạn!";
            return RedirectToAction("Index", "Home");
        }


    }
}