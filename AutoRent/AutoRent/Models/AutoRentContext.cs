using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AutoRent.Models
{
    public class AutoRentContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerQuery> CustomerFavours { get; set; }
        public DbSet<RentDeal> Rents { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public AutoRentContext() : base("AutoRentContext") {}

    }
}
