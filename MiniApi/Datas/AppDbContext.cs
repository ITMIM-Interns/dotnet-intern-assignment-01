using Microsoft.EntityFrameworkCore;
using MiniApi.Entites;

namespace MiniApi.Datas
{
    public sealed class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }

        //public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        //public DbSet<OrderItems> OrderItems { get; set; }
        //public DbSet<User> Users { get; set; }
    }
}
