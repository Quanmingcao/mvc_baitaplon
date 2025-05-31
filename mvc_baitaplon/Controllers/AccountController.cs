using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc_baitaplon.Helpers;
using mvc_baitaplon.Models;

namespace mvc_baitaplon.Controllers
{
    public class AccountController : Controller
    {   

        private Model_Music context = new Model_Music();
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Vui lòng nhập đầy đủ tài khoản và mật khẩu.";
                return View();
            }

            string hashedPassword = HashHelper.ToSHA256(password);

            var account = context.Accounts.FirstOrDefault(a =>
                a.Username == username &&
                a.PasswordHash == hashedPassword &&
                a.IsDeleted == false &&
                a.IsLocked == false
            );

            if (account != null)
            {
                Session["username"] = account.Username;
                Session["accountId"] = account.AccountID;
                Session["role"] = account.Role;
                Session["ProfileImage"] = account.ProfileImage;


                if (account.Role == true)
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.Error = "Tài khoản hoặc mật khẩu không đúng.";
            return View();
        }

        public ActionResult Register(string username, string password)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(string username, string email, string password, string confirmPassword)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(email))
            {
                ViewBag.Error = "Vui lòng nhập đầy đủ thông tin.";
                return View();
            }

            if (password != confirmPassword)
            {
                ViewBag.Error = "Mật khẩu nhập lại không khớp.";
                return View();
            }

            var existingUser = context.Accounts.FirstOrDefault(a => a.Username == username || a.Email == email);
            if (existingUser != null)
            {
                ViewBag.Error = "Tên tài khoản hoặc email đã tồn tại.";
                return View();
            }

            string hashedPassword = HashHelper.ToSHA256(password);

            var newAccount = new Account
            {
                Username = username,
                Email = email,
                PasswordHash = hashedPassword,
                Role = false
            };

            context.Accounts.Add(newAccount);
            context.SaveChanges();

            TempData["Success"] = "Đăng ký thành công! Mời bạn đăng nhập.";
            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}