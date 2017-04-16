using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AutoRent.Models
{
    public class AutoRentContext : DbContext
    {
        public DbSet<Car> cars;
        public DbSet<Customer> customers { get; set; }
        public DbSet<CustomerFavour> customerFavours { get; set; }

        public DbSet<Rent> rents { get; set; }
        public DbSet<Penalty> penalties { get; set; }
        public DbSet<Payment> payments { get; set; }

        public AutoRentContext() : base("AutoRentContext") {}


    }
}