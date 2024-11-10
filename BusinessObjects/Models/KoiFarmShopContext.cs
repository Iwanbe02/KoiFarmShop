using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BusinessObjects.Models;

public partial class KoiFarmShopContext : DbContext
{
    public KoiFarmShopContext()
    {
    }

    public KoiFarmShopContext(DbContextOptions<KoiFarmShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Consignment> Consignments { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<HealthCertificate> HealthCertificates { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<KoiFish> KoiFishes { get; set; }

    public virtual DbSet<KoiFishy> KoiFishies { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OriginCertificate> OriginCertificates { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<RewardCertificate> RewardCertificates { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);uid=sa;pwd=12345;database=KoiFarmShop;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Account__3214EC072FD15FB8");

            entity.ToTable("Account");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Email).IsUnicode(false);
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Password).IsUnicode(false);
            entity.Property(e => e.Phone).IsUnicode(false);
            entity.Property(e => e.RoleId).HasColumnName("Role_Id");

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Account__Role_Id__3D5E1FD2");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC07D21D1C03");

            entity.ToTable("Category");

            entity.Property(e => e.Category1).HasColumnName("Category");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Consignment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Consignm__3214EC0708C041A5");

            entity.ToTable("Consignment");

            entity.Property(e => e.AccountId).HasColumnName("Account_Id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.KoiCode)
                .HasMaxLength(10)
                .HasColumnName("Koi_Code");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Account).WithMany(p => p.Consignments)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__Consignme__Accou__5DCAEF64");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Feedback__3214EC076EDF8748");

            entity.ToTable("Feedback");

            entity.Property(e => e.AccountId).HasColumnName("Account_Id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Account).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__Feedback__Accoun__59FA5E80");
        });

        modelBuilder.Entity<HealthCertificate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HealthCe__3214EC07D45911A2");

            entity.ToTable("HealthCertificate");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.KoiId).HasColumnName("Koi_Id");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.OrderId).HasColumnName("Order_Id");
            entity.Property(e => e.StartTime).HasColumnType("datetime");

            entity.HasOne(d => d.Koi).WithMany(p => p.HealthCertificates)
                .HasForeignKey(d => d.KoiId)
                .HasConstraintName("FK__HealthCer__Koi_I__4BAC3F29");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Image__3214EC076706F908");

            entity.ToTable("Image");

            entity.Property(e => e.ConsignmentId).HasColumnName("Consignment_Id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.KoiFishyId).HasColumnName("KoiFishy_Id");
            entity.Property(e => e.KoiId).HasColumnName("Koi_Id");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Consignment).WithMany(p => p.Images)
                .HasForeignKey(d => d.ConsignmentId)
                .HasConstraintName("FK__Image__Consignme__6383C8BA");

            entity.HasOne(d => d.KoiFishy).WithMany(p => p.Images)
                .HasForeignKey(d => d.KoiFishyId)
                .HasConstraintName("FK__Image__KoiFishy___628FA481");

            entity.HasOne(d => d.Koi).WithMany(p => p.Images)
                .HasForeignKey(d => d.KoiId)
                .HasConstraintName("FK__Image__Koi_Id__619B8048");
        });

        modelBuilder.Entity<KoiFish>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__KoiFish__3214EC0707D611AF");

            entity.ToTable("KoiFish");

            entity.Property(e => e.CategoryId).HasColumnName("Category_Id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Category).WithMany(p => p.KoiFishes)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__KoiFish__Categor__440B1D61");
        });

        modelBuilder.Entity<KoiFishy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__KoiFishy__3214EC074B1654B0");

            entity.ToTable("KoiFishy");

            entity.Property(e => e.CategoryId).HasColumnName("Category_Id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Category).WithMany(p => p.KoiFishies)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__KoiFishy__Catego__47DBAE45");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order__3214EC076A528EC2");

            entity.ToTable("Order");

            entity.Property(e => e.AccountId).HasColumnName("Account_Id");
            entity.Property(e => e.ConsignmentId).HasColumnName("Consignment_Id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.KoiFishyId).HasColumnName("KoiFishy_Id");
            entity.Property(e => e.KoiId).HasColumnName("Koi_Id");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentId).HasColumnName("Payment_Id");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Account).WithMany(p => p.Orders)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__Order__Account_I__6A30C649");

            entity.HasOne(d => d.Consignment).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ConsignmentId)
                .HasConstraintName("FK__Order__Consignme__693CA210");

            entity.HasOne(d => d.KoiFishy).WithMany(p => p.Orders)
                .HasForeignKey(d => d.KoiFishyId)
                .HasConstraintName("FK__Order__KoiFishy___68487DD7");

            entity.HasOne(d => d.Koi).WithMany(p => p.Orders)
                .HasForeignKey(d => d.KoiId)
                .HasConstraintName("FK__Order__Koi_Id__6754599E");

            entity.HasOne(d => d.Payment).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PaymentId)
                .HasConstraintName("FK__Order__Payment_I__6B24EA82");
        });

        modelBuilder.Entity<OriginCertificate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OriginCe__3214EC07AC1B34AA");

            entity.ToTable("OriginCertificate");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.KoiId).HasColumnName("Koi_Id");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.OrderId).HasColumnName("Order_Id");
            entity.Property(e => e.StartTime).HasColumnType("datetime");

            entity.HasOne(d => d.Koi).WithMany(p => p.OriginCertificates)
                .HasForeignKey(d => d.KoiId)
                .HasConstraintName("FK__OriginCer__Koi_I__4F7CD00D");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payment__3214EC0727E3CD37");

            entity.ToTable("Payment");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Promotio__3214EC07D4FC450D");

            entity.ToTable("Promotion");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<RewardCertificate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RewardCe__3214EC0787CC417D");

            entity.ToTable("RewardCertificate");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.KoiId).HasColumnName("Koi_Id");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.OrderId).HasColumnName("Order_Id");

            entity.HasOne(d => d.Koi).WithMany(p => p.RewardCertificates)
                .HasForeignKey(d => d.KoiId)
                .HasConstraintName("FK__RewardCer__Koi_I__534D60F1");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC07DD57536A");

            entity.ToTable("Role");

            entity.Property(e => e.Role1).HasColumnName("Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
