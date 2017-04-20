using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AutoRent.ViewModels
{
    public class PaymentData
    {
        [Display(Name = "Rent Price Per Day")]
        [DataType(DataType.Currency)]
        public decimal rentPrice { get; set; }

        [Display(Name = "Total Rent Days")]
        public int rentDays { get; set; }

        [Display(Name = "Customer Discount")]
        [DisplayFormat(DataFormatString = "{0:P}")]
        public decimal customerDiscount { get; set; }

        [Display(Name = "Price Before Discount")]
        [DataType(DataType.Currency)]
        public decimal priceBeforeDiscount { get; set; }

        [Display(Name = "Total")]
        [DataType(DataType.Currency)]
        public decimal priceAfterDiscount { get; set; }
    }
}
