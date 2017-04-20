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
    public class PaymentsController : Controller
    {
        private AutoRentContext db = new AutoRentContext();

        private CarsController carsController = new CarsController();
        private RentDealsController dealsController = new RentDealsController();

        public ActionResult Index()
        {
            var payments = db.Payments.Include(p => p.Penalty).Include(p => p.Rent);
            return View(payments.ToList());
        }


        public ActionResult AddPayment()
        {
            if (TempData["Payment"] != null)
            {
                var payment = (Payment)TempData["Payment"];

                carsController.ReleaseCar(db.Rents.Find(payment.RentID).CarID);

                dealsController.CloseDeal(payment.RentID);

                db.Payments.Add(payment);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult CreatePayment(int? rentId)
        {
            if (rentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var selectedDeal = db.Rents.Find(rentId);

            PaymentData paymentConfirmationViewModel = new PaymentData
            {
                rentPrice = selectedDeal.car.rentPrice,
                rentDays = Convert.ToInt32
                    ((selectedDeal.dateOfReturn - selectedDeal.dateOfService).TotalDays),
                customerDiscount = selectedDeal.customer.discountPercentage.Value
            };

            paymentConfirmationViewModel.priceBeforeDiscount =
                paymentConfirmationViewModel.rentDays * paymentConfirmationViewModel.rentPrice;

            paymentConfirmationViewModel.priceAfterDiscount = 
                paymentConfirmationViewModel.priceBeforeDiscount * (1 - paymentConfirmationViewModel.customerDiscount);

            Payment rentPayment = new Payment
            {
                RentID = rentId.Value,
                amount = paymentConfirmationViewModel.priceAfterDiscount,
            };

            TempData["Payment"] = rentPayment;

            return View(paymentConfirmationViewModel);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }


        public ActionResult Create()
        {
            ViewBag.PenaltyID = new SelectList(db.Penalties, "ID", "ID");
            ViewBag.RentID = new SelectList(db.Rents, "ID", "ID");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RentID,PenaltyID,amount")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Payments.Add(payment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PenaltyID = new SelectList(db.Penalties, "ID", "ID", payment.PenaltyID);
            ViewBag.RentID = new SelectList(db.Rents, "ID", "ID", payment.RentID);
            return View(payment);
        }

        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            ViewBag.PenaltyID = new SelectList(db.Penalties, "ID", "ID", payment.PenaltyID);
            ViewBag.RentID = new SelectList(db.Rents, "ID", "ID", payment.RentID);
            return View(payment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RentID,PenaltyID,amount")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PenaltyID = new SelectList(db.Penalties, "ID", "ID", payment.PenaltyID);
            ViewBag.RentID = new SelectList(db.Rents, "ID", "ID", payment.RentID);
            return View(payment);
        }
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment payment = db.Payments.Find(id);
            db.Payments.Remove(payment);
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
