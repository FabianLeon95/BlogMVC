using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogMVC.ViewModels
{
    public class EditPostViewModel
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

        public string ImagePath { get; set; }
    }
}