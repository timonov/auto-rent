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

            return RedirectToAction("Create", "CustomerQueries", new { customerId = id });
        }

        public ActionResult SelectCarToRent(int? queryId)
        {
            if (queryId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return RedirectToAction("MatchCar", "Cars", new { customerQueryId = queryId });
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,firstName,lastName,middleName,passportDetails,phoneNumber,discountPercentage")] Customer customer)
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


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,firstName,lastName,middleName,passportDetails,phoneNumber,discountPercentage")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.CustomerFavours.RemoveRange(customer.customerQueries);
            db.Customers.Remove(customer);
            db.SaveChanges();

            return RedirectToAction("Index");
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
