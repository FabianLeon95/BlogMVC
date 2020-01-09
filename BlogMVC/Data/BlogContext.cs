using BlogMVC.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BlogMVC.Data
{
    public class BlogContext: IdentityDbContext<User>
    {
        public BlogContext() : base("BlogContext") { }

        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }

        public static BlogContext Create()
        {
            return new BlogContext();
        }
    }
}