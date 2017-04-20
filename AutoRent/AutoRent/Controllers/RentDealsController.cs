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


        public void CloseDeal(int? rentId)
        {
            if (rentId != null)
            {
                db.Rents.Find(rentId).isClosed = true;

                db.SaveChanges();
            }
        }

        public ActionResult InitDealClose(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return RedirectToAction("CreatePayment", "Payments", new { rentId = id });
        }

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
