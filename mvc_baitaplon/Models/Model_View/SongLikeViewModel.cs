using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvc_baitaplon.Models.Model_View
{
    public class SongLikeViewModel
    {
        public Song Song { get; set; }
        public bool IsLiked { get; set; }
    }
}