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
    public class CarsController : Controller
    {

        private AutoRentContext db = new AutoRentContext();


        public void TakeCar(int? carId)
        {
            if (carId != null)
            {
                db.Cars.Find(carId).isTaken = true;
                db.SaveChanges();
            }
        }

        public void ReleaseCar(int? carId)
        {
            if (carId != null)
            {
                db.Cars.Find(carId).isTaken = false;
                db.SaveChanges();
            }
        }

        public ActionResult Index(string filter)
        {
            var cars = from car in db.Cars
                       select car;
            switch(filter)
            {
                case "Available":
                    cars = cars.Where(car => !car.isTaken);
                    break;
                case "Taken":
                    cars = cars.Where(car => car.isTaken);
                    break;
                default:
                    break;
            }

            return View(cars.ToList());
        }

        public ActionResult MatchCar(int? customerQueryId)
        {
            if (customerQueryId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.selectedQueryId = customerQueryId;

            CustomerQuery customerQuery = db.CustomerFavours.Find(customerQueryId);

            if (customerQuery == null)
            {
                return HttpNotFound();
            }

            var matchedCars = db.Cars.Where(car => !car.isTaken).
                Where(car => car.rentPrice <= customerQuery.maxRentPricePerDay);

            if (customerQuery.favouriteBrand != null)
            {
                matchedCars = matchedCars.
                    Where(car => car.brand == customerQuery.favouriteBrand);
            }

            if (matchedCars.Any())
            {
                return View(matchedCars.ToList());
            }
            return View();
        }

        public ActionResult AddCar(int? selectedCarId, int? selectedQueryId)
        {
            if (selectedCarId == null | selectedQueryId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var query = db.CustomerFavours.Find(selectedQueryId);

            if (query == null)
            {
                return HttpNotFound();
            }

            var selectedCustomerId = query.CustomerID;

            return RedirectToAction("CreateDeal", "RentDeals",
                new { customerId = selectedCustomerId, queryId = selectedQueryId,
                    carId = selectedCarId});
        }

        public ActionResult CreateCar()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCar([Bind(Include = "ID,brand,totalValue,rentPrice")] Car car)
        {
            if (ModelState.IsValid)
            {
                db.Cars.Add(car);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(car);
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
