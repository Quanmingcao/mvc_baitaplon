using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc_baitaplon.Models.Model_View
{
    public class SongWithGenreViewModel
    {
        public int SongID { get; set; }
        public string Title { get; set; }
        public string GenreName { get; set; }
        public TimeSpan Duration { get; set; }
    }
}