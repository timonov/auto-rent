﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AutoRent.Models
{
    public class CustomerQuery
    {
        [Key]
        public int ID { get; set; }

        public int? CustomerID { get; set; }

        [Display(Name="Rent Start Date")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Required]
        public DateTime rentStartDate { get; set; }

        [Display(Name="Days To Rent")]
        [Range(1, int.MaxValue, ErrorMessage="Days value should be positive integer")]
        [Required]
        public int rentDays { get; set; }

        [Display(Name="Favourite Brand")]
        public string favouriteBrand { get; set; }

        [Display(Name="Max Rent Price Per Day")]
        [DataType(DataType.Currency)]
        [Range(0.0, Double.MaxValue, ErrorMessage="Rent price limit should be positive integer")]
        public decimal maxRentPricePerDay { get; set; }

        [Display(Name = "Is Completed?")]
        public bool isCompleted { get; set; }

        [Display(Name="Customer")]
        [ForeignKey("CustomerID")]
        public virtual Customer customer { get; set; }
    }
}
