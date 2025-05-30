namespace mvc_baitaplon.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ListeningHistory")]
    public partial class ListeningHistory
    {
        [Key]
        public int HistoryID { get; set; }

        public int AccountID { get; set; }

        public int SongID { get; set; }

        public DateTime? ListenDate { get; set; }

        public virtual Account Account { get; set; }

        public virtual Song Song { get; set; }
    }
}
