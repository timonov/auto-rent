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

        [Display(Name="Brand Name")]
        [Required]
        public string brand { get; set; }

        [Display(Name="Total Value")]
        [DataType(DataType.Currency)]
        [Range(0.0, Double.MaxValue, ErrorMessage="Total value should be positive")]
        [Required]
        public decimal totalValue { get; set; }

        [Display(Name="Rent Price Per Day")]
        [DataType(DataType.Currency)]
        [Range(0.0, Double.MaxValue, ErrorMessage="Price should be positive number")]
        [Required]
        public decimal rentPrice { get; set; }

        [Display(Name="Is taken?")]
        [Required]
        public bool isTaken { get; set; }
    }
}
