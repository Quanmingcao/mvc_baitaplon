using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc_baitaplon.Models.Model_View
{
    public class HomePageViewModel
    {
        public List<Song> TopSongs { get; set; }
        public List<Song> NewSongs { get; set; }
        public List<SongLikeViewModel> RecentlyPlayed { get; set; }
    }
}