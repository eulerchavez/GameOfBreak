using GameOfBreak.Models.GoB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameOfBreak.Models {

    public class ExternalLoginConfirmationViewModel {

        [Required]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }

    }

    public class ExternalLoginListViewModel {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Código")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "¿Recordar este explorador?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel {
        [Required]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }
    }

    public class LoginViewModel {
        [Required]
        [Display(Name = "Usuario")]
        //[EmailAddress]
        public string Usuario { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "¿Recordar cuenta?")]
        public bool RememberMe { get; set; }
    }

    // Contiene los campos relevantes de AspNetUsers
    public class RegisterViewModel {

        [Required]
        [EmailAddress]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Nombre de usuario")]
        public string UserName { get; set; }

        [Required]
        [StringLength(25)]
        [Display(Name = "Primer Nombre")]
        public string PrimerNombre { get; set; }

        [StringLength(50)]
        [Display(Name = "Segundo Nombre")]
        public string SegundoNombre { get; set; }

        [Required]
        [StringLength(25)]
        [Display(Name = "Apellido Paterno")]
        public string ApellidoPaterno { get; set; }

        [StringLength(25)]
        [Display(Name = "Apellido Materno")]
        public string ApellidoMaterno { get; set; }

        [Required]
        [StringLength(30)]
        public string Calle { get; set; }

        [Required]
        [StringLength(5)]
        [Display(Name = "Número Interio")]
        public string NumInt { get; set; }

        [StringLength(5)]
        [Display(Name = "Número Exterior")]
        public string NumExt { get; set; }

        [Display(Name = "Codigo postal")]
        [Required]
        [StringLength(maximumLength: 5, MinimumLength = 5)]
        public string CP { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de nacimiento")]
        public DateTime? FechaNacimiento { get; set; }

        [StringLength(13)]
        [Display(Name = "Telefono de casa")]
        public string TelefonoCasa { get; set; }

        public DateTime FechaAlta { get; set; }

        [Required]
        public string Role { get; set; }

    }

    public class ResetPasswordViewModel {
        [Required]
        [EmailAddress]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel {
        [Required]
        [EmailAddress]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }
    }

    public class RoleViewModel {

        [Required]
        public string Id { get; set; }

        [Required]
        [Display(Name = "Nombre del Role")]
        public string Name { get; set; }

    }

}
