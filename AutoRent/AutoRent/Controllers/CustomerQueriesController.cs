using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoRent.Models;

namespace AutoRent.Controllers
{
    public class CustomerQueriesController : Controller
    {
        private AutoRentContext db = new AutoRentContext();


        public void CompleteQuery(int? queryId)
        {
            if (queryId != null)
            {
                db.CustomerFavours.Find(queryId).isCompleted = true;

                db.SaveChanges();
            }
        }

        public ActionResult CreateQuery(int? customerId)
        {
            if (customerId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.customersList = new SelectList
                (db.Customers, "ID", "fullName", customerId);

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateQuery([Bind(Include = "ID,CustomerID,rentStartDate,rentDays,favouriteBrand,maxRentPricePerDay")] CustomerQuery customerQuery)
        {
            if (ModelState.IsValid)
            {
                db.CustomerFavours.Add(customerQuery);
                db.SaveChanges();
                return RedirectToAction("Index", "Customers", new { id = customerQuery.CustomerID });
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "fullName", customerQuery.CustomerID);

            return View(customerQuery);
        }


        public ActionResult Index()
        {
            var customerFavours = db.CustomerFavours.Include(c => c.customer);
            return View(customerFavours.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerQuery customerQuery = db.CustomerFavours.Find(id);
            if (customerQuery == null)
            {
                return HttpNotFound();
            }
            return View(customerQuery);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerQuery customerQuery = db.CustomerFavours.Find(id);
            if (customerQuery == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "firstName", customerQuery.CustomerID);
            return View(customerQuery);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CustomerID,rentStartDate,rentDays,favouriteBrand,maxRentPricePerDay")] CustomerQuery customerQuery)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerQuery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "firstName", customerQuery.CustomerID);
            return View(customerQuery);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerQuery customerQuery = db.CustomerFavours.Find(id);
            if (customerQuery == null)
            {
                return HttpNotFound();
            }
            return View(customerQuery);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomerQuery customerQuery = db.CustomerFavours.Find(id);
            db.CustomerFavours.Remove(customerQuery);
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
