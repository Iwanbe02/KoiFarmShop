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

    public virtual DbSet<CartItem> CartItems { get; set; }

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
            entity.HasKey(e => e.Id).HasName("PK__Account__3214EC07856A17F3");

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
            entity.HasKey(e => e.Id).HasName("PK__Cart__3214EC07BBA50D1A");

            entity.ToTable("Cart");

            entity.Property(e => e.AccountId).HasColumnName("Account_Id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Account).WithMany(p => p.Carts)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__Cart__Account_Id__5DCAEF64");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CartItem__3214EC07DD92E500");

            entity.ToTable("CartItem");

            entity.Property(e => e.CartId).HasColumnName("Cart_Id");
            entity.Property(e => e.ConsignmentId).HasColumnName("Consignment_Id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.KoiFishId).HasColumnName("KoiFish_Id");
            entity.Property(e => e.KoiFishyId).HasColumnName("KoiFishy_Id");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Cart).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.CartId)
                .HasConstraintName("FK__CartItem__Cart_I__619B8048");

            entity.HasOne(d => d.Consignment).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.ConsignmentId)
                .HasConstraintName("FK__CartItem__Consig__6477ECF3");

            entity.HasOne(d => d.KoiFish).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.KoiFishId)
                .HasConstraintName("FK__CartItem__KoiFis__628FA481");

            entity.HasOne(d => d.KoiFishy).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.KoiFishyId)
                .HasConstraintName("FK__CartItem__KoiFis__6383C8BA");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC07342F1A57");

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
            entity.HasKey(e => e.Id).HasName("PK__Consignm__3214EC07A93F8702");

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
                .HasConstraintName("FK__Consignme__Accou__5070F446");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Feedback__3214EC072BDEBD0F");

            entity.ToTable("Feedback");

            entity.HasIndex(e => e.OrderId, "UQ__Feedback__F1E4607ADEF5240A").IsUnique();

            entity.Property(e => e.AccountId).HasColumnName("Account_Id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.OrderId).HasColumnName("Order_Id");

            entity.HasOne(d => d.Account).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__Feedback__Accoun__6FE99F9F");

            entity.HasOne(d => d.Order).WithOne(p => p.Feedback)
                .HasForeignKey<Feedback>(d => d.OrderId)
                .HasConstraintName("FK__Feedback__Order___70DDC3D8");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Image__3214EC07EE12D3A9");

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
            entity.Property(e => e.OriginCertificateId).HasColumnName("OriginCertificate_Id");

            entity.HasOne(d => d.Consignment).WithMany(p => p.Images)
                .HasForeignKey(d => d.ConsignmentId)
                .HasConstraintName("FK__Image__Consignme__59063A47");

            entity.HasOne(d => d.KoiFishy).WithMany(p => p.Images)
                .HasForeignKey(d => d.KoiFishyId)
                .HasConstraintName("FK__Image__KoiFishy___5812160E");

            entity.HasOne(d => d.Koi).WithMany(p => p.Images)
                .HasForeignKey(d => d.KoiId)
                .HasConstraintName("FK__Image__Koi_Id__571DF1D5");

            entity.HasOne(d => d.OriginCertificate).WithMany(p => p.Images)
                .HasForeignKey(d => d.OriginCertificateId)
                .HasConstraintName("FK__Image__OriginCer__59FA5E80");
        });

        modelBuilder.Entity<KoiFish>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__KoiFish__3214EC073885037B");

            entity.ToTable("KoiFish");

            entity.HasIndex(e => e.OriginCertificateId, "UQ__KoiFish__3BD70FD41C0D12B5").IsUnique();

            entity.Property(e => e.CategoryId).HasColumnName("Category_Id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.OriginCertificateId).HasColumnName("OriginCertificate_Id");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Category).WithMany(p => p.KoiFishes)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__KoiFish__Categor__47DBAE45");

            entity.HasOne(d => d.OriginCertificate).WithOne(p => p.KoiFish)
                .HasForeignKey<KoiFish>(d => d.OriginCertificateId)
                .HasConstraintName("FK__KoiFish__OriginC__48CFD27E");
        });

        modelBuilder.Entity<KoiFishy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__KoiFishy__3214EC072EB4C6B2");

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
                .HasConstraintName("FK__KoiFishy__Catego__4CA06362");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order__3214EC07BD8F84D9");

            entity.ToTable("Order");

            entity.HasIndex(e => e.CartId, "UQ__Order__D6AB47586222AF06").IsUnique();

            entity.Property(e => e.AccountId).HasColumnName("Account_Id");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.CartId).HasColumnName("Cart_Id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentId).HasColumnName("Payment_Id");
            entity.Property(e => e.Phone).HasMaxLength(255);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Account).WithMany(p => p.Orders)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__Order__Account_I__6A30C649");

            entity.HasOne(d => d.Cart).WithOne(p => p.Order)
                .HasForeignKey<Order>(d => d.CartId)
                .HasConstraintName("FK__Order__Cart_Id__693CA210");

            entity.HasOne(d => d.Payment).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PaymentId)
                .HasConstraintName("FK__Order__Payment_I__6B24EA82");
        });

        modelBuilder.Entity<OriginCertificate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OriginCe__3214EC07A66AAF69");

            entity.ToTable("OriginCertificate");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Variety).HasMaxLength(255);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payment__3214EC07FCC3BFFE");

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
            entity.HasKey(e => e.Id).HasName("PK__Promotio__3214EC0712A0A15A");

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
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC07DD98C869");

            entity.ToTable("Role");

            entity.Property(e => e.Role1).HasColumnName("Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
