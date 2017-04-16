using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AutoRent.Models
{
    public class Customer
    {
        [Key]
        public int ID { get; set; }

        [Display(Name="First name")]
        [Required]
        public string firstName { get; set; }

        [Display(Name="Last name")]
        [Required]
        public string lastName { get; set; }

        [Display(Name="Middle name")]
        public string middleName { get; set; }

        [Required]
        [Display(Name="Passport details")]
        public string passportDetails { get; set; }

        [Required]
        [Display(Name="Phone number")]
        [DataType(DataType.PhoneNumber)]
        public string phoneNumber { get; set; }

        [Required]
        [Display(Name="Discount")]
        public decimal discountPercentage { get; set; }
    }

    public class CustomerDbContext : DbContext
    {
        public DbSet<Customer> customers { get; set; }
    }
}
