namespace mvc_baitaplon.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CollectionLibrary")]
    public partial class CollectionLibrary
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AccountID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CollectionID { get; set; }

        public DateTime? SavedAt { get; set; }

        public virtual Account Account { get; set; }

        public virtual Collection Collection { get; set; }
    }
}
