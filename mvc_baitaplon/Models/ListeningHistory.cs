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

        public int UserID { get; set; }

        public int SongID { get; set; }

        public DateTime? ListenDate { get; set; }

        public virtual Songs Songs { get; set; }

        public virtual Songs Songs1 { get; set; }

        public virtual Songs Songs2 { get; set; }

        public virtual Users Users { get; set; }

        public virtual Users Users1 { get; set; }

        public virtual Users Users2 { get; set; }
    }
}
