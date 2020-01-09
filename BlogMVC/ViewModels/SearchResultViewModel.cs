using BlogMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogMVC.ViewModels
{
    public class SearchResultViewModel
    {
        public string Param { get; set; }
        public IEnumerable<Post> Posts { get; set; }
    }
}