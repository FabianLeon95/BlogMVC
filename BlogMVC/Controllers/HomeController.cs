using BlogMVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using BlogMVC.Models;
using BlogMVC.ViewModels;
using Microsoft.AspNet.Identity;

namespace BlogMVC.Controllers
{
    public class HomeController : Controller
    {
        private BlogContext db = new BlogContext();

        // GET: Home
        public async Task<ActionResult> Index()
        {
            var posts = db.Posts.Include(p => p.Category).OrderByDescending(p => p.CreatedDate);
            var categories = db.Categories;
            var model = new IndexViewModel
            {
                posts = await posts.ToListAsync(),
                categories = await categories.ToListAsync()
            };

            return View(model);
        }

        public async Task<ActionResult> Post(int? id)
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

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Comment(int? id, CommentViewModel model)
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

            var comment = new Comment
            {
                Content = model.Content,
                UserId = User.Identity.GetUserId(),
                PostId = post.Id,
                CreatedDate = DateTime.Now
            };

            db.Comments.Add(comment);
            await db.SaveChangesAsync();

            return RedirectToAction("Post", new { id = post.Id });
        }

        public async Task<ActionResult> Category(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            var categories = db.Categories;

            var model = new BlogCategoryViewModel
            {
                Category = category,
                Categories = categories
            };

            return View(model);
        }

        public async Task<ActionResult> Search(SearchViewModel model)
        {

            if (ModelState.IsValid)
            {
                var posts = db.Posts.Where(p => p.Title.Contains(model.Param) || p.Description.Contains(model.Param) || p.Category.Title.Contains(model.Param));

                return View(new SearchResultViewModel { Param = model.Param, Posts = await posts.ToListAsync() });
            }

            return RedirectToAction("Index");

        }

        [ChildActionOnly]
        public ActionResult CategoriesList()
        {
            var categories = db.Categories;

            return PartialView("_CategoriesList", categories.ToList());
        }

        [ChildActionOnly]
        public ActionResult SearchForm()
        {
            return PartialView("_SearchForm");
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