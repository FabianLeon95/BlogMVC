using BlogMVC.Helpers;
using BlogMVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogMVC.ViewModels
{
    public class ProfileViewModel
    {
        [Required(ErrorMessage = "El {0} es obligatorio.")]
        [Display(Name = "Nombre")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El {0} es obligatorio.")]
        [Display(Name = "Apellido")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El {0} es obligatorio.")]
        [EmailAddress(ErrorMessage = "Introduzca un {0} válido.")]
        [UniqueEmail(ErrorMessage = "El {0} ya está en uso.")]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }

        public ProfileViewModel() { }
        public ProfileViewModel(User user) {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
        }
    }
}