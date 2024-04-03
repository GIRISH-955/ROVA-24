using Microsoft.EntityFrameworkCore;
using ROVA_24.Model;

namespace ROVA_24.Data
{
    public class ROVADBContext : DbContext
    {
        public ROVADBContext(DbContextOptions<ROVADBContext> options) : base(options) { }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Addresses)  // One Customer has many Addresses
                .WithOne(a => a.Customer)   // Each Address belongs to one Customer
                .HasForeignKey(a => a.CustomerId);  // Foreign key property in Address

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Orders)     // One Customer has many Orders
                .WithOne(o => o.Customer)   // Each Order belongs to one Customer
                .HasForeignKey(o => o.CustomerId);  // Foreign key property in Order

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Reviews)    // One Customer has many Reviews
                .WithOne(r => r.Customer)   // Each Review belongs to one Customer
                .HasForeignKey(r => r.CustomerId);  // Foreign key property in Review

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)  // One Order has many OrderItems
                .WithOne(oi => oi.Order)     // Each OrderItem belongs to one Order
                .HasForeignKey(oi => oi.OrderId);  // Foreign key property in OrderItem

            // Add configurations for other relationships if needed

            base.OnModelCreating(modelBuilder);
        }
    }
}