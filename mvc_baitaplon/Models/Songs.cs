namespace mvc_baitaplon.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Songs
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Songs()
        {
            ListeningHistory = new HashSet<ListeningHistory>();
            ListeningHistory1 = new HashSet<ListeningHistory>();
            ListeningHistory2 = new HashSet<ListeningHistory>();
            PlaylistSongs = new HashSet<PlaylistSongs>();
            PlaylistSongs1 = new HashSet<PlaylistSongs>();
            PlaylistSongs2 = new HashSet<PlaylistSongs>();
        }

        [Key]
        public int SongID { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public int ArtistID { get; set; }

        public int? AlbumID { get; set; }

        public int? GenreID { get; set; }

        public TimeSpan? Duration { get; set; }

        [StringLength(255)]
        public string FilePath { get; set; }

        public DateTime? UploadDate { get; set; }

        public int? Views { get; set; }

        public virtual Albums Albums { get; set; }

        public virtual Albums Albums1 { get; set; }

        public virtual Albums Albums2 { get; set; }

        public virtual Artists Artists { get; set; }

        public virtual Artists Artists1 { get; set; }

        public virtual Artists Artists2 { get; set; }

        public virtual Genres Genres { get; set; }

        public virtual Genres Genres1 { get; set; }

        public virtual Genres Genres2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ListeningHistory> ListeningHistory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ListeningHistory> ListeningHistory1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ListeningHistory> ListeningHistory2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlaylistSongs> PlaylistSongs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlaylistSongs> PlaylistSongs1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlaylistSongs> PlaylistSongs2 { get; set; }
    }
}
