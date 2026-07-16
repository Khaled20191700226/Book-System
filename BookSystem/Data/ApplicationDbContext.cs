using BookSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BookSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .Property(b => b.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.UnitPrice)
                .HasPrecision(18, 2);


            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Customer> Customers { get; set; }     // جدول العملاء
        public DbSet<Order> Orders { get; set; }           // جدول الطلبات
        public DbSet<Book> Books { get; set; }             // جدول الكتب
        public DbSet<OrderItem> OrderItems { get; set; }   // جدول عناصر الطلب
    }
}


