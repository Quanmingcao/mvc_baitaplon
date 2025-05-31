using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc_baitaplon.Models.Model_View
{
    public class SearchResultViewModel
    {
        public List<Song> Songs { get; set; }
        public List<Collection> Collections { get; set; }
        public List<Account> Accounts { get; set; }
        public string Search { get; set; }
    }
}