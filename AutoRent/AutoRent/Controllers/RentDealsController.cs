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

        // GET: RentDeals
        public ActionResult Index()
        {
            var rents = db.Rents.Include(r => r.car).Include(r => r.customer).Include(r => r.customerFavour);
            return View(rents.ToList());
        }

        // GET: RentDeals/Details/5
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

        // GET: RentDeals/Create
        public ActionResult Create()
        {
            ViewBag.CarID = new SelectList(db.Cars, "ID", "brand");
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "firstName");
            ViewBag.CustomerQueryID = new SelectList(db.CustomerFavours, "ID", "favouriteBrand");
            return View();
        }

        // POST: RentDeals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CarID,CustomerID,CustomerQueryID,dateOfService,dateOfReturn")] RentDeal rentDeal)
        {
            if (ModelState.IsValid)
            {
                db.Rents.Add(rentDeal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarID = new SelectList(db.Cars, "ID", "brand", rentDeal.CarID);
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "firstName", rentDeal.CustomerID);
            ViewBag.CustomerQueryID = new SelectList(db.CustomerFavours, "ID", "favouriteBrand", rentDeal.CustomerQueryID);
            return View(rentDeal);
        }

        // GET: RentDeals/Edit/5
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

        // POST: RentDeals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: RentDeals/Delete/5
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

        // POST: RentDeals/Delete/5
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
