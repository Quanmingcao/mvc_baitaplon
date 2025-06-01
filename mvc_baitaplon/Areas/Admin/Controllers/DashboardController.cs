using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc_baitaplon.Models;

namespace mvc_baitaplon.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {   
        private Model_Music db=  new Model_Music();
        // GET: Admin/Dashboard
        [CustomAuthorize]
        public ActionResult Index()
        {
            var totalUsers = db.Accounts.Count(a => a.Role == false);
            var totalSongs = db.Songs.Count();
            var totalCollection = db.Collections.Count();
            var report = db.Reports.Count(r => r.Status == "Chưa xem xét");

            ViewBag.totalUsers = totalUsers;
            ViewBag.totalSongs = totalSongs;
            ViewBag.totalCollection = totalCollection;
            ViewBag.report = report;

            return View();
        }

        public JsonResult ListenPerDay()
        {
            var data = db.ListeningHistories
                .Where(l => l.ListenDate.HasValue)
                .GroupBy(l => l.ListenDate.Value.Date)
                .Select(g => new
                {
                    Date = g.Key.ToString("yyyy-MM-dd"),
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Date)
                .Take(30)
                .ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ListenPerMonth()
        {
            var data = db.ListeningHistories
                .Where(l => l.ListenDate.HasValue)
                .GroupBy(l => new { l.ListenDate.Value.Year, l.ListenDate.Value.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Year).ThenByDescending(x => x.Month)
                .Take(12)
                .ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}