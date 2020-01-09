using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogMVC.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        public string Title { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}