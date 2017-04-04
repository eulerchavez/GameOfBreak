using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace GameOfBreak.Models.GoB {

    [Table("CarritoTemporal")]
    public partial class CarritoTemporal {

        public CarritoTemporal () {
        }

        [Key]
        public string ID_CARRITO { get; set; }

        public int ID_TIENDA { get; set; }

        [Required]
        [StringLength(128)]
        public string ID_USUARIO { get; set; }

    }

}
