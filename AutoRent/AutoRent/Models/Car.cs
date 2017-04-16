using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace AutoRent.Models
{
    public class Car
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Brand name")]
        [Required]
        public string brand { get; set; }

        [Display(Name = "Value")]
        [DataType(DataType.Currency)]
        [Required]
        public decimal totalValue { get; set; }

        [Display(Name = "Rent price")]
        [DataType(DataType.Currency)]
        [Required]
        public decimal rentPrice { get; set; }
    }

    public class CarsDbContext : DbContext
    {
        public DbSet<Car> cars;
    }
}
