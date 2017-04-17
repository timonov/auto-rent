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
        public DbSet<CustomerFavour> CustomerFavours { get; set; }

        public DbSet<Rent> Rents { get; set; }
        public DbSet<Penalty> Penalties { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public AutoRentContext() : base("AutoRentContext") {}

    }
}