using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AutoRent.Models
{
    public enum PenaltyType
    {
        WARNING, ACTION
    };

    public class Penalty
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Type")]
        [DataType(DataType.Text)]
        [Required]
        public PenaltyType type { get; set; }

        [Display(Name = "Penalty amount")]
        [DataType(DataType.Currency)]
        [Required]
        public decimal amount { get; set; }
    }
}
