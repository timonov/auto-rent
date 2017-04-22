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
            var payments = db.Payments.Include(p => p.Rent);
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
