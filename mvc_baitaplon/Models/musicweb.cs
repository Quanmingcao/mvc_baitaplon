using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace mvc_baitaplon.Models
{
    public partial class musicweb : DbContext
    {
        public musicweb()
            : base("name=musicweb")
        {
        }

        public virtual DbSet<Accounts> Accounts { get; set; }
        public virtual DbSet<Admins> Admins { get; set; }
        public virtual DbSet<Albums> Albums { get; set; }
        public virtual DbSet<Artists> Artists { get; set; }
        public virtual DbSet<Genres> Genres { get; set; }
        public virtual DbSet<ListeningHistory> ListeningHistory { get; set; }
        public virtual DbSet<Playlists> Playlists { get; set; }
        public virtual DbSet<PlaylistSongs> PlaylistSongs { get; set; }
        public virtual DbSet<Songs> Songs { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accounts>()
                .HasMany(e => e.Admins)
                .WithRequired(e => e.Accounts)
                .HasForeignKey(e => e.AccountID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Accounts>()
                .HasMany(e => e.Admins1)
                .WithRequired(e => e.Accounts1)
                .HasForeignKey(e => e.AccountID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Accounts>()
                .HasMany(e => e.Admins2)
                .WithRequired(e => e.Accounts2)
                .HasForeignKey(e => e.AccountID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Accounts>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Accounts)
                .HasForeignKey(e => e.AccountID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Accounts>()
                .HasMany(e => e.Users1)
                .WithRequired(e => e.Accounts1)
                .HasForeignKey(e => e.AccountID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Accounts>()
                .HasMany(e => e.Users2)
                .WithRequired(e => e.Accounts2)
                .HasForeignKey(e => e.AccountID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Albums>()
                .HasMany(e => e.Songs)
                .WithOptional(e => e.Albums)
                .HasForeignKey(e => e.AlbumID);

            modelBuilder.Entity<Albums>()
                .HasMany(e => e.Songs1)
                .WithOptional(e => e.Albums1)
                .HasForeignKey(e => e.AlbumID);

            modelBuilder.Entity<Albums>()
                .HasMany(e => e.Songs2)
                .WithOptional(e => e.Albums2)
                .HasForeignKey(e => e.AlbumID);

            modelBuilder.Entity<Artists>()
                .HasMany(e => e.Albums)
                .WithRequired(e => e.Artists)
                .HasForeignKey(e => e.ArtistID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Artists>()
                .HasMany(e => e.Albums1)
                .WithRequired(e => e.Artists1)
                .HasForeignKey(e => e.ArtistID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Artists>()
                .HasMany(e => e.Albums2)
                .WithRequired(e => e.Artists2)
                .HasForeignKey(e => e.ArtistID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Artists>()
                .HasMany(e => e.Songs)
                .WithRequired(e => e.Artists)
                .HasForeignKey(e => e.ArtistID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Artists>()
                .HasMany(e => e.Songs1)
                .WithRequired(e => e.Artists1)
                .HasForeignKey(e => e.ArtistID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Artists>()
                .HasMany(e => e.Songs2)
                .WithRequired(e => e.Artists2)
                .HasForeignKey(e => e.ArtistID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Genres>()
                .HasMany(e => e.Songs)
                .WithOptional(e => e.Genres)
                .HasForeignKey(e => e.GenreID);

            modelBuilder.Entity<Genres>()
                .HasMany(e => e.Songs1)
                .WithOptional(e => e.Genres1)
                .HasForeignKey(e => e.GenreID);

            modelBuilder.Entity<Genres>()
                .HasMany(e => e.Songs2)
                .WithOptional(e => e.Genres2)
                .HasForeignKey(e => e.GenreID);

            modelBuilder.Entity<Playlists>()
                .HasMany(e => e.PlaylistSongs)
                .WithRequired(e => e.Playlists)
                .HasForeignKey(e => e.PlaylistID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Playlists>()
                .HasMany(e => e.PlaylistSongs1)
                .WithRequired(e => e.Playlists1)
                .HasForeignKey(e => e.PlaylistID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Playlists>()
                .HasMany(e => e.PlaylistSongs2)
                .WithRequired(e => e.Playlists2)
                .HasForeignKey(e => e.PlaylistID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Songs>()
                .HasMany(e => e.ListeningHistory)
                .WithRequired(e => e.Songs)
                .HasForeignKey(e => e.SongID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Songs>()
                .HasMany(e => e.ListeningHistory1)
                .WithRequired(e => e.Songs1)
                .HasForeignKey(e => e.SongID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Songs>()
                .HasMany(e => e.ListeningHistory2)
                .WithRequired(e => e.Songs2)
                .HasForeignKey(e => e.SongID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Songs>()
                .HasMany(e => e.PlaylistSongs)
                .WithRequired(e => e.Songs)
                .HasForeignKey(e => e.SongID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Songs>()
                .HasMany(e => e.PlaylistSongs1)
                .WithRequired(e => e.Songs1)
                .HasForeignKey(e => e.SongID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Songs>()
                .HasMany(e => e.PlaylistSongs2)
                .WithRequired(e => e.Songs2)
                .HasForeignKey(e => e.SongID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.ListeningHistory)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.ListeningHistory1)
                .WithRequired(e => e.Users1)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.ListeningHistory2)
                .WithRequired(e => e.Users2)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Playlists)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Playlists1)
                .WithRequired(e => e.Users1)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Playlists2)
                .WithRequired(e => e.Users2)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);
        }
    }
}
