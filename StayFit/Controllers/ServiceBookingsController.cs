using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StayFit.Models;
using Microsoft.AspNet.Identity;

namespace StayFit.Controllers
{
    public class ServiceBookingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ServiceBookings
        public ActionResult Index()
        {
            return View(db.ServiceBooking.ToList());
        }

        // GET: ServiceBookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceBooking serviceBooking = db.ServiceBooking.Find(id);
            if (serviceBooking == null)
            {
                return HttpNotFound();
            }
            return View(serviceBooking);
        }

        // GET: ServiceBookings/Create
        public ActionResult Create()
        {
            ViewBag.Service    = new SelectList(db.Service, "Service_Id ", "SeviceName");
            return View();
        }

        // POST: ServiceBookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Booking_Id,BookingDate,BookingStatus,ServiceTime")] ServiceBooking serviceBooking)
        {
            if (ModelState.IsValid)
            {
                serviceBooking.ApplicationUser = db.Users.Find(User.Identity.GetUserId());
                db.ServiceBooking.Add(serviceBooking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(serviceBooking);
        }
        public JsonResult GetTimings(int service_id)
        {

            List < ServiceTimings > ServiceTimings = db.ServiceTimings.Where(x => x.Service.Service_Id == service_id).ToList();
            return Json(ServiceTimings,JsonRequestBehavior.AllowGet);
        }
        // GET: ServiceBookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceBooking serviceBooking = db.ServiceBooking.Find(id);
            if (serviceBooking == null)
            {
                return HttpNotFound();
            }
            return View(serviceBooking);
        }

        // POST: ServiceBookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Booking_Id,BookingDate,BookingStatus,ServiceTime")] ServiceBooking serviceBooking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serviceBooking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(serviceBooking);
        }

        // GET: ServiceBookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceBooking serviceBooking = db.ServiceBooking.Find(id);
            if (serviceBooking == null)
            {
                return HttpNotFound();
            }
            return View(serviceBooking);
        }

        // POST: ServiceBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceBooking serviceBooking = db.ServiceBooking.Find(id);
            db.ServiceBooking.Remove(serviceBooking);
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
