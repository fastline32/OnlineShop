using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace OnlineShop.Data
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
    }
}
