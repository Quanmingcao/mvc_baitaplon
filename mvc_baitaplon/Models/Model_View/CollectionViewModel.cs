using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc_baitaplon.Models.Model_View
{
    public class CollectionViewModel
    {
        public int CollectionID { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string CoverImage { get; set; }
        public string TypeName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int SongCount { get; set; }
    }
}