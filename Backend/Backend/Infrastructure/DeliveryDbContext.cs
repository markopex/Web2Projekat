using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure
{
    public class DeliveryDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {optionsBuilder.UseSqlServer(@"Data Source=(localdb)\ProjectModels;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // setup users
            modelBuilder.Entity<User>().HasKey(s => s.Email);
            modelBuilder.Entity<User>().Property(s => s.Username)
                .IsRequired()
                .IsUnicode();
            modelBuilder.Entity<User>().Property(s => s.Email)
                .IsRequired();
            modelBuilder.Entity<User>().Property(s => s.Birthday)
                .IsRequired();
            modelBuilder.Entity<User>().Property(s => s.Address)
                .IsRequired();
            modelBuilder.Entity<User>().Property(s => s.FirstName)
                .IsRequired();
            modelBuilder.Entity<User>().Property(s => s.LastName)
                .IsRequired();
            modelBuilder.Entity<User>().Property(s => s.Type)
                .IsRequired();

            // setup product
            modelBuilder.Entity<Product>().HasKey(s => s.Id);
            modelBuilder.Entity<Product>().Property(s => s.Price).IsRequired();
            modelBuilder.Entity<Product>().Property(s => s.Name).IsRequired();

            // setup order
            modelBuilder.Entity<Order>().HasKey(s => s.Id);
            modelBuilder.Entity<Order>().Property(s => s.Address)
                .IsRequired();
            modelBuilder.Entity<Order>()
                .HasOne<User>(s => s.Customer)
                .WithMany(g => g.Orders)
                .HasPrincipalKey(s => s.Email)
                .IsRequired();
            modelBuilder.Entity<Order>()
                .HasOne<User>(s => s.Deliverer)
                .WithMany(g => g.OrdersToDiliver)
                .HasPrincipalKey(s => s.Email)
                .IsRequired();

            modelBuilder.Entity<Order>()
                .HasMany<OrderDetails>(s => s.OrderDetails)
                .WithOne(g => g.Order)
                .HasPrincipalKey(s => s.Id)
                .IsRequired();

            // setup order details
            modelBuilder.Entity<OrderDetails>()
                .HasKey(s => s.Id);
            modelBuilder.Entity<OrderDetails>().Property(s => s.Quantity).IsRequired();
            modelBuilder.Entity<OrderDetails>().Property(s => s.ProductPrice).IsRequired();
            modelBuilder.Entity<OrderDetails>()
                .HasOne<Order>(s => s.Order)
                .WithMany(ad => ad.OrderDetails)
                .HasForeignKey(ad => ad.Id);


        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
    }
}
