using System;
using System.Data.Entity;
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
        public ActionResult Index(Account model, HttpPostedFileBase ProfileImageFile)
        {
            if (ModelState.IsValid)
            {
                var user = db.Accounts.Find(model.AccountID);
                if (user != null)
                {
                    user.Fullname = model.Fullname;
                    user.Email = model.Email;

                    if (ProfileImageFile != null && ProfileImageFile.ContentLength > 0)
                    {
                        var fileName = System.IO.Path.GetFileName(ProfileImageFile.FileName);
                        var path = Server.MapPath("~/Content/Upload/Image/ProfileImage/" + fileName);
                        ProfileImageFile.SaveAs(path);
                        user.ProfileImage = fileName;
                    }

                    db.SaveChanges();
                    return RedirectToAction("Index", new { id = user.AccountID });
                }
            }

            var refreshedUser = db.Accounts
                                  .Include(c => c.Collections)
                                  .FirstOrDefault(a => a.AccountID == model.AccountID);
            return View(refreshedUser);
        }

        [HttpPost]
        public ActionResult ChangePassword(int AccountID, string CurrentPassword, string NewPassword, string ConfirmNewPassword)
        {
            var account = db.Accounts.Find(AccountID);

            if (account == null)
                return HttpNotFound();
            string hashedPassword = HashHelper.ToSHA256(CurrentPassword);


            if (account.PasswordHash != hashedPassword)
            {
                TempData["Error"] = "Mật khẩu hiện tại không đúng.";
                return RedirectToAction("Detail", new { id = AccountID });
            }

            if (NewPassword != ConfirmNewPassword)
            {
                TempData["Error"] = "Mật khẩu xác nhận không khớp.";
                return RedirectToAction("Detail", new { id = AccountID });
            }

            account.PasswordHash = NewPassword;
            db.SaveChanges();

            TempData["Success"] = "Đổi mật khẩu thành công!";
            return RedirectToAction("Detail", new { id = AccountID });
        }

    }
}
