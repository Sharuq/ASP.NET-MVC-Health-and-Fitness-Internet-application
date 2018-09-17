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
    public class GymMembersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: GymMembers
        public ActionResult List()
        {
            return View();
        }

        // GET: GymMembers
        public ActionResult Index()
        {
            return View(db.GymMember.ToList());
        }

        // GET: GymMembers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GymMember gymMember = db.GymMember.Find(id);
            if (gymMember == null)
            {
                return HttpNotFound();
            }
            return View(gymMember);
        }

        // GET: GymMembers/UserProfile/
        public ActionResult UserProfile()
        {
            string id = User.Identity.GetUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            GymMember memberProfile = db.GymMember.Where(p => p.ApplicationUser.Id == id).FirstOrDefault();
            if (memberProfile == null)
            {
                return HttpNotFound();
            }
            return View(memberProfile);
        }

        // GET: GymMembers/Create
        public ActionResult Create()
        {
            ViewBag.MembershipType = new SelectList(db.MembershipType, "Membership_Id", "Membership_tier");
            return View();
        }

        // POST: GymMembers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Member_Id,FirstName,LastName,DateOfBirth,Address,Height,Weight,MembershipType")] GymMember gymMember)
        {
            if (ModelState.IsValid)
            {
                gymMember.ApplicationUser = db.Users.Find(User.Identity.GetUserId());
                
                db.GymMember.Add(gymMember);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gymMember);
        }

        // GET: GymMembers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GymMember gymMember = db.GymMember.Find(id);
            if (gymMember == null)
            {
                return HttpNotFound();
            }
            return View(gymMember);
        }

        // POST: GymMembers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Member_Id,FirstName,LastName,DateOfBirth,Address,Height,Weight,MembershipType")] GymMember gymMember)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gymMember).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gymMember);
        }

        // GET: GymMembers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GymMember gymMember = db.GymMember.Find(id);
            if (gymMember == null)
            {
                return HttpNotFound();
            }
            return View(gymMember);
        }

        // POST: GymMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GymMember gymMember = db.GymMember.Find(id);
            db.GymMember.Remove(gymMember);
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
