using BlogMVC.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogMVC.ViewModels
{
    public class PostImageViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La {0} es obligatoria.")]
        [AllowExtensions(Extensions = "jpg,jpeg,png", ErrorMessage = "Seleccione un archivo con formato válido (.jpg, .jpeg o .png).")]
        [Display(Name = "Imagen")]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}