using System;
using System.Linq;
using System.Web.Mvc;
using mvc_baitaplon.Models;

namespace mvc_baitaplon.Controllers
{
    public class LikesController : Controller
    {
        private Model_Music db = new Model_Music();

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
