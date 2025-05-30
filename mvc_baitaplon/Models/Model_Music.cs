using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace mvc_baitaplon.Models
{
    public partial class Model_Music : DbContext
    {
        public Model_Music()
            : base("name=Model_Music")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<CollectionLibrary> CollectionLibraries { get; set; }
        public virtual DbSet<Collection> Collections { get; set; }
        public virtual DbSet<CollectionSong> CollectionSongs { get; set; }
        public virtual DbSet<CollectionType> CollectionTypes { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Like> Likes { get; set; }
        public virtual DbSet<ListeningHistory> ListeningHistories { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<Song> Songs { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasMany(e => e.Collections)
                .WithRequired(e => e.Account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.CollectionLibraries)
                .WithRequired(e => e.Account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Likes)
                .WithRequired(e => e.Account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.ListeningHistories)
                .WithRequired(e => e.Account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Reports)
                .WithRequired(e => e.Account)
                .HasForeignKey(e => e.ReporterID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Reports1)
                .WithOptional(e => e.Account1)
                .HasForeignKey(e => e.ReportedAccountID);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Songs)
                .WithRequired(e => e.Account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Accounts1)
                .WithMany(e => e.Accounts)
                .Map(m => m.ToTable("Follows").MapLeftKey("FollowerID").MapRightKey("FollowingID"));

            modelBuilder.Entity<Collection>()
                .HasMany(e => e.CollectionLibraries)
                .WithRequired(e => e.Collection)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Collection>()
                .HasMany(e => e.CollectionSongs)
                .WithRequired(e => e.Collection)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Collection>()
                .HasMany(e => e.Reports)
                .WithOptional(e => e.Collection)
                .HasForeignKey(e => e.ReportedCollectionID);

            modelBuilder.Entity<CollectionType>()
                .HasMany(e => e.Collections)
                .WithRequired(e => e.CollectionType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Song>()
                .HasMany(e => e.CollectionSongs)
                .WithRequired(e => e.Song)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Song>()
                .HasMany(e => e.Likes)
                .WithRequired(e => e.Song)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Song>()
                .HasMany(e => e.ListeningHistories)
                .WithRequired(e => e.Song)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Song>()
                .HasMany(e => e.Reports)
                .WithOptional(e => e.Song)
                .HasForeignKey(e => e.ReportedSongID);
        }
    }
}
