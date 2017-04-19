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
    public class RentDealsController : Controller
    {
        private AutoRentContext db = new AutoRentContext();

        private CarsController carsController = new CarsController();

        private CustomerQueriesController queriesController = new CustomerQueriesController();


        public ActionResult AddDeal()
        {
            if (TempData["Deal"] != null)
            {
                var rentDeal = (RentDeal)TempData["Deal"];

                carsController.TakeCar(rentDeal.CarID);

                queriesController.CompleteQuery(rentDeal.CustomerQueryID);

                db.Rents.Add(rentDeal);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Index()
        {
            var rents = db.Rents.Include(r => r.car).
                Include(r => r.customer).Include(r => r.customerFavour)
                .Where(r => !r.isClosed);

            return View(rents.ToList());
        }


       
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentDeal rentDeal = db.Rents.Find(id);
            if (rentDeal == null)
            {
                return HttpNotFound();
            }
            return View(rentDeal);
        }


        public ActionResult CreateDeal(int? customerId, int? queryId, int? carId)
        {
            if (customerId == null | queryId == null | carId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var query = db.CustomerFavours.Find(queryId);

            var selectedCar = db.Cars.Find(carId);

            if (query == null || selectedCar == null)
            {
                return HttpNotFound();
            }

            var startingDate = query.rentStartDate;

            var daysForRent = query.rentDays;

            RentDeal anotherRentDeal = new RentDeal
            {
                CarID = carId,
                CustomerQueryID = queryId,
                CustomerID = customerId,
                dateOfService = startingDate,
                dateOfReturn = startingDate.AddDays(daysForRent)
            };

            ViewBag.Brand = db.Cars.Find(carId).brand;
            ViewBag.FullName = db.Customers.Find(customerId).fullName;
            ViewBag.RentPrice = db.Cars.Find(carId).rentPrice;

            TempData["Deal"] = anotherRentDeal;

            return View(anotherRentDeal);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentDeal rentDeal = db.Rents.Find(id);
            if (rentDeal == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarID = new SelectList(db.Cars, "ID", "brand", rentDeal.CarID);
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "firstName", rentDeal.CustomerID);
            ViewBag.CustomerQueryID = new SelectList(db.CustomerFavours, "ID", "favouriteBrand", rentDeal.CustomerQueryID);
            return View(rentDeal);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CarID,CustomerID,CustomerQueryID,dateOfService,dateOfReturn")] RentDeal rentDeal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rentDeal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarID = new SelectList(db.Cars, "ID", "brand", rentDeal.CarID);
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "firstName", rentDeal.CustomerID);
            ViewBag.CustomerQueryID = new SelectList(db.CustomerFavours, "ID", "favouriteBrand", rentDeal.CustomerQueryID);
            return View(rentDeal);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentDeal rentDeal = db.Rents.Find(id);
            if (rentDeal == null)
            {
                return HttpNotFound();
            }
            return View(rentDeal);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RentDeal rentDeal = db.Rents.Find(id);
            db.Rents.Remove(rentDeal);
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
