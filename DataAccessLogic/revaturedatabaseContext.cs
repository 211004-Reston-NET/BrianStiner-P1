using Microsoft.EntityFrameworkCore;
using Models;

#nullable disable

namespace DataAccessLogic
{
    public partial class revaturedatabaseContext : DbContext
    {
        public revaturedatabaseContext() {}

        public revaturedatabaseContext(DbContextOptions<revaturedatabaseContext> options)
            : base(options) {}

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<InventoryItem> Inventory { get; set; }
        public virtual DbSet<LineItem> LineItems { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.Id)
                    .HasColumnName("Id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.Picture)
                    .HasColumnName("picture");

                entity.Property(e => e.TotalSpent)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("totalspent");

                entity.HasMany(d => d.Orders)
                    .WithOne(p => p.Customer)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_Order");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("Store");

                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.Expenses)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("expenses");

                entity.Property(e => e.Revenue)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("revenue");
                
                entity.HasMany(d => d.Inventory)
                    .WithOne(p => p.Store)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Store__StoreId__1CF15040");
            });
            
            modelBuilder.Entity<InventoryItem>(entity =>
            {
                entity.ToTable("Inventory");

                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.ProductId).HasColumnName("ProductId");

                entity.Property(e => e.StoreId).HasColumnName("StoreId");

                entity.Property(e => e.Quantity).HasColumnName("Quantity");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Inventory__Product__282DF8C2");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Inventory__Store__2739D489");
            });

            modelBuilder.Entity<LineItem>(entity =>
            {
                entity.ToTable("LineItem");

                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.ProductId).HasColumnName("ProductId");

                entity.Property(e => e.OrderId).HasColumnName("OrderId");

                entity.Property(e => e.Quantity).HasColumnName("Quantity");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.LineItems)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LineItem__Produc__19DFD96B");
                
                entity.HasOne(d => d.Order)
                    .WithMany(p => p.LineItems)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LineItem__Order__1A14E395");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Orders");

                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerId");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Customer");
                
                entity.HasMany(d => d.LineItems)
                    .WithOne(p => p.Order)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_LineItem");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");
                
                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("category");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("price");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.HasMany(d => d.LineItems)
                    .WithOne(p => p.Product)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__Produc__1B0907CE");
                
                entity.HasMany(d => d.Inventory)
                    .WithOne(p => p.Product)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__Produc__1BFD2C07");
            });


            

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("password");
                
                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("phone");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
