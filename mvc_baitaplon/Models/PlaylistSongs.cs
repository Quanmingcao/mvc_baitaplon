namespace mvc_baitaplon.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PlaylistSongs
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PlaylistID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SongID { get; set; }

        public virtual Playlists Playlists { get; set; }

        public virtual Playlists Playlists1 { get; set; }

        public virtual Playlists Playlists2 { get; set; }

        public virtual Songs Songs { get; set; }

        public virtual Songs Songs1 { get; set; }

        public virtual Songs Songs2 { get; set; }
    }
}
