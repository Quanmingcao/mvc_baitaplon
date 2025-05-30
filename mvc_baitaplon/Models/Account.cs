namespace mvc_baitaplon.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Account
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Account()
        {
            Collections = new HashSet<Collection>();
            CollectionLibraries = new HashSet<CollectionLibrary>();
            Likes = new HashSet<Like>();
            ListeningHistories = new HashSet<ListeningHistory>();
            Reports = new HashSet<Report>();
            Reports1 = new HashSet<Report>();
            Songs = new HashSet<Song>();
            Accounts1 = new HashSet<Account>();
            Accounts = new HashSet<Account>();
        }

        public int AccountID { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(255)]
        public string Fullname { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(255)]
        public string PasswordHash { get; set; }

        public bool? Role { get; set; }

        [StringLength(255)]
        public string ProfileImage { get; set; }

        public DateTime? CreatedAt { get; set; }

        public bool? IsLocked { get; set; }

        public bool? IsDeleted { get; set; }

        public int? reportcount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Collection> Collections { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CollectionLibrary> CollectionLibraries { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Like> Likes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ListeningHistory> ListeningHistories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Report> Reports { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Report> Reports1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Song> Songs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Account> Accounts1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
