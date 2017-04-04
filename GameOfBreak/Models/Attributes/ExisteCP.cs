using GameOfBreak.Models.GoB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameOfBreak.Models.Attributes {

    public class ExisteCP : ValidationAttribute {

        protected override ValidationResult IsValid (object value, ValidationContext validationContext) {

            var codigoPostal = validationContext.ObjectInstance as CodigoPostal;

            if (CodigoPostal.ExisteCP(codigoPostal.ID_CP)) {

                return new ValidationResult("El Codigo Postal ingresado ya existe");

            }

            return ValidationResult.Success;

        }

    }

}