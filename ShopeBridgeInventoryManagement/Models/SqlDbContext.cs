using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ShopeBridgeInventoryManagement.Models
{
    public class SqlDbContext
    {
        public SqlDbContext() : base("name=SqlConn")
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}