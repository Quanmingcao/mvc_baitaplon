using System;
using System.Linq;
using System.Web.Mvc;
using mvc_baitaplon.Models;
using mvc_baitaplon.Models.Model_View;

namespace mvc_baitaplon.Controllers
{
    public class LikesController : Controller
    {
        private Model_Music db = new Model_Music();
        public ActionResult LikedSongs()
        {
            if (Session["accountId"] == null)
                return RedirectToAction("Login", "Login");

            int accountId = (int)Session["accountId"];

            // Lấy danh sách bài hát user đã like, loại bỏ bài private
            var likedSongs = db.Likes
                .Where(l => l.AccountID == accountId)
                .OrderByDescending(l => l.LikedAt)
                .Select(l => new SongLikeViewModel
                {
                    Song = l.Song,
                    IsLiked = true // vì đây là danh sách liked nên luôn true
                })
                .Where(vm => vm.Song.IsPublic == true || vm.Song.AccountID == accountId) // Hiển thị nếu public hoặc của chính user
                .ToList();

            return PartialView("_LikedSongs", likedSongs);
        }

        [HttpPost]
        public JsonResult ToggleLike(int songId)
        {
            if (Session["accountId"] == null || !int.TryParse(Session["accountId"].ToString(), out int accountId))
            {
                return Json(new { success = false, message = "Bạn chưa đăng nhập." });
            }
             
            try
            {
                var existing = db.Likes.FirstOrDefault(l => l.AccountID == accountId && l.SongID == songId);
                bool isLiked;

                if (existing == null)
                {
                    db.Likes.Add(new Like
                    {
                        AccountID = accountId,
                        SongID = songId,
                        LikedAt = DateTime.Now
                    });
                    isLiked = true;
                }
                else
                {
                    db.Likes.Remove(existing);
                    isLiked = false;
                }

                db.SaveChanges();

                return Json(new { success = true, isLiked });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


    }
}
