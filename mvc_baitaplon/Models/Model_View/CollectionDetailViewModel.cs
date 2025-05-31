using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc_baitaplon.Models.Model_View
{
    public class CollectionDetailViewModel
    {
        public Collection Collection { get; set; }
        public IEnumerable<SongLikeViewModel> Songs { get; set; }

    }
}