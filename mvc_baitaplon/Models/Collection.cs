namespace mvc_baitaplon.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Collection
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Collection()
        {
            CollectionLibraries = new HashSet<CollectionLibrary>();
            CollectionSongs = new HashSet<CollectionSong>();
            Reports = new HashSet<Report>();
        }

        public int CollectionID { get; set; }

        public int AccountID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int TypeID { get; set; }

        [StringLength(255)]
        public string CoverImage { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public DateTime? CreatedAt { get; set; }

        public bool? IsDeleted { get; set; }

        public bool? IsReported { get; set; }

        public bool? IsPublic { get; set; }

        public bool? IsLocked { get; set; }

        public int? reportcount { get; set; }

        public virtual Account Account { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CollectionLibrary> CollectionLibraries { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CollectionSong> CollectionSongs { get; set; }

        public virtual CollectionType CollectionType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Report> Reports { get; set; }
    }
}
