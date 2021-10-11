using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {

        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Session> Sessions { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Purchase> Purchases { get; set; }
    }
}
