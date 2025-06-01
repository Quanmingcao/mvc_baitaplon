using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc_baitaplon.Models;

namespace mvc_baitaplon.Areas.Admin.Controllers
{
    public class ReportsController : Controller
    {   

        private Model_Music db = new Model_Music();
        [CustomAuthorize]
        public ActionResult Index(int? page,string searchString)
        {
            var reports = db.Reports.Where(r => r.Status == "Chưa xem xét").OrderBy(r => r.ReporterID)
                .Include(a => a.Account)
                .Include(s => s.Song)
                .Include(c => c.Collection)
                .Include(a1 => a1.Account1)
                .OrderBy(r => r.ReporterID)
                .AsQueryable();
            if (!String.IsNullOrEmpty(searchString))
            {
                reports = reports.Where(h => h.Song.Title.Contains(searchString)
                                     || h.Account1.Username.Contains(searchString)
                                     || h.Collection.Name.Contains(searchString));
            }

            return View(reports.ToList());
        }

        public ActionResult Details(int id)
        {
            var report = db.Reports
                           .Include("Account")
                           .Include("Song")
                           .Include("Collection")
                           .FirstOrDefault(r => r.ReportID == id);

            if (report == null)
            {
                return HttpNotFound();
            }

            return View(report);
        }


        [HttpPost]
        public ActionResult Process(int id)
        {
            var report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            report.Status = "Đã xem xét";
            db.SaveChanges();

            return Redirect("Index");
        }


    }
}