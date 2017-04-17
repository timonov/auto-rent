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
    public class CustomerFavoursController : Controller
    {
        private AutoRentContext db = new AutoRentContext();

        // GET: CustomerFavours
        public ActionResult Index()
        {
            var customerFavours = db.CustomerFavours.Include(c => c.customer);
            return View(customerFavours.ToList());
        }

        // GET: CustomerFavours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerFavour customerFavour = db.CustomerFavours.Find(id);
            if (customerFavour == null)
            {
                return HttpNotFound();
            }
            return View(customerFavour);
        }

        // GET: CustomerFavours/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "firstName");
            return View();
        }

        // POST: CustomerFavours/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CustomerID,rentStartDate,favouriteBrand,maxRentPricePerDay")] CustomerFavour customerFavour)
        {
            if (ModelState.IsValid)
            {
                db.CustomerFavours.Add(customerFavour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "firstName", customerFavour.CustomerID);
            return View(customerFavour);
        }

        // GET: CustomerFavours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerFavour customerFavour = db.CustomerFavours.Find(id);
            if (customerFavour == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "firstName", customerFavour.CustomerID);
            return View(customerFavour);
        }

        // POST: CustomerFavours/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CustomerID,rentStartDate,favouriteBrand,maxRentPricePerDay")] CustomerFavour customerFavour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerFavour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "firstName", customerFavour.CustomerID);
            return View(customerFavour);
        }

        // GET: CustomerFavours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerFavour customerFavour = db.CustomerFavours.Find(id);
            if (customerFavour == null)
            {
                return HttpNotFound();
            }
            return View(customerFavour);
        }

        // POST: CustomerFavours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomerFavour customerFavour = db.CustomerFavours.Find(id);
            db.CustomerFavours.Remove(customerFavour);
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
