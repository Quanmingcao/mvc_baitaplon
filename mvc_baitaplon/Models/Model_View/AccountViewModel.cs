using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc_baitaplon.Models.Model_View
{
    public class AccountViewModel
    {
        public int AccountID { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool? Role { get; set; }
        public bool? IsLocked { get; set; }
    }
}