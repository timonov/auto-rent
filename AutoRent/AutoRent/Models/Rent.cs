using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AutoRent.Models
{
    public class Rent
    {
        [Key]
        public int ID { get; set; }

        public int? CarID { get; set; }
        public int? CustomerID { get; set; }
        public int? CustomerFavourID { get; set; }

        [Display(Name="Service date")]
        [Required]
        public DateTime dateOfService { get; set; }

        [Display(Name="Date to return")]
        [Required]
        public DateTime dateOfReturn { get; set; }

        [ForeignKey("CarID")]
        public virtual Car car { get; set; }

        [ForeignKey("CustomerID")]
        public virtual Customer customer { get; set; }

        [ForeignKey("CustomerFavourID")]
        public virtual CustomerFavour customerFavour { get; set; }
    }
}
