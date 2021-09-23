using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace FlowerAPI.FlowerModel
{
    public partial class dbFlowerStoreContext : DbContext
    {
        public dbFlowerStoreContext()
        {
        }

        public dbFlowerStoreContext(DbContextOptions<dbFlowerStoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Flower> Flowers { get; set; }
        public virtual DbSet<Occasion> Occasions { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=KANINI-LTP-484\\SQLSERVER2019SUG;Database=dbFlowerStore;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(e => new { e.CustomerId, e.FlowerId })
                    .HasName("pk_cart");

                entity.ToTable("Cart");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");

                entity.Property(e => e.FlowerId).HasColumnName("flower_id");

                entity.Property(e => e.ItemPrice).HasColumnName("item_price");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cart__Customer_I__44FF419A");

                entity.HasOne(d => d.Flower)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.FlowerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cart__flower_id__45F365D3");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.Vendor)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("vendor")
                    .HasDefaultValueSql("('User')");
            });

            modelBuilder.Entity<Flower>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AvailableQuantity).HasColumnName("available_Quantity");

                entity.Property(e => e.FlImage).HasColumnName("fl_image");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Occassion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("occassion");

                entity.Property(e => e.UnitPrice).HasColumnName("unitPrice");
            });

            modelBuilder.Entity<Occasion>(entity =>
            {
                entity.HasKey(e => e.OccId)
                    .HasName("PK__occasion__BE0ACE5CAECF51D8");

                entity.ToTable("occasion");

                entity.Property(e => e.OccId)
                    .ValueGeneratedNever()
                    .HasColumnName("occ_id");

                entity.Property(e => e.OccDetails)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("occ_details");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__orderDet__46596229718FEB77");

                entity.ToTable("orderDetails");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.FlowerId).HasColumnName("flower_id");

                entity.Property(e => e.PaymentStatus)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("payment_status")
                    .HasDefaultValueSql("('Pending')");

                entity.Property(e => e.Remark)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("remark");

                entity.Property(e => e.Totalprice).HasColumnName("totalprice");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__orderDeta__custo__3E52440B");

                entity.HasOne(d => d.Flower)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.FlowerId)
                    .HasConstraintName("FK__orderDeta__flowe__3D5E1FD2");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
