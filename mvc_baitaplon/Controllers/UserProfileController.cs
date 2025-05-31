using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc_baitaplon.Helpers;
using mvc_baitaplon.Models;

namespace mvc_baitaplon.Controllers
{
    public class UserProfileController : Controller
    {
        private Model_Music db = new Model_Music();

        [HttpGet]
        public ActionResult Index(int id)
        {
            var user = db.Accounts
                        .Include(c => c.Collections)
                        .FirstOrDefault(a => a.AccountID == id && a.IsDeleted != true);
            if (user == null)
            {
                return HttpNotFound("User not found");
            }

            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(int AccountID, HttpPostedFileBase ProfileImageFile)
        {
            var user = db.Accounts.Find(AccountID);
            if (user != null)
            {
                user.Fullname = Request.Form["Fullname"];
                user.Email = Request.Form["Email"];

                if (ProfileImageFile != null && ProfileImageFile.ContentLength > 0)
                {
                    if (!string.IsNullOrEmpty(user.ProfileImage) && user.ProfileImage != "default.jpg")
                    {
                        var oldImagePath = Path.Combine(Server.MapPath("~/Content/Upload/Image/ProfileImage"), user.ProfileImage);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    var fileName = Path.GetFileName(ProfileImageFile.FileName);
                    var newImagePath = Path.Combine(Server.MapPath("~/Content/Upload/Image/ProfileImage"), fileName);
                    ProfileImageFile.SaveAs(newImagePath);
                    user.ProfileImage = fileName;
                    Session["ProfileImage"] = fileName;

                }

                db.SaveChanges();
                return RedirectToAction("Index", new { id = user.AccountID });
            }

            var refreshedUser = db.Accounts.Include(c => c.Collections)
                                           .FirstOrDefault(a => a.AccountID == AccountID);
            return View("Index", refreshedUser);
        }



        [HttpPost]
        public ActionResult ChangePassword(int AccountID, string CurrentPassword, string NewPassword, string ConfirmNewPassword)
        {
            var account = db.Accounts.Find(AccountID);
            if (account == null)
                return HttpNotFound();

            string hashedCurrentPassword = HashHelper.ToSHA256(CurrentPassword);

            if (account.PasswordHash != hashedCurrentPassword)
            {
                TempData["Error"] = "Mật khẩu hiện tại không đúng.";
                return RedirectToAction("Detail", new { id = AccountID });
            }

            if (NewPassword != ConfirmNewPassword)
            {
                TempData["Error"] = "Mật khẩu xác nhận không khớp.";
                return RedirectToAction("Detail", new { id = AccountID });
            }

            string hashedNewPassword = HashHelper.ToSHA256(NewPassword);
            account.PasswordHash = hashedNewPassword;
            db.SaveChanges();

            TempData["Success"] = "Đổi mật khẩu thành công!";
            return RedirectToAction("Detail", new { id = AccountID });
        }


    }
}
