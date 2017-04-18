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
        public int? CustomerQueryID { get; set; }

        [Display(Name="Service Date")]
        [Required]
        public DateTime dateOfService { get; set; }

        [Display(Name="Date To Return")]
        [Required]
        public DateTime dateOfReturn { get; set; }

        [ForeignKey("CarID")]
        public virtual Car car { get; set; }

        [ForeignKey("CustomerID")]
        public virtual Customer customer { get; set; }

        [ForeignKey("CustomerQueryID")]
        public virtual CustomerQuery customerFavour { get; set; }
    }
}
