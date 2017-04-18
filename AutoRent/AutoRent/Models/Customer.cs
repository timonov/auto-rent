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

        [Display(Name="Full name")]
        public string fullName
        {
            get
            {
                return lastName + ", " + firstName + " " + middleName;
            }
        }

        [Display(Name="First Name")]
        [Required]
        public string firstName { get; set; }

        [Display(Name="Last Name")]
        [Required]
        public string lastName { get; set; }

        [Display(Name="Middle Name")]
        public string middleName { get; set; }

        [Required]
        [Display(Name="Passport Details")]
        public string passportDetails { get; set; }

        [Required]
        [Display(Name="Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string phoneNumber { get; set; }


        [Display(Name = "Percentage of Discount")]
        [DisplayFormat(DataFormatString = "{0:P}")]
        public decimal? discountPercentage { get; set; }
    }
}
