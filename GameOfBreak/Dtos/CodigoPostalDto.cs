using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameOfBreak.Dtos {
    public class CodigoPostalDto {

        [Key]
        public int ID_CP { get; set; }

        [Required]
        [StringLength(8)]
        public string CP { get; set; }

        public int ID_COLONIA { get; set; }

        public int ID_MUNICIPIO_DELEGACION { get; set; }

        public int ID_ESTADO { get; set; }

        public int ID_CIUDAD { get; set; }

        public CiudadDto Ciudad { get; set; }

        public ColoniaDto Colonia { get; set; }

        public EstadoDto Estado { get; set; }

        public MunicipioDelegacionDto MunicipioDelegacion { get; set; }

    }
}