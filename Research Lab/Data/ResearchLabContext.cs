using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Research_Lab.Models;

namespace Research_Lab.Data
{
    public partial class ResearchLabContext : DbContext
    {
        public ResearchLabContext()
        {
        }

        public ResearchLabContext(DbContextOptions<ResearchLabContext> options)
            : base(options)
        {
        }

        public  DbSet<AppUser> AppUser { get; set; }
        public  DbSet<BookingInfo> BookingInfo { get; set; }
        public  DbSet<Computer> Computer { get; set; }
        public  DbSet<ResearchLab> ResearchLab { get; set; }
        public  DbSet<UserRole> UserRole { get; set; }

        public  DbSet<LabUseCost> LabUseCosts { get; set; }

        public  DbSet<LabCostRate> LabCostRates { get; set; }

       /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.Property(e => e.AppUserId).HasColumnName("AppUserID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);


                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AppUser)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AppUser__RoleId__398D8EEE");
            });

            modelBuilder.Entity<BookingInfo>(entity =>
            {
                entity.HasKey(e => e.Biid)
                    .HasName("PK__BookingI__3B3C5D092A8FEB3E");

                entity.Property(e => e.Biid).HasColumnName("BIID");

                entity.Property(e => e.AppUserId).HasColumnName("AppUserID");

                entity.Property(e => e.BookingDate).HasColumnType("date");

                entity.Property(e => e.Cid).HasColumnName("CID");

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.BookingInfo)
                    .HasForeignKey(d => d.AppUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BookingIn__AppUs__48CFD27E");

                entity.HasOne(d => d.C)
                    .WithMany(p => p.BookingInfo)
                    .HasForeignKey(d => d.Cid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BookingInfo__CID__47DBAE45");
            });


            

            modelBuilder.Entity<Computer>(entity =>
            {
                entity.HasKey(e => e.Cid)
                    .HasName("PK__Computer__C1F8DC5982FDED25");

                entity.Property(e => e.Cid).HasColumnName("CID");

                entity.Property(e => e.LabId).HasColumnName("LabID");

                entity.HasOne(d => d.Lab)
                    .WithMany(p => p.Computer)
                    .HasForeignKey(d => d.LabId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Computer__LabID__44FF419A");
            });

            modelBuilder.Entity<ResearchLab>(entity =>
            {
                entity.HasKey(e => e.Rlid)
                    .HasName("PK__Research__478CAB7599BC5AF0");

                entity.Property(e => e.Rlid).HasColumnName("RLID");

                entity.Property(e => e.LabLoction)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LabName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.LabAssistantNavigation)
                    .WithMany(p => p.ResearchLab)
                    .HasForeignKey(d => d.LabAssistant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ResearchL__LabAs__3F466844");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK__UserRole__CD98462A570AAC4C");

                entity.Property(e => e.RoleId).HasColumnName("roleId");

                entity.Property(e => e.RoleType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    
        */
    }
}
