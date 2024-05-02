using LapShop.Domains.Models;
using LapShop.Ef.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LapShop.EF.DBContext
{
    public partial class LapShopContext : IdentityDbContext<ApplicationUser>
    {
        public LapShopContext(DbContextOptions<LapShopContext> options)
            : base(options)
        {
        }





        public virtual DbSet<TbBusinessInfo> TbBusinessInfos { get; set; } = null!;
        public virtual DbSet<TbCashTransacion> TbCashTransacions { get; set; } = null!;
        public virtual DbSet<TbCategory> TbCategories { get; set; } = null!;
        public virtual DbSet<TbCustomer> TbCustomers { get; set; } = null!;
        public virtual DbSet<TbItem> TbItems { get; set; } = null!;
        public virtual DbSet<TbItemDiscount> TbItemDiscounts { get; set; } = null!;
        public virtual DbSet<TbItemImage> TbItemImages { get; set; } = null!;
        public virtual DbSet<TbItemType> TbItemTypes { get; set; } = null!;
        public virtual DbSet<TbO> TbOs { get; set; } = null!;
        public virtual DbSet<TbPurchaseInvoice> TbPurchaseInvoices { get; set; } = null!;
        public virtual DbSet<TbPurchaseInvoiceItem> TbPurchaseInvoiceItems { get; set; } = null!;
        public virtual DbSet<TbSalesInvoice> TbSalesInvoices { get; set; } = null!;
        public virtual DbSet<TbSalesInvoiceItem> TbSalesInvoiceItems { get; set; } = null!;
        public virtual DbSet<TbSlider> TbSliders { get; set; } = null!;
        public virtual DbSet<TbSupplier> TbSuppliers { get; set; } = null!;
        public virtual DbSet<VwItem> VwItems { get; set; } = null!;
        public virtual DbSet<VwItemCategory> VwItemCategories { get; set; } = null!;
        public virtual DbSet<VwItemsOutOfInvoice> VwItemsOutOfInvoices { get; set; } = null!;
        public virtual DbSet<VwSalesInvoice> VwSalesInvoices { get; set; } = null!;
        public virtual DbSet<VwOrderDetails> VwOrderDetails { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //for Identity
            base.OnModelCreating(modelBuilder);
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");



            modelBuilder.Entity<TbBusinessInfo>(entity =>
            {
                entity.HasKey(e => e.BusinessInfoId);

                entity.ToTable("TbBusinessInfo");

                entity.HasIndex(e => e.CutomerId, "IX_TbBusinessInfo_CutomerId")
                    .IsUnique();

                entity.Property(e => e.Budget).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.BusinessCardNumber).HasMaxLength(20);

                entity.HasOne(d => d.Cutomer)
                    .WithOne(p => p.TbBusinessInfo)
                    .HasForeignKey<TbBusinessInfo>(d => d.CutomerId);
            });

            modelBuilder.Entity<TbCashTransacion>(entity =>
            {
                entity.HasKey(e => e.CashTransactionId);

                entity.ToTable("TbCashTransacion");

                entity.Property(e => e.CashDate).HasColumnType("datetime");

                entity.Property(e => e.CashValue).HasColumnType("decimal(8, 2)");
            });

            modelBuilder.Entity<TbCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.Property(e => e.CategoryName).HasMaxLength(100);

                entity.Property(e => e.CreatedBy).HasDefaultValueSql("(N'')");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.Property(e => e.ImageName).HasDefaultValueSql("(N'')");

                entity.Property(e => e.ShowInHomePage)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");
            });

            modelBuilder.Entity<TbCustomer>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.Property(e => e.CustomerName).HasMaxLength(100);
            });

            modelBuilder.Entity<TbItem>(entity =>
            {
                entity.HasKey(e => e.ItemId);

                entity.HasIndex(e => e.ItemTypeId, "IX_TbItems_ItemTypeId");

                entity.HasIndex(e => e.OsId, "IX_TbItems_OsId");

                entity.Property(e => e.CreatedBy).HasDefaultValueSql("(N'')");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("('2020-09-20T00:00:00.0000000')");

                entity.Property(e => e.ImageName).HasMaxLength(200);

                entity.Property(e => e.ItemName).HasMaxLength(100);

                entity.Property(e => e.ItemTypeId).HasDefaultValueSql("((0))");

                entity.Property(e => e.OsId).HasDefaultValueSql("((0))");

                entity.Property(e => e.PurchasePrice).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.SalesPrice).HasColumnType("decimal(8, 2)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.TbItems)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TbItems_TbCategories");

                entity.HasOne(d => d.ItemType)
                    .WithMany(p => p.TbItems)
                    .HasForeignKey(d => d.ItemTypeId)
                    .HasConstraintName("FK_TbItems_TbItemTypes");

                entity.HasOne(d => d.Os)
                    .WithMany(p => p.TbItems)
                    .HasForeignKey(d => d.OsId)
                    .HasConstraintName("FK_TbItems_TbOs");

                entity.HasMany(d => d.Customers)
                    .WithMany(p => p.Items)
                    .UsingEntity<Dictionary<string, object>>(
                        "TbCustomerItem",
                        l => l.HasOne<TbCustomer>().WithMany().HasForeignKey("CustomerId"),
                        r => r.HasOne<TbItem>().WithMany().HasForeignKey("ItemId"),
                        j =>
                        {
                            j.HasKey("ItemId", "CustomerId");

                            j.ToTable("TbCustomerItems");

                            j.HasIndex(new[] { "CustomerId" }, "IX_TbCustomerItems_CustomerId");
                        });
            });

            modelBuilder.Entity<TbItemDiscount>(entity =>
            {
                entity.HasKey(e => e.ItemDiscountId);

                entity.ToTable("TbItemDiscount");

                entity.HasIndex(e => e.ItemId, "IX_TbItemDiscount_ItemId");

                entity.Property(e => e.DiscountPercent).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.TbItemDiscounts)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TbItemDiscounts_TbItems");
            });

            modelBuilder.Entity<TbItemImage>(entity =>
            {
                entity.HasKey(e => e.ImageId);

                entity.Property(e => e.ImageName).HasMaxLength(200);

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.TbItemImages)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TbItemImages_TbItems");
            });

            modelBuilder.Entity<TbItemType>(entity =>
            {
                entity.HasKey(e => e.ItemTypeId);

                entity.Property(e => e.ItemTypeName).HasMaxLength(100);
            });

            modelBuilder.Entity<TbO>(entity =>
            {
                entity.HasKey(e => e.OsId);

                entity.Property(e => e.OsName).HasMaxLength(100);
            });

            modelBuilder.Entity<TbPurchaseInvoice>(entity =>
            {
                entity.HasKey(e => e.InvoiceId);

                entity.Property(e => e.InvoiceDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.TbPurchaseInvoices)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TbPurchaseInvoices_TbSuppliers");
            });

            modelBuilder.Entity<TbPurchaseInvoiceItem>(entity =>
            {
                entity.HasKey(e => e.InvoiceItemId);

                entity.Property(e => e.InvoiceItemId).ValueGeneratedNever();

                entity.Property(e => e.InvoicePrice).HasColumnType("decimal(8, 4)");

                entity.Property(e => e.Qty).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.TbPurchaseInvoiceItems)
                    .HasForeignKey(d => d.InvoiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TbPurchaseInvoiceItems_TbPurchaseInvoices");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.TbPurchaseInvoiceItems)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TbPurchaseInvoiceItems_TbItems");
            });

            modelBuilder.Entity<TbSalesInvoice>(entity =>
            {
                entity.HasKey(e => e.InvoiceId);

                entity.Property(e => e.CreatedBy).HasDefaultValueSql("(N'')");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.Property(e => e.DelivryDate).HasColumnType("datetime");

                entity.Property(e => e.InvoiceDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TbSalesInvoiceItem>(entity =>
            {
                entity.HasKey(e => e.InvoiceItemId);

                entity.Property(e => e.InvoicePrice).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.Qty).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.TbSalesInvoiceItems)
                    .HasForeignKey(d => d.InvoiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TbSalesInvoiceItems_TbSalesInvoices");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.TbSalesInvoiceItems)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TbSalesInvoiceItems_TbItems");
            });

            modelBuilder.Entity<TbSlider>(entity =>
            {
                entity.HasKey(e => e.SliderId);

                entity.ToTable("TbSlider");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.ImageName).HasMaxLength(200);

                entity.Property(e => e.Title).HasMaxLength(200);
            });

            modelBuilder.Entity<TbSupplier>(entity =>
            {
                entity.HasKey(e => e.SupplierId)
                    .HasName("PK_TbSupplier");

                entity.Property(e => e.SupplierName).HasMaxLength(100);
            });

            modelBuilder.Entity<VwItem>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwItems");

                entity.Property(e => e.CategoryName).HasMaxLength(100);

                entity.Property(e => e.ImageName).HasMaxLength(200);

                entity.Property(e => e.ItemName).HasMaxLength(100);

                entity.Property(e => e.ItemTypeName).HasMaxLength(100);

                entity.Property(e => e.OsName).HasMaxLength(100);

                entity.Property(e => e.PurchasePrice).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.SalesPrice).HasColumnType("decimal(8, 2)");
            });

            modelBuilder.Entity<VwItemCategory>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwItemCategories");

                entity.Property(e => e.CategoryName).HasMaxLength(100);

                entity.Property(e => e.ImageName).HasMaxLength(200);

                entity.Property(e => e.ItemName).HasMaxLength(100);

                entity.Property(e => e.PurchasePrice).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.SalesPrice).HasColumnType("decimal(8, 2)");
            });

            modelBuilder.Entity<VwItemsOutOfInvoice>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwItemsOutOfInvoices");

                entity.Property(e => e.CategoryName).HasMaxLength(100);

                entity.Property(e => e.InvoicePrice).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.ItemName).HasMaxLength(100);

                entity.Property(e => e.PurchasePrice).HasColumnType("decimal(8, 2)");
            });

            modelBuilder.Entity<VwSalesInvoice>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwSalesInvoices");

                entity.Property(e => e.DelivryDate).HasColumnType("datetime");

                entity.Property(e => e.InvoiceDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<VwOrderDetails>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwOrderDetails");

                entity.Property(e => e.DelivryDate).HasColumnType("datetime");

                entity.Property(e => e.InvoiceDate).HasColumnType("datetime");
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
