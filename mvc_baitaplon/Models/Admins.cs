namespace mvc_baitaplon.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Admins
    {
        [Key]
        public int AdminID { get; set; }

        public int AccountID { get; set; }

        [StringLength(50)]
        public string Role { get; set; }

        public DateTime? CreatedAt { get; set; }

        public virtual Accounts Accounts { get; set; }

        public virtual Accounts Accounts1 { get; set; }

        public virtual Accounts Accounts2 { get; set; }
    }
}
