using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AutoRent.Models
{
    public class CustomerFavour
    {
        [Key]
        public int ID { get; set; }

        public int customerId { get; set; }

        public DateTime rentStartDate { get; set; }

        public uint rentDays { get; set; }
        public string favouriteBrand { get; set; }

        [DataType(DataType.Currency)]
        public decimal maxRentPricePerDay { get; set; }

        public virtual Customer customer { get; set; }
    }

    public class CustomerFavourDbContext : DbContext
    {
        public DbSet<CustomerFavour> customerFavours { get; set; }
    }
}
