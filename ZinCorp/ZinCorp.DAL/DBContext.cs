using Microsoft.EntityFrameworkCore;
using ZinCorp.DAL.Models;

namespace ZinCorp.DAL
{
    public sealed class DBContext : DbContext
    {
        public DBContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<DbCustomer> Customers { get; set; }

        public DbSet<DbProduct> Products { get; set; }

        public DbSet<DbProductImage> ProductImages { get; set; }
    }

}
