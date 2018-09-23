using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Blog_Posting;
using Blog_Posting.Helpers;
using Blog_Posting.Models;

namespace Blog_Posting.Controllers
{
    public class BlogPostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BlogPosts

        public ActionResult Index()
        {
            var model = new BlogIndexViewModel();
            model.AllPosts = db.BlogPosts.ToList();
            model.RecentPosts = db.BlogPosts.OrderByDescending(p => p.Created).Take(5).ToList();

            return View(model);
        }

        // GET: BlogPosts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.BlogPosts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }
        public ActionResult DetailSlug(string Slug)
        {
            if (Slug == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.BlogPosts.Where(p => p.Slug == Slug).FirstOrDefault();
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View("Details", blogPost);
        }


        // GET: BlogPosts/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogPosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,Title,Body,MediaURL,Published")] BlogPost blogPost, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (ImageUploadValidator.IsWebFriendlyImage(image))
                {
                    var fileName = Path.GetFileName(image.FileName);
                    image.SaveAs(Path.Combine(Server.MapPath("~/Uploads/"), fileName));
                    blogPost.MediaUrl = "/Uploads/" + fileName;
                }

                var Slug = StringUtilities.URLFriendly(blogPost.Title);
                if (String.IsNullOrWhiteSpace(Slug))
                {
                    ModelState.AddModelError(nameof(BlogPost.Title), "Invalid title");
                    return View(blogPost);
                }
                if ((db.BlogPosts.Any(p => p.Slug == Slug)) || (db.BlogPosts.Any(p => p.Title == blogPost.Title)))
                {
                    ModelState.AddModelError(nameof(BlogPost.Title), "This title is exist");
                    return View(blogPost);
                }
                blogPost.Slug = Slug;
                blogPost.Created = DateTimeOffset.Now;
                db.BlogPosts.Add(blogPost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blogPost);
        }

        // GET: BlogPosts/Edit/5
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.BlogPosts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // POST: BlogPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult Edit([Bind(Include = "Id,Title,Body,MediaUrl,Published")] BlogPost blogPost, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                var blog = db.BlogPosts.Where(p => p.Id == blogPost.Id).FirstOrDefault();
                if (ImageUploadValidator.IsWebFriendlyImage(image))
                {
                    var fileName = Path.GetFileName(image.FileName);
                    image.SaveAs(Path.Combine(Server.MapPath("~/Uploads/"), fileName));
                    blog.MediaUrl = "/Uploads/" + fileName;
                }

                if (blog.Title != blogPost.Title)
                {
                    if ((db.BlogPosts.Any(p => p.Title == blogPost.Title && p.Id != blogPost.Id)))
                    {
                        blogPost.Slug = StringUtilities.URLFriendly(blogPost.Title) + "-" + Guid.NewGuid();
                        blog.Slug = blogPost.Slug;
                    }
                    else
                    {
                        blog.Slug = StringUtilities.URLFriendly(blog.Title);
                    }
                }
                blog.Title = blogPost.Title;
                blog.Body = blogPost.Body;
                blog.Published = blogPost.Published;
                blog.Updated = DateTime.Now;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blogPost);
        }

        // GET: BlogPosts/Delete/5
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.BlogPosts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // POST: BlogPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogPost blogPost = db.BlogPosts.Find(id);
            db.BlogPosts.Remove(blogPost);
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
