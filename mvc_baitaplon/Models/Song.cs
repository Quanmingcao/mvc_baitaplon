namespace mvc_baitaplon.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Song
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Song()
        {
            CollectionSongs = new HashSet<CollectionSong>();
            Likes = new HashSet<Like>();
            ListeningHistories = new HashSet<ListeningHistory>();
            Reports = new HashSet<Report>();
        }

        public int SongID { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public int? GenreID { get; set; }

        public int? Duration { get; set; }

        [StringLength(255)]
        public string CoverImage { get; set; }

        [Required]
        [StringLength(255)]
        public string FilePath { get; set; }

        public DateTime? UploadDate { get; set; }

        public int? Views { get; set; }

        public int AccountID { get; set; }

        public bool? IsPublic { get; set; }

        public bool? IsDeleted { get; set; }

        public bool? IsLocked { get; set; }

        public int? reportcount { get; set; }

        public virtual Account Account { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CollectionSong> CollectionSongs { get; set; }

        public virtual Genre Genre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Like> Likes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ListeningHistory> ListeningHistories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Report> Reports { get; set; }
    }
}
