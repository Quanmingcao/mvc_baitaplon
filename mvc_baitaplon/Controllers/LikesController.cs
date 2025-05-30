using System;
using System.Linq;
using System.Web.Mvc;
using mvc_baitaplon.Models;

namespace mvc_baitaplon.Controllers
{
    public class LikesController : Controller
    {
        private Model_Music db = new Model_Music();

        // POST: Likes/Add
        [HttpPost]
        public JsonResult Add(Like request)
        {
            int? accountId = Session["AccountID"] as int?;
            if (accountId == null)
            {
                return Json(new { success = false, message = "Bạn cần đăng nhập để thả tim." });
            }

            var existing = db.Likes.FirstOrDefault(l => l.AccountID == accountId && l.SongID == request.SongID);
            if (existing != null)
            {
                return Json(new { success = false, message = "Bạn đã thả tim bài này rồi!" });
            }

            var like = new Like
            {
                AccountID = accountId.Value,
                SongID = request.SongID,
                LikedAt = DateTime.Now
            };

            db.Likes.Add(like);
            db.SaveChanges();

            return Json(new { success = true, message = "Đã thả tim bài hát.", liked = true });
        }

        [HttpPost]
        public JsonResult Remove(int songId)
        {
            int? accountId = Session["AccountID"] as int?;
            if (accountId == null)
            {
                return Json(new { success = false, message = "Bạn cần đăng nhập để bỏ thả tim." });
            }

            var like = db.Likes.FirstOrDefault(l => l.AccountID == accountId && l.SongID == songId);
            if (like == null)
            {
                return Json(new { success = false, message = "Bạn chưa thả tim bài hát này." });
            }

            db.Likes.Remove(like);
            db.SaveChanges();

            return Json(new { success = true, message = "Đã bỏ thả tim bài hát.", liked = false });
        }

        public JsonResult Isliked(int songid)
        {
            int? accountId = Session["AccountID"] as int?;
            bool liked = false;
            if (accountId != null)
            {
                liked = db.Likes.Any(l => l.AccountID == accountId && l.SongID == songid);
            }
            return Json(new { liked = liked }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Toggle(int songId)
        {
            int? accountId = Session["AccountID"] as int?;
            if (accountId == null)
            {
                return Json(new { success = false, message = "Bạn cần đăng nhập để thả tim." });
            }

            var existing = db.Likes.FirstOrDefault(l => l.AccountID == accountId && l.SongID == songId);

            if (existing != null)
            {
                db.Likes.Remove(existing);
                db.SaveChanges();
                return Json(new { success = true, message = "Đã bỏ thả tim bài hát.", liked = false });
            }
            else
            {
                // Nếu chưa like thì thêm like
                var like = new Like
                {
                    AccountID = accountId.Value,
                    SongID = songId,
                    LikedAt = DateTime.Now
                };
                db.Likes.Add(like);
                db.SaveChanges();
                return Json(new { success = true, message = "Đã thả tim bài hát.", liked = true });
            }
        

    }

}
}
