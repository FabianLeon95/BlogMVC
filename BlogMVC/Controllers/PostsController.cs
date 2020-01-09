using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlogMVC.Data;
using BlogMVC.Models;
using BlogMVC.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.IO;

namespace BlogMVC.Controllers
{
    public class PostsController : Controller
    {
        private BlogContext db = new BlogContext();

        // GET: Posts
        [Authorize(Roles = "Administrator,Editor")]
        public async Task<ActionResult> Index()
        {
            var posts = db.Posts.Include(p => p.Category);
            return View(await posts.ToListAsync());
        }

        // GET: Posts/Details/5
        [Authorize(Roles = "Administrator,Editor")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Posts.FindAsync(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        [Authorize(Roles = "Administrator,Editor")]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Title");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator,Editor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PostViewModel model)
        {
            if (ModelState.IsValid)
            {
                string fileName = $"{DateTime.Now.Ticks}_{Path.GetFileName(model.ImageFile.FileName)}";
                model.ImageFile.SaveAs(Path.Combine(Server.MapPath("~/Uploads/PostImages"), fileName));

                var post = new Post(model, fileName, User.Identity.GetUserId());

                db.Posts.Add(post);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Title", model.CategoryId);
            return View(model);
        }

        // GET: Posts/Edit/5
        [Authorize(Roles = "Administrator,Editor")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Posts.FindAsync(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            var model = new EditPostViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                Content = post.Content,
                CategoryId = post.CategoryId,
                ImagePath = post.ImagePath
            };

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Title", model.CategoryId);
            return View(model);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator,Editor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Exclude = "ImagePath")] EditPostViewModel model)
        {
            if (ModelState.IsValid)
            {
                Post post = await db.Posts.FindAsync(model.Id);
                post.Title = model.Title;
                post.Description = model.Description;
                post.Content = model.Content;
                post.CategoryId = model.CategoryId;
                post.UpdatedDate = DateTime.Now;

                db.Entry(post).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Title", model.CategoryId);
            return View(model);
        }

        public async Task<ActionResult> EditImage(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Posts.FindAsync(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            var model = new PostImageViewModel { Id = post.Id };
            return View(model);
        }

        [Authorize(Roles = "Administrator,Editor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditImage(PostImageViewModel model)
        {
            if (ModelState.IsValid)
            {
                string fileName = $"{DateTime.Now.Ticks}_{Path.GetFileName(model.ImageFile.FileName)}";
                model.ImageFile.SaveAs(Path.Combine(Server.MapPath("~/Uploads/PostImages"), fileName));

                Post post = await db.Posts.FindAsync(model.Id);
                post.ImagePath = fileName;

                db.Entry(post).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Edit", new { id = model.Id });
            }
            return View(model);
        }

        // GET: Posts/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Posts.FindAsync(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Post post = await db.Posts.FindAsync(id);
            db.Posts.Remove(post);
            await db.SaveChangesAsync();
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
