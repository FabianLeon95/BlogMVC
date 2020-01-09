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
using BlogMVC.App_Start;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using BlogMVC.ViewModels;

namespace BlogMVC.Controllers
{
    public class UsersController : Controller
    {
        private BlogContext db = new BlogContext();
        private ApplicationUserManager _userManager;

        public UsersController()
        {
        }

        public UsersController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Users
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Index()
        {
            var users = from u in db.Users
                        join r in db.Roles
                        on u.Roles.FirstOrDefault().RoleId equals r.Id
                        select new UserViewModel
                        {
                            Id = u.Id,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            Email = u.Email,
                            RoleName = r.Name
                        };

            return View(await users.ToListAsync());
        }

        // GET: Users/Details/5
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            var model = new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                RoleName = db.Roles.Find(user.Roles.FirstOrDefault().RoleId).Name
            };

            return View(model);
        }

        // GET: Users/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            var model = new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                RoleName = db.Roles.Find(user.Roles.FirstOrDefault().RoleId).Name
            };

            ViewBag.RoleName = new SelectList(db.Roles, "Name", "Name", model.RoleName);

            return View(model);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await UserManager.FindByIdAsync(model.Id);
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.UserName = model.Email;

                string oldRole = db.Roles.Find(user.Roles.FirstOrDefault().RoleId).Name;

                IdentityResult result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    UserManager.RemoveFromRole(user.Id, oldRole);
                    UserManager.AddToRole(user.Id, model.RoleName);
                    return RedirectToAction("Index");
                }
                AddErrors(result);
            }
            ViewBag.RoleName = new SelectList(db.Roles, "Name", "Name", model.RoleName);
            return View(model);
        }

        // GET: Users/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            User user = await UserManager.FindByIdAsync(id);
            await UserManager.DeleteAsync(user);

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

        #region Helpers
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        #endregion
    }
}
