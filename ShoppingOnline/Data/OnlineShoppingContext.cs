using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingOnline.Models;

namespace ShoppingOnline.Data
{
    public class ShoppingOnlineContext : DbContext
    {
        public ShoppingOnlineContext (DbContextOptions<ShoppingOnlineContext> options)
            : base(options)
        {
        }
        public DbSet<Items> Items { get; set; }
        public DbSet<YourOrders> YourOrders { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Authenticate> Authenticate { get; set; }
    }
}
