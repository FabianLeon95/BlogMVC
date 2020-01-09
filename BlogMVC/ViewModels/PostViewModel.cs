using BlogMVC.Helpers;
using BlogMVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace BlogMVC.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El {0} es obligatorio.")]
        [Display(Name = "Título")]
        public string Title { get; set; }

        [Required(ErrorMessage = "La {0} es obligatoria.")]
        [Display(Name = "Descripción")]
        public string Description { get; set; }

        [Required(ErrorMessage = "El {0} es obligatorio.")]
        [Display(Name = "Contenido")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Categoría")]
        public int CategoryId { get; set; }

        [Required]
        [AllowExtensions(Extensions = "jpg,jpeg,png", ErrorMessage = "Seleccione un archivo con formato válido (.jpg, .jpeg o .png).")]
        [Display(Name = "Imagen")]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}