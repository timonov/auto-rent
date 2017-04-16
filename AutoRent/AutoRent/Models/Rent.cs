using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AutoRent.Models
{
    public class Rent
    {
        [Key]
        public int ID { get; set; }

        public int carID { get; set; }
        public int customerID { get; set; }
        public int customerFavourID { get; set; }

        public DateTime dateOfService { get; set; }
        public DateTime dateOfReturn { get; set; }

        public virtual Car car { get; set; }
        public virtual Customer customer { get; set; }
        public virtual CustomerFavour customerFavour { get; set; }
    }
}
