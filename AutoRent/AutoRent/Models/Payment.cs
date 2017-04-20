using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AutoRent.Models
{
    public class Payment
    {
        [Key]
        [ForeignKey("Rent")]
        public int RentID { get; set; }


        [ForeignKey("Penalty")]
        public int? PenaltyID { get; set; }

        [Display(Name="Payment Amount")]
        [DataType(DataType.Currency)]
        [Required]
        public decimal amount { get; set; }

        [ForeignKey("RentID")]
        public virtual RentDeal Rent { get; set; }

        [ForeignKey("PenaltyID")]
        public virtual Penalty Penalty { get; set; }
    }
}
