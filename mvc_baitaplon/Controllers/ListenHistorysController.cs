using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc_baitaplon.Models;
using mvc_baitaplon.Models.Model_View;

namespace mvc_baitaplon.Controllers
{
    public class ListenHistorysController : Controller
    {   
        private Model_Music db  = new Model_Music();
        // GET: ListenHistorys
        [CustomAuthorize]
        public ActionResult Index()
        {
            if (Session["accountId"] == null)
            {
                var emptyList = new List<SongLikeViewModel>();
                return View(emptyList);
            }

            int accountid = (int)Session["accountId"];
            var listeningHistory = db.ListeningHistories.Where(l => l.AccountID == accountid)
                .Include(s => s.Song)
                .AsQueryable();
            var viewModel = listeningHistory
            .Select(cs => new SongLikeViewModel
            {
                Song = cs.Song,
                IsLiked = db.Likes.Any(l => l.AccountID == accountid && l.SongID == cs.Song.SongID)
            }).ToList();


            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddToHistory(int songId)
        {
            if (Session["accountId"] == null)
            {
                return new HttpStatusCodeResult(401);
            }

            int userId = (int)Session["accountId"];

            var existingHistory = db.ListeningHistories
                .FirstOrDefault(h => h.AccountID == userId && h.SongID == songId);

            if (existingHistory != null)
            {
                existingHistory.ListenDate = DateTime.Now;
                db.Entry(existingHistory).State = EntityState.Modified;
            }
            else
            {
                var history = new ListeningHistory
                {
                    AccountID = userId,
                    SongID = songId,
                    ListenDate = DateTime.Now
                };
                db.ListeningHistories.Add(history);
            }

            db.SaveChanges();

            return new HttpStatusCodeResult(200);
        }



    }
}