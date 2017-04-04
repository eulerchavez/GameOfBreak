using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace GameOfBreak.Models.GoB {

    [Table("DetalleCarritoTemporal")]
    public partial class DetalleCarritoTemporal {

        [Key]
        public int ID_DETALLE_CARRITO { get; set; }

        public string ID_CARRITO { get; set; }

        [Required]
        [StringLength(30)]
        public string UPC { get; set; }

        public int Cantidad { get; set; }

        public decimal Monto { get; set; }

        public CarritoTemporal Carrito { get; set; }

    }

}
