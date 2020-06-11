using LAB4_150348.Models;
using Microsoft.EntityFrameworkCore;

namespace LAB4_150348
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            this.Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<BookOrder>()
                .HasKey(order => new {order.OrderId, order.BookId});

            modelBuilder
                .Entity<BookOrder>()
                .HasOne(bookOrder => bookOrder.Order)
                .WithMany(order => order.BookOrders);

            modelBuilder
                .Entity<BookOrder>()
                .HasOne(bookOrder => bookOrder.Book)
                .WithMany(book => book.BookOrders);
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<BookOrder> BookOrders { get; set; }
    }
}