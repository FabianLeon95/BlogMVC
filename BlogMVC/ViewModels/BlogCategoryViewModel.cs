using BlogMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogMVC.ViewModels
{
    public class BlogCategoryViewModel
    {
        public IEnumerable <Category> Categories { get; set; }
        public Category Category { get; set; }
    }
}