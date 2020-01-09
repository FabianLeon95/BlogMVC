using BlogMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogMVC.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Post> posts { get; set; }
        public IEnumerable<Category> categories { get; set; }
    }
}