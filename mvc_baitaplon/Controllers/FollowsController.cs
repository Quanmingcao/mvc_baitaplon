using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc_baitaplon.Models;

namespace mvc_baitaplon.Controllers
{
    public class FollowsController : Controller
    {
        private Model_Music db = new Model_Music();
        // GET: Follows
        [HttpPost]
        public JsonResult ToggleFollow(int followedUserId)
        {
            try
            {
                if (Session["accountId"] == null)
                {
                    return Json(new { success = false, message = "Bạn chưa đăng nhập." });
                }

                int followerId = (int)Session["accountId"];

                var follower = db.Accounts.Include(a => a.Accounts1).FirstOrDefault(a => a.AccountID == followerId);
                var followed = db.Accounts.FirstOrDefault(a => a.AccountID == followedUserId);

                if (follower == null || followed == null)
                {
                    return Json(new { success = false, message = "Tài khoản không tồn tại." });
                }

                if (follower.Accounts1 == null)
                {
                    follower.Accounts1 = new List<Account>();
                }

                bool isFollowing = follower.Accounts1.Any(a => a.AccountID == followedUserId);

                if (!isFollowing)
                {
                    follower.Accounts1.Add(followed);
                    isFollowing = true;
                }
                else
                {
                    var toRemove = follower.Accounts1.FirstOrDefault(a => a.AccountID == followedUserId);
                    if (toRemove != null)
                    {
                        follower.Accounts1.Remove(toRemove);
                    }
                    isFollowing = false;
                }

                db.SaveChanges();

                return Json(new { success = true, isFollowing });
            }
            catch (Exception ex)
            {
                // Bạn có thể log lỗi ở đây
                return Json(new { success = false, message = "Lỗi server: " + ex.Message });
            }
        }


    }
}