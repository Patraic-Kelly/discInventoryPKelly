using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DiscInventory.Models
{
    public partial class discInventoryPKContext : DbContext
    {
        public discInventoryPKContext()
        {
        }

        public discInventoryPKContext(DbContextOptions<discInventoryPKContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<ArtistType> ArtistTypes { get; set; }
        public virtual DbSet<Borrower> Borrowers { get; set; }
        public virtual DbSet<Disc> Discs { get; set; }
        public virtual DbSet<DiscHasArtist> DiscHasArtists { get; set; }
        public virtual DbSet<DiscStatus> DiscStatuses { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<MediaType> MediaTypes { get; set; }
        public virtual DbSet<Rental> Rentals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLDEV01;Database=discInventoryPK;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Artist>(entity =>
            {
                entity.ToTable("Artist");

                entity.Property(e => e.ArtistId).HasColumnName("ArtistID");

                entity.Property(e => e.ArtistLastName)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.ArtistName)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.ArtistTypeId).HasColumnName("ArtistTypeID");

                entity.HasOne(d => d.ArtistType)
                    .WithMany(p => p.Artists)
                    .HasForeignKey(d => d.ArtistTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Artist__ArtistTy__300424B4");
            });

            modelBuilder.Entity<ArtistType>(entity =>
            {
                entity.ToTable("ArtistType");

                entity.Property(e => e.ArtistTypeId).HasColumnName("ArtistTypeID");

                entity.Property(e => e.ArtistTypeDesc)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Borrower>(entity =>
            {
                entity.ToTable("Borrower");

                entity.Property(e => e.BorrowerId).HasColumnName("BorrowerID");

                entity.Property(e => e.Email)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.FName)
                    .IsRequired()
                    .HasMaxLength(80)
                    .HasColumnName("fName");

                entity.Property(e => e.LName)
                    .IsRequired()
                    .HasMaxLength(80)
                    .HasColumnName("lName");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Disc>(entity =>
            {
                entity.ToTable("Disc");

                entity.Property(e => e.DiscId).HasColumnName("DiscID");

                entity.Property(e => e.DiscName)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.GenreId).HasColumnName("GenreID");

                entity.Property(e => e.MediaId).HasColumnName("MediaID");

                entity.Property(e => e.ReleaseDate).HasColumnType("date");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Discs)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Disc__GenreID__2B3F6F97");

                entity.HasOne(d => d.Media)
                    .WithMany(p => p.Discs)
                    .HasForeignKey(d => d.MediaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Disc__MediaID__2A4B4B5E");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Discs)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Disc__StatusID__29572725");
            });

            modelBuilder.Entity<DiscHasArtist>(entity =>
            {
                entity.ToTable("DiscHasArtist");

                entity.Property(e => e.DiscHasArtistId).HasColumnName("DiscHasArtistID");

                entity.Property(e => e.ArtistId).HasColumnName("ArtistID");

                entity.Property(e => e.DiscId).HasColumnName("DiscID");

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.DiscHasArtists)
                    .HasForeignKey(d => d.ArtistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DiscHasAr__Artis__33D4B598");

                entity.HasOne(d => d.Disc)
                    .WithMany(p => p.DiscHasArtists)
                    .HasForeignKey(d => d.DiscId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DiscHasAr__DiscI__32E0915F");
            });

            modelBuilder.Entity<DiscStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId)
                    .HasName("PK__DiscStat__C8EE2043E22BE082");

                entity.ToTable("DiscStatus");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.StatusDesc)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("Genre");

                entity.Property(e => e.GenreId).HasColumnName("GenreID");

                entity.Property(e => e.GenreDesc)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MediaType>(entity =>
            {
                entity.HasKey(e => e.MediaId)
                    .HasName("PK__MediaTyp__B2C2B5AF9BF96111");

                entity.ToTable("MediaType");

                entity.Property(e => e.MediaId).HasColumnName("MediaID");

                entity.Property(e => e.MediaDesc)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Rental>(entity =>
            {
                entity.ToTable("Rental");

                entity.Property(e => e.RentalId).HasColumnName("RentalID");

                entity.Property(e => e.BorrowerId).HasColumnName("BorrowerID");

                entity.Property(e => e.DiscId).HasColumnName("DiscID");

                entity.Property(e => e.DueDate).HasDefaultValueSql("(getdate()+(30))");

                entity.HasOne(d => d.Borrower)
                    .WithMany(p => p.Rentals)
                    .HasForeignKey(d => d.BorrowerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Rental__Borrower__398D8EEE");

                entity.HasOne(d => d.Disc)
                    .WithMany(p => p.Rentals)
                    .HasForeignKey(d => d.DiscId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Rental__DiscID__3A81B327");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
