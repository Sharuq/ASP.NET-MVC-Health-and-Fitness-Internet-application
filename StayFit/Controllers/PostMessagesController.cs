using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StayFit.Models;

namespace StayFit.Controllers
{
    public class PostMessagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PostMessages
        public ActionResult Index()
        {
            return View(db.PostMessages.ToList());
        }

        // GET: PostMessages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostMessage postMessage = db.PostMessages.Find(id);
            if (postMessage == null)
            {
                return HttpNotFound();
            }
            return View(postMessage);
        }

        // GET: PostMessages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PostMessages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Post_Message_Id,Post_Message")] PostMessage postMessage)
        {
            if (ModelState.IsValid)
            {
                db.PostMessages.Add(postMessage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(postMessage);
        }

        // GET: PostMessages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostMessage postMessage = db.PostMessages.Find(id);
            if (postMessage == null)
            {
                return HttpNotFound();
            }
            return View(postMessage);
        }

        // POST: PostMessages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Post_Message_Id,Post_Message")] PostMessage postMessage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(postMessage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(postMessage);
        }

        // GET: PostMessages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostMessage postMessage = db.PostMessages.Find(id);
            if (postMessage == null)
            {
                return HttpNotFound();
            }
            return View(postMessage);
        }

        // POST: PostMessages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PostMessage postMessage = db.PostMessages.Find(id);
            db.PostMessages.Remove(postMessage);
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
