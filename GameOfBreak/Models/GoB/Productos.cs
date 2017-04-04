using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace GameOfBreak.Models.GoB {

    [Table("Productos")]
    public partial class Productos {
        [Key]
        [Column(Order = 0)]
        [StringLength(30)]
        public string UPC { get; set; }

        [StringLength(50)]
        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(30)]
        public string Plataforma { get; set; }

        [StringLength(30)]
        public string Desarrolladora { get; set; }

        [StringLength(30)]
        public string Clasificacion { get; set; }

        [StringLength(30)]
        public string Genero { get; set; }

        public int? NumeroJugadores { get; set; }

        public DateTime? FechaSalida { get; set; }
    }
}
