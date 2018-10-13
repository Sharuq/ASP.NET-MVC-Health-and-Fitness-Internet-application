using Microsoft.AspNet.Identity;
using StayFit.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace StayFit.Controllers
{
    
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult GymMembersList()
        {
                return View(db.GymMember.ToList());
        }
        // GET: Admin/GymMemberDetails/5
        public ActionResult GymMemberDetails(int id)
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

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET:  Admin/GymMemberEdit/5
        public ActionResult GymMemberEdit(int? id)
        {

            ViewBag.MembershipType = new SelectList(db.MembershipType, "Membership_Id", "Membership_tier");
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

        // POST:  Admin/GymMemberEdit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GymMemberEdit([Bind(Include = "Member_Id,FirstName,LastName,DateOfBirth,Address,Height,Weight,MembershipType")] GymMember gymMember)
        {
            var member_id = gymMember.Member_Id;
            gymMember.ApplicationUser = db.GymMember.Where(p => p.Member_Id == member_id).Select(p => p.ApplicationUser).FirstOrDefault();


            ModelState.Clear();
            TryValidateModel(gymMember);

            if (ModelState.IsValid)
            {
                db.Entry(gymMember).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GymMembersList");
            }
            ViewBag.MembershipType = new SelectList(db.MembershipType, "Membership_Id", "Membership_tier");
            return View(gymMember);
        }




        // GET: Admin/GymMemberDelete/5
        public ActionResult GymMemberDelete(int? id)
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

        // POST: Admin/GymMemberDelete/5
        [HttpPost, ActionName("GymMemberDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            GymMember gymMember = db.GymMember.Find(id);
            var user = gymMember.ApplicationUser;
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("GymMembersList");
        }

        // GET: Posts list
        public ActionResult PostsList()
        {
            return View(db.Posts.ToList());
        }
        // GET: PostMessages/Details/5
        public ActionResult PostDetails(int? id)
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

        // GET: Posts/Create
        public ActionResult CreatePost()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePost([Bind(Include = "post_title,post_message")] PostViewModel postViewModel)
        {

            if (ModelState.IsValid)
            {
                Post post = new Post();
                PostMessage postMessage = new PostMessage();
                post.Post_Title = postViewModel.post_title;
                db.Posts.Add(post);
                db.SaveChanges();
                postMessage.ApplicationUser = db.Users.Find(User.Identity.GetUserId());
                postMessage.Post_Message = postViewModel.post_message;
                postMessage.Post = post;
                db.PostMessages.Add(postMessage);
                db.SaveChanges();
                return RedirectToAction("GymMembersList");
            }

            return View(postViewModel);
        }



    }
}
