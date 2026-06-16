using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VideoSummarization.Models
{
    public partial class VideoSummDBContext : DbContext
    {
        public VideoSummDBContext()
        {
        }

        public VideoSummDBContext(DbContextOptions<VideoSummDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Exam> Exams { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=VideoSummDB;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exam>(entity =>
            {
                entity.ToTable("Exam");

                entity.Property(e => e.DateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("dateTime");

                entity.Property(e => e.Result)
                    .HasMaxLength(50)
                    .HasColumnName("result");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.Video)
                    .HasMaxLength(50)
                    .HasColumnName("video");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Exams)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Exam_User");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .HasColumnName("phone");

                entity.Property(e => e.RoleId).HasColumnName("roleID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
