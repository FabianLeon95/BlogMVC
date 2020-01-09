using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogMVC.ViewModels
{
    public class SearchViewModel
    {
        [Required]
        public string Param { get; set; }
    }
}