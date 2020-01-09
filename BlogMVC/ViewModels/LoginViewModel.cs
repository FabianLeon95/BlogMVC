using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BlogMVC.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El {0} es obligatorio.")]
        [EmailAddress]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "Recordarme")]
        public bool RememberMe { get; set; }
    }
}