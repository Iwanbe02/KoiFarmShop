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

    public virtual DbSet<Cartdetail> Cartdetails { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Consignment> Consignments { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<HealthCertificate> HealthCertificates { get; set; }

    public virtual DbSet<KoiFish> KoiFishes { get; set; }

    public virtual DbSet<KoiFish1> KoiFishes1 { get; set; }

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
            entity.HasKey(e => e.AccountId).HasName("account_account_id_primary");

            entity.ToTable("Account");

            entity.Property(e => e.AccountId).HasColumnName("account_Id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Gender)
                .HasMaxLength(255)
                .HasColumnName("gender");
            entity.Property(e => e.IsDelete)
                .IsRequired()
                .HasDefaultValueSql("('0')")
                .HasColumnName("isDelete");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .HasColumnName("phone");
            entity.Property(e => e.Point).HasColumnName("point");
            entity.Property(e => e.RoleId).HasColumnName("role_Id");

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("account_role_id_foreign");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("cart_cart_id_primary");

            entity.ToTable("Cart");

            entity.Property(e => e.CartId).HasColumnName("cart_id");
            entity.Property(e => e.AccountId).HasColumnName("account_Id");
            entity.Property(e => e.IsDelete)
                .IsRequired()
                .HasDefaultValueSql("('0')")
                .HasColumnName("isDelete");
            entity.Property(e => e.OrderId).HasColumnName("order_Id");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Order).WithMany(p => p.Carts)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cart_order_id_foreign");
        });

        modelBuilder.Entity<Cartdetail>(entity =>
        {
            entity.HasKey(e => e.CartDetailId).HasName("cartdetail_cartdetail_id_primary");

            entity.ToTable("Cartdetail");

            entity.Property(e => e.CartDetailId).HasColumnName("cartDetail_Id");
            entity.Property(e => e.CartId).HasColumnName("cart_Id");
            entity.Property(e => e.FeedbackId).HasColumnName("feedback_Id");
            entity.Property(e => e.IsDelete)
                .IsRequired()
                .HasDefaultValueSql("('0')")
                .HasColumnName("isDelete");
            entity.Property(e => e.KoiId).HasColumnName("koi_Id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.TotalPrice).HasColumnName("totalPrice");

            entity.HasOne(d => d.Cart).WithMany(p => p.Cartdetails)
                .HasForeignKey(d => d.CartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cartdetail_cart_id_foreign");

            entity.HasOne(d => d.Feedback).WithMany(p => p.Cartdetails)
                .HasForeignKey(d => d.FeedbackId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cartdetail_feedback_id_foreign");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("category_category_id_primary");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryId).HasColumnName("category_Id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("category_name");
            entity.Property(e => e.IsDelete)
                .IsRequired()
                .HasDefaultValueSql("('0')")
                .HasColumnName("isDelete");
        });

        modelBuilder.Entity<Consignment>(entity =>
        {
            entity.HasKey(e => e.ConsignmentId).HasName("consignment_consignment_id_primary");

            entity.ToTable("Consignment");

            entity.Property(e => e.ConsignmentId).HasColumnName("consignment_Id");
            entity.Property(e => e.AccountId).HasColumnName("account_Id");
            entity.Property(e => e.EndTime)
                .HasColumnType("datetime")
                .HasColumnName("endTime");
            entity.Property(e => e.IsDelete)
                .IsRequired()
                .HasDefaultValueSql("('0')")
                .HasColumnName("isDelete");
            entity.Property(e => e.KoiId).HasColumnName("koi_Id");
            entity.Property(e => e.PaymentId).HasColumnName("payment_Id");
            entity.Property(e => e.StartTime)
                .HasColumnType("datetime")
                .HasColumnName("startTime");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasColumnName("status");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .HasColumnName("type");

            entity.HasOne(d => d.Account).WithMany(p => p.Consignments)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("consignment_account_id_foreign");

            entity.HasOne(d => d.Koi).WithMany(p => p.Consignments)
                .HasForeignKey(d => d.KoiId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("consignment_koi_id_foreign");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("feedback_feedback_id_primary");

            entity.ToTable("Feedback");

            entity.Property(e => e.FeedbackId).HasColumnName("feedback_Id");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.IsDelete)
                .IsRequired()
                .HasDefaultValueSql("('0')")
                .HasColumnName("isDelete");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasColumnName("status");
        });

        modelBuilder.Entity<HealthCertificate>(entity =>
        {
            entity.HasKey(e => e.HealthCertificateId).HasName("healthcertificate_healthcertificate_id_primary");

            entity.ToTable("HealthCertificate");

            entity.Property(e => e.HealthCertificateId).HasColumnName("healthCertificate_Id");
            entity.Property(e => e.EndTime)
                .HasColumnType("datetime")
                .HasColumnName("endTime");
            entity.Property(e => e.IsDelete)
                .IsRequired()
                .HasDefaultValueSql("('0')")
                .HasColumnName("isDelete");
            entity.Property(e => e.KoiId).HasColumnName("koi_Id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.StartTime)
                .HasColumnType("datetime")
                .HasColumnName("startTime");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasColumnName("status");

            entity.HasOne(d => d.Koi).WithMany(p => p.HealthCertificates)
                .HasForeignKey(d => d.KoiId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("healthcertificate_koi_id_foreign");
        });

        modelBuilder.Entity<KoiFish>(entity =>
        {
            entity.HasKey(e => e.KoiId).HasName("koifish_koi_id_primary");

            entity.ToTable("KoiFish");

            entity.Property(e => e.KoiId).HasColumnName("Koi_Id");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.AmountFood).HasColumnName("amountFood");
            entity.Property(e => e.CategoryId).HasColumnName("category_Id");
            entity.Property(e => e.Character)
                .HasMaxLength(255)
                .HasColumnName("character");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Gender)
                .HasMaxLength(255)
                .HasColumnName("gender");
            entity.Property(e => e.IsDelete)
                .IsRequired()
                .HasDefaultValueSql("('0')")
                .HasColumnName("isDelete");
            entity.Property(e => e.Origin)
                .HasMaxLength(255)
                .HasColumnName("origin");
            entity.Property(e => e.ScreeningRate).HasColumnName("screeningRate");
            entity.Property(e => e.Size).HasColumnName("size");
            entity.Property(e => e.Species)
                .HasMaxLength(255)
                .HasColumnName("species");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .HasColumnName("type");

            entity.HasOne(d => d.Category).WithMany(p => p.KoiFishes)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("koifish_category_id_foreign");
        });

        modelBuilder.Entity<KoiFish1>(entity =>
        {
            entity.HasKey(e => e.KoiFishesId).HasName("koifishes_koifishes_id_primary");

            entity.ToTable("KoiFishes");

            entity.Property(e => e.KoiFishesId).HasColumnName("koiFishes_Id");
            entity.Property(e => e.CategoryId).HasColumnName("category_Id");
            entity.Property(e => e.IsDelete)
                .IsRequired()
                .HasDefaultValueSql("('0')")
                .HasColumnName("isDelete");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Category).WithMany(p => p.KoiFish1s)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("koifishes_category_id_foreign");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("order_order_id_primary");

            entity.ToTable("Order");

            entity.Property(e => e.OrderId).HasColumnName("order_Id");
            entity.Property(e => e.AccountId).HasColumnName("account_Id");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.IsDelete)
                .IsRequired()
                .HasDefaultValueSql("('0')")
                .HasColumnName("isDelete");
            entity.Property(e => e.KoiFishesId).HasColumnName("koiFishes_Id");
            entity.Property(e => e.KoiId).HasColumnName("koi_Id");
            entity.Property(e => e.PaymentId).HasColumnName("payment_Id");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasColumnName("status");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .HasColumnName("type");

            entity.HasOne(d => d.Account).WithMany(p => p.Orders)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_account_id_foreign");

            entity.HasOne(d => d.KoiFishes).WithMany(p => p.Orders)
                .HasForeignKey(d => d.KoiFishesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_koifishes_foreign");

            entity.HasOne(d => d.Koi).WithMany(p => p.Orders)
                .HasForeignKey(d => d.KoiId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_koi_id_foreign");

            entity.HasOne(d => d.Payment).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PaymentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_payment_id_foreign");
        });

        modelBuilder.Entity<OriginCertificate>(entity =>
        {
            entity.HasKey(e => e.OriginCertificateId).HasName("origincertificate_origincertificate_id_primary");

            entity.ToTable("OriginCertificate");

            entity.Property(e => e.OriginCertificateId).HasColumnName("originCertificate_Id");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.IsDelete)
                .IsRequired()
                .HasDefaultValueSql("('0')")
                .HasColumnName("isDelete");
            entity.Property(e => e.KoiId).HasColumnName("koi_Id");
            entity.Property(e => e.OrderId).HasColumnName("order_Id");
            entity.Property(e => e.Origin)
                .HasMaxLength(255)
                .HasColumnName("origin");

            entity.HasOne(d => d.Koi).WithMany(p => p.OriginCertificates)
                .HasForeignKey(d => d.KoiId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("origincertificate_koi_id_foreign");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("payment_payment_id_primary");

            entity.ToTable("Payment");

            entity.Property(e => e.PaymentId).HasColumnName("payment_Id");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.IsDelete)
                .IsRequired()
                .HasDefaultValueSql("('0')")
                .HasColumnName("isDelete");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(255)
                .HasColumnName("paymentMethod");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.PromotionId).HasName("promotion_promotion_id_primary");

            entity.ToTable("Promotion");

            entity.Property(e => e.PromotionId).HasColumnName("promotion_Id");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.DiscountPercentage).HasColumnName("discountPercentage");
            entity.Property(e => e.IsDelete)
                .IsRequired()
                .HasDefaultValueSql("('0')")
                .HasColumnName("isDelete");
            entity.Property(e => e.Point).HasColumnName("point");
        });

        modelBuilder.Entity<RewardCertificate>(entity =>
        {
            entity.HasKey(e => e.RewardcertificateId).HasName("rewardcertificate_rewardcertificate_id_primary");

            entity.ToTable("RewardCertificate");

            entity.Property(e => e.RewardcertificateId).HasColumnName("rewardcertificate_Id");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Desciptions)
                .HasMaxLength(255)
                .HasColumnName("desciptions");
            entity.Property(e => e.IsDelete)
                .IsRequired()
                .HasDefaultValueSql("('0')")
                .HasColumnName("isDelete");
            entity.Property(e => e.KoiId).HasColumnName("koi_Id");
            entity.Property(e => e.OrderId).HasColumnName("order_Id");

            entity.HasOne(d => d.Order).WithMany(p => p.RewardCertificates)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("rewardcertificate_order_id_foreign");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("role_role_id_primary");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.AccountId).HasColumnName("account_Id");
            entity.Property(e => e.Role1)
                .HasMaxLength(255)
                .HasColumnName("role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
