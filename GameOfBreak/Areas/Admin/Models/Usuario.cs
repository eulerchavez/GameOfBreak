using GameOfBreak.Models.GoB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameOfBreak.Areas.Admin.Models {

    public class Usuario {

        [Required]
        public string Id { get; set; }

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

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

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

        [Required]
        public int ID_CP { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de nacimiento")]
        public DateTime? FechaNacimiento { get; set; }

        [StringLength(13)]
        [Display(Name = "Telefono de casa")]
        [DataType(DataType.PhoneNumber)]
        public string TelefonoCasa { get; set; }

        public string Role { get; set; }

    }

}