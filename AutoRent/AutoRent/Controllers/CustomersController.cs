using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoRent.Models;
using AutoRent.ViewModels;

namespace AutoRent.Controllers
{
    public class CustomersController : Controller
    {
        private AutoRentContext db = new AutoRentContext();

        public ActionResult Index(int? id)
        {
            var customerViewModel = new CustomerData();
            customerViewModel.customers = db.Customers.Include(customer => customer.customerQueries);

            if (id != null)
            {
                ViewBag.selectedCustomerID = id;
                customerViewModel.queries =
                    db.CustomerFavours
                    .Where(query => query.CustomerID == id)
                    .Where(query => !query.isCompleted);
            }

            return View(customerViewModel);
        }

        public ActionResult AddQuery(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return RedirectToAction("CreateQuery", "CustomerQueries", new { customerId = id });
        }

        public ActionResult SelectCarToRent(int? queryId)
        {
            if (queryId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return RedirectToAction("MatchCar", "Cars", new { customerQueryId = queryId });
        }


        public ActionResult CreateCustomer()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCustomer([Bind(Include = "ID,firstName,lastName,middleName,passportDetails,phoneNumber,discountPercentage")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (!customer.discountPercentage.HasValue)
                {
                    customer.discountPercentage = 0;
                }
                else
                {
                    customer.discountPercentage /= 100;
                }

                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
