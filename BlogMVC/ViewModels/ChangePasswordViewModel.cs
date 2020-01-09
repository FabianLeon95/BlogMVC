using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogMVC.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        [DataType(DataType.Password)]
        [Display(Name = "Antigua Contraseña")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "La {0} es obligatoria.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,}$", ErrorMessage = "La contraseña debe tener al menos 6 caracteres e incluir una minúscula, una mayúscula, un número y un carácter especial.")]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva Contraseña")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        [Compare("NewPassword", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmNewPassword { get; set; }
    }
}