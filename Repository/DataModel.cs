using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CMCS.Repository;

public partial class DataModel : DbContext
{
    public DataModel()
    {
    }

    public DataModel(DbContextOptions<DataModel> options)
        : base(options)
    {
    }

    public virtual DbSet<Claim> Claims { get; set; }

    public virtual DbSet<ClaimSupportingDocument> ClaimSupportingDocuments { get; set; }

    public virtual DbSet<SystemUser> SystemUsers { get; set; }

    public virtual DbSet<SystemUserRole> SystemUserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=TROYLAPTOP\\SQLEXPRESS;Database=CMCS;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Claim>(entity =>
        {
            entity.HasKey(e => e.ClaimId).HasName("PK__Claim__EF2E13BB991DFFD0");

            entity.ToTable("Claim");

            entity.Property(e => e.ClaimId).HasColumnName("ClaimID");
            entity.Property(e => e.ApprovedByProgrammeManagerId).HasColumnName("ApprovedByProgrammeManagerID");
            entity.Property(e => e.ClaimDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.HourlyRate).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.LecturerId).HasColumnName("LecturerID");
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .HasDefaultValue("Pending");

            entity.HasOne(d => d.ApprovedByProgrammeManager).WithMany(p => p.ClaimApprovedByProgrammeManagers)
                .HasForeignKey(d => d.ApprovedByProgrammeManagerId)
                .HasConstraintName("FK_Claim_Manager");

            entity.HasOne(d => d.Lecturer).WithMany(p => p.ClaimLecturers)
                .HasForeignKey(d => d.LecturerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Claim_Lecturer");
        });

        modelBuilder.Entity<SystemUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__SystemUs__1788CCAC6087847F");

            entity.HasIndex(e => e.Email, "UQ__SystemUs__A9D105347F3A86A9").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
        });

        modelBuilder.Entity<SystemUserRole>(entity =>
        {
            entity.HasKey(e => e.SystemUserRoleId).HasName("PK__SystemUs__B40A0815E3D6ACF8");

            entity.Property(e => e.RoleTitle).HasMaxLength(150);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
