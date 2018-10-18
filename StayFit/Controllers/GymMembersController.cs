using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StayFit.Models;
using StayFit.Common;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System.IO;

namespace StayFit.Controllers
{   [Authorize]
    public class GymMembersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

      
        
        // GET: GymMembers
        public ActionResult Index()
        {
            string id = User.Identity.GetUserId();
            GymMember memberProfile = db.GymMember.Where(p => p.ApplicationUser.Id == id).FirstOrDefault();
            if (memberProfile == null)
            {
                //return HttpNotFound();
                return RedirectToAction("Create");
            }
            return View();
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
                //return HttpNotFound();
                return RedirectToAction("Create");
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
            gymMember.ApplicationUser = db.Users.Find(User.Identity.GetUserId());
            ModelState.Clear();
            TryValidateModel(gymMember);
            if (ModelState.IsValid)
            {
                gymMember.ApplicationUser = db.Users.Find(User.Identity.GetUserId());
                
                db.GymMember.Add(gymMember);
                db.SaveChanges();
                String toEmail = gymMember.ApplicationUser.Email;
                String subject = "StayFit Registeration Confirmation";
                String contents = String.Empty;
                using (StreamReader reader = new StreamReader(Server.MapPath("~/Email_Template/Email_Contents.html")))
                {
                    contents = reader.ReadToEnd();
                }
                
                EmailSender es = new EmailSender();
                es.Send(toEmail, subject, contents);
                return RedirectToAction("Index");
            }
            ViewBag.MembershipType = new SelectList(db.MembershipType, "Membership_Id", "Membership_tier");
            return View(gymMember);
        }

        


        // GET: GymMembers/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: GymMembers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Member_Id,FirstName,LastName,DateOfBirth,Address,Height,Weight,MembershipType")] GymMember gymMember)
        {
            gymMember.ApplicationUser = db.Users.Find(User.Identity.GetUserId());
            ModelState.Clear();
            TryValidateModel(gymMember);

            if (ModelState.IsValid)
            {
                db.Entry(gymMember).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MembershipType = new SelectList(db.MembershipType, "Membership_Id", "Membership_tier");
            return View(gymMember);
        }

        /// <summary>  
        /// Validate Captcha  
        /// </summary>  
        /// <param name="response"></param>  
        /// <returns></returns>  
        public static CaptchaResponse ValidateCaptcha(string response)
        {
            string secret = System.Web.Configuration.WebConfigurationManager.AppSettings["recaptchaPrivateKey"];
            var client = new WebClient();
            var jsonResult = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));
            return JsonConvert.DeserializeObject<CaptchaResponse>(jsonResult.ToString());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }



        // GET: Posts list
        public ActionResult MemberPostsList()
        {
            return View(db.Posts.ToList());
        }


        // GET: PostMessages/Details/5
        
        public ActionResult MemberPostDetails(int? id)
        {
           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            PostMessageDetailsViewModel postMessageDetailsViewModel = new PostMessageDetailsViewModel();
            var postMessage = db.PostMessages.Where(m => m.Post.Post_Id == id).ToList();
            postMessageDetailsViewModel.postMessages = postMessage;
            postMessageDetailsViewModel.post_id = post.Post_Id;
            if (postMessageDetailsViewModel == null)
            {
                return HttpNotFound();
            }
            return View(postMessageDetailsViewModel);
        }


        // POST: Posts/Detials
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult MemberPostDetails([Bind(Include = "post_message")] PostMessageDetailsViewModel postMessageDetailsViewModel,string btn)
        {
            CaptchaResponse response = ValidateCaptcha(Request["g-recaptcha-response"]);
            if (response.Success)
            {
                if (ModelState.IsValid)
                {
                    int post_id = Convert.ToInt32(btn);
                    PostMessage postMessage = new PostMessage();
                    postMessage.ApplicationUser = db.Users.Find(User.Identity.GetUserId());
                    postMessage.Post_Message = postMessageDetailsViewModel.post_message;
                    Post post = db.Posts.Find(post_id);
                    postMessage.Post = post;
                    db.PostMessages.Add(postMessage);
                    db.SaveChanges();
                    return RedirectToAction("MemberPostDetails");
                }
                else { return View(postMessageDetailsViewModel); }
            }
            else
            {
                return Content("Error From Google ReCaptcha : " + response.ErrorMessage[0].ToString());
            }
        }
    }
}
