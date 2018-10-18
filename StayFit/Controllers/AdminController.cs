using Microsoft.AspNet.Identity;
using StayFit.Common;
using StayFit.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace StayFit.Controllers
{
    [Authorize(Roles = "Admin")]
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
        public ActionResult PostDetails([Bind(Include = "post_message")] PostMessageDetailsViewModel postMessageDetailsViewModel, string btn, HttpPostedFileBase postedFile)
        {
            var myUniqueFileName = string.Format(@"{0}", Guid.NewGuid());
            Image image = new Image();
            if (ModelState.IsValid)
            {
                int post_id = Convert.ToInt32(btn);
                PostMessage postMessage = new PostMessage();
                postMessage.ApplicationUser = db.Users.Find(User.Identity.GetUserId());
                postMessage.Post_Message = postMessageDetailsViewModel.post_message;
                Post post = db.Posts.Find(post_id);
                postMessage.Post = post;
                if (postedFile != null)
                {
                    image.Image_Path = myUniqueFileName;
                    string serverPath = Server.MapPath("~/Uploads/");
                    string fileExtension = Path.GetExtension(postedFile.FileName);
                    string filePath = image.Image_Path + fileExtension;
                    image.Image_Path = filePath;
                    postedFile.SaveAs(serverPath + image.Image_Path);
                    db.Images.Add(image);
                    db.SaveChanges();
                    postMessage.Image = image;
                }
                db.PostMessages.Add(postMessage);
                db.SaveChanges();
                return RedirectToAction("PostDetails");
            }

            return View(postMessageDetailsViewModel);
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
        public ActionResult CreatePost([Bind(Include = "post_title,post_message")] PostViewModel postViewModel, HttpPostedFileBase postedFile)
        {
            var myUniqueFileName = string.Format(@"{0}", Guid.NewGuid());
            Image image = new Image();
           

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
                if (postedFile != null)
                {
                    image.Image_Path = myUniqueFileName;
                    string serverPath = Server.MapPath("~/Uploads/");
                    string fileExtension = Path.GetExtension(postedFile.FileName);
                    string filePath = image.Image_Path + fileExtension;
                    image.Image_Path = filePath;
                    postedFile.SaveAs(serverPath + image.Image_Path);
                    db.Images.Add(image);
                    db.SaveChanges();
                    postMessage.Image = image;
                }
                db.PostMessages.Add(postMessage);
                db.SaveChanges();
                return RedirectToAction("PostsList");
            }

            return View(postViewModel);
        }

        // GET: Posts/Delete/5
        public ActionResult DeletePost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("DeletePost")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePostConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("PostsList");
        }

        // GET: Posts/DisapprovePostMessage/5
        public ActionResult DisapprovePostMessage(int? id)
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

        // POST: Posts/DisapprovePostMessage/5
        [HttpPost, ActionName("DisapprovePostMessage")]
        [ValidateAntiForgeryToken]
        public ActionResult DisapprovePostMessageConfirmed(int id)
        {
            PostMessage postMessage = db.PostMessages.Find(id);
            postMessage.Message_Status = true;
            db.Entry(postMessage).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("PostDetails", new { id = postMessage.Post.Post_Id });
        }


        // GET: Posts/ApprovePostMessage/5
        public ActionResult ApprovePostMessage(int? id)
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

        // POST: Posts/ApprovePostMessage/5
        [HttpPost, ActionName("ApprovePostMessage")]
        [ValidateAntiForgeryToken]
        public ActionResult ApprovePostMessageConfirmed(int id)
        {
            PostMessage postMessage = db.PostMessages.Find(id);
            postMessage.Message_Status = false;
            db.Entry(postMessage).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("PostDetails", new { id = postMessage.Post.Post_Id });
        }


        // GET: Posts/DeletePostMessage/5
        public ActionResult DeletePostMessage(int? id)
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

        // POST: Posts/DeletePostMessage/5
        [HttpPost, ActionName("DeletePostMessage")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePostMessageConfirmed(int id)
        {
            PostMessage postMessage = db.PostMessages.Find(id);
            int post_id = postMessage.Post.Post_Id;
            db.PostMessages.Remove(postMessage);
            db.SaveChanges();
            return RedirectToAction("PostDetails", new { id = post_id });
        }

        // GET: PostMessages/EditPostMessage/5
        public ActionResult EditPostMessage(int? id)
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
            PostMessageViewModel postMessageViewModel = new PostMessageViewModel();
            postMessageViewModel.post_message_id = postMessage.Post_Message_Id;
            postMessageViewModel.post_message = postMessage.Post_Message;
            int message_id = postMessage.Post_Message_Id;
            int post_id = db.PostMessages.Where(p => p.Post_Message_Id == message_id).Select(p => p.Post.Post_Id).FirstOrDefault();
            Post post = db.Posts.Find(post_id);
            //post.Post_Title = postMessageViewModel.post_title;
            postMessageViewModel.post_title = post.Post_Title;


            return View(postMessageViewModel);
        }

        // POST: PostMessages/EditPostMessage/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPostMessage([Bind(Include = "post_title,post_message,post_message_id")] PostMessageViewModel postMessageViewModel)
        {
            int message_id = postMessageViewModel.post_message_id;

            PostMessage postMessage = new PostMessage();
            postMessage.Post_Message = postMessageViewModel.post_message;
            postMessage.Post_Message_Id = postMessageViewModel.post_message_id;
            postMessage.ApplicationUser = db.PostMessages.Where(p => p.Post_Message_Id == message_id).Select(p => p.ApplicationUser).FirstOrDefault();
            postMessage.Post = db.PostMessages.Where(p => p.Post_Message_Id == message_id).Select(p => p.Post).FirstOrDefault();
            postMessageViewModel.post_title = postMessage.Post.Post_Title;
            ModelState.Clear();
            TryValidateModel(postMessageViewModel);
            if (ModelState.IsValid)
            {

                db.Entry(postMessage).State = EntityState.Modified;
                db.SaveChanges();
               // return RedirectToAction("PostDetails");
                return RedirectToAction("PostDetails", new { id = postMessage.Post.Post_Id });
            }
            return View(postMessageViewModel);
        }




        public ActionResult Newsletter()
        {
            return View(new NewsletterViewModel());
        }

        [HttpPost]
        public ActionResult Newsletter(NewsletterViewModel newsletterViewModel)
        {
            var gymMembers = db.GymMember.ToList();
            if (ModelState.IsValid)
            {
                foreach (var x in gymMembers)
                {
                    
                        String toEmail = x.ApplicationUser.Email;
                        String subject = newsletterViewModel.News_title;
                        String contents = newsletterViewModel.News_content;
                    String contents1 = String.Empty;
                    using (StreamReader reader = new StreamReader(Server.MapPath("~/Email_Template/Newsletter_Contents.html")))
                    {
                        contents1 = reader.ReadToEnd();
                    }
                    contents1 = contents1.Replace("CONTENTS", contents);
                    //contents = contents1 + contents ;
                    EmailSender es = new EmailSender();
                        es.Send(toEmail, subject, contents1);

                        ViewBag.Result = "Newsletter has been send.";

                        
  
                }
                ModelState.Clear();
                return View(new NewsletterViewModel());
            }

            return View();
        }

    }
}
