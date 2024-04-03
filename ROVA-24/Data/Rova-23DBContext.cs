using Microsoft.EntityFrameworkCore;
using ROVA_24.Models;


namespace ROVA_24.Data
{
    public class Rova_23DBContext : DbContext
    {
        public Rova_23DBContext(DbContextOptions<Rova_23DBContext> options) : base(options) { }
        public DbSet<Customers> Customers { get; set; } = default;
        public DbSet<Address> Address { get; set; } = default;
        public DbSet<Products> Products { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
            .HasOne<Customers>(a => a.Customer)
            .WithMany(c => c.Addresses)
            .HasForeignKey(a => a.CustomerId);

            modelBuilder.Entity<Reviews>()
               .HasOne(r => r.Customers)
               .WithMany(c => c.Reviews)
               .HasForeignKey(r => r.CustomerId);

            modelBuilder.Entity<Reviews>()
                .HasOne(r => r.Products)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductId);

        }
    }
        
}
