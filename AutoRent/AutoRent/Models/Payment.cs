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
        public int rentID { get; set; }

        public int? penaltyID { get; set; }

        [DataType(DataType.Currency)]
        [Required]
        public decimal amount { get; set; }

        public virtual Rent rent { get; set; }
        public virtual Penalty penalty { get; set; }
    }
}
