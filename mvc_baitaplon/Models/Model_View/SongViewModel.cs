using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc_baitaplon.Models.Model_View
{
    public class SongViewModel
    {
        public int CollectionID { get; set; }
        public string Name { get; set; }
        public string CollectionTypeName { get; set; }
        public string AccountName { get; set; }
        public int LikeCount { get; set; }
    }
}