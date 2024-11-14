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

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Consignment> Consignments { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<KoiFish> KoiFishes { get; set; }

    public virtual DbSet<KoiFishy> KoiFishies { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OriginCertificate> OriginCertificates { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);uid=sa;pwd=12345;database=KoiFarmShop;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Account__3214EC0712582A58");

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

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cart__3214EC071761FF60");

            entity.ToTable("Cart");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.KoiFishyId).HasColumnName("KoiFishy_Id");
            entity.Property(e => e.KoiId).HasColumnName("Koi_Id");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.KoiFishy).WithMany(p => p.Carts)
                .HasForeignKey(d => d.KoiFishyId)
                .HasConstraintName("FK__Cart__KoiFishy_I__6383C8BA");

            entity.HasOne(d => d.Koi).WithMany(p => p.Carts)
                .HasForeignKey(d => d.KoiId)
                .HasConstraintName("FK__Cart__Koi_Id__628FA481");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC0796E4A0FD");

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
            entity.HasKey(e => e.Id).HasName("PK__Consignm__3214EC07F3255411");

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
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Account).WithMany(p => p.Consignments)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__Consignme__Accou__4BAC3F29");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Feedback__3214EC070194FCC7");

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
                .HasConstraintName("FK__Feedback__Accoun__571DF1D5");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Image__3214EC0792D31BB8");

            entity.ToTable("Image");

            entity.HasIndex(e => e.OriginCertificateId, "UQ__Image__3BD70FD475038911").IsUnique();

            entity.Property(e => e.ConsignmentId).HasColumnName("Consignment_Id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.KoiFishyId).HasColumnName("KoiFishy_Id");
            entity.Property(e => e.KoiId).HasColumnName("Koi_Id");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.OriginCertificateId).HasColumnName("OriginCertificate_Id");

            entity.HasOne(d => d.Consignment).WithMany(p => p.Images)
                .HasForeignKey(d => d.ConsignmentId)
                .HasConstraintName("FK__Image__Consignme__5DCAEF64");

            entity.HasOne(d => d.KoiFishy).WithMany(p => p.Images)
                .HasForeignKey(d => d.KoiFishyId)
                .HasConstraintName("FK__Image__KoiFishy___5CD6CB2B");

            entity.HasOne(d => d.Koi).WithMany(p => p.Images)
                .HasForeignKey(d => d.KoiId)
                .HasConstraintName("FK__Image__Koi_Id__5BE2A6F2");

            entity.HasOne(d => d.OriginCertificate).WithOne(p => p.Image)
                .HasForeignKey<Image>(d => d.OriginCertificateId)
                .HasConstraintName("FK__Image__OriginCer__5EBF139D");
        });

        modelBuilder.Entity<KoiFish>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__KoiFish__3214EC072C82F3AD");

            entity.ToTable("KoiFish");

            entity.Property(e => e.CategoryId).HasColumnName("Category_Id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Category).WithMany(p => p.KoiFishes)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__KoiFish__Categor__440B1D61");
        });

        modelBuilder.Entity<KoiFishy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__KoiFishy__3214EC0761054576");

            entity.ToTable("KoiFishy");

            entity.Property(e => e.CategoryId).HasColumnName("Category_Id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Category).WithMany(p => p.KoiFishies)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__KoiFishy__Catego__47DBAE45");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order__3214EC07074AF27C");

            entity.ToTable("Order");

            entity.HasIndex(e => e.AccountId, "UQ__Order__B19E45E8722EBBAA").IsUnique();

            entity.HasIndex(e => e.CartId, "UQ__Order__D6AB475879BA5A41").IsUnique();

            entity.HasIndex(e => e.PaymentId, "UQ__Order__DA6C7FC049C2FA84").IsUnique();

            entity.Property(e => e.AccountId).HasColumnName("Account_Id");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.CartId).HasColumnName("Cart_Id");
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
            entity.Property(e => e.Phone).HasMaxLength(255);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Account).WithOne(p => p.Order)
                .HasForeignKey<Order>(d => d.AccountId)
                .HasConstraintName("FK__Order__Account_I__6E01572D");

            entity.HasOne(d => d.Cart).WithOne(p => p.Order)
                .HasForeignKey<Order>(d => d.CartId)
                .HasConstraintName("FK__Order__Cart_Id__6A30C649");

            entity.HasOne(d => d.Consignment).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ConsignmentId)
                .HasConstraintName("FK__Order__Consignme__6D0D32F4");

            entity.HasOne(d => d.KoiFishy).WithMany(p => p.Orders)
                .HasForeignKey(d => d.KoiFishyId)
                .HasConstraintName("FK__Order__KoiFishy___6C190EBB");

            entity.HasOne(d => d.Koi).WithMany(p => p.Orders)
                .HasForeignKey(d => d.KoiId)
                .HasConstraintName("FK__Order__Koi_Id__6B24EA82");

            entity.HasOne(d => d.Payment).WithOne(p => p.Order)
                .HasForeignKey<Order>(d => d.PaymentId)
                .HasConstraintName("FK__Order__Payment_I__6EF57B66");
        });

        modelBuilder.Entity<OriginCertificate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OriginCe__3214EC0731A5FD89");

            entity.ToTable("OriginCertificate");

            entity.HasIndex(e => e.KoiId, "UQ__OriginCe__008F96F0AC0878D2").IsUnique();

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.KoiId).HasColumnName("Koi_Id");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Variety).HasMaxLength(255);

            entity.HasOne(d => d.Koi).WithOne(p => p.OriginCertificate)
                .HasForeignKey<OriginCertificate>(d => d.KoiId)
                .HasConstraintName("FK__OriginCer__Koi_I__5070F446");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payment__3214EC07BE587057");

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
            entity.HasKey(e => e.Id).HasName("PK__Promotio__3214EC074E8419B4");

            entity.ToTable("Promotion");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC079DE10428");

            entity.ToTable("Role");

            entity.Property(e => e.Role1).HasColumnName("Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
