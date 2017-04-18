using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoRent.Models;

namespace AutoRent.ViewModels
{
    public class CustomerData
    {
        public IEnumerable<Customer> customers { get; set; }
        public IEnumerable<CustomerQuery> queries { get; set; }
        public IEnumerable<RentDeal> deals { get; set; }
    }
}
