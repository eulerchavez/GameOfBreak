namespace GameOfBreak.Models.GoB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DetalleCarrito")]
    public partial class DetalleCarrito
    {
        [Key]
        public int ID_DETALLE_CARRITO { get; set; }

        public int ID_CARRITO { get; set; }

        [Required]
        [StringLength(30)]
        public string UPC { get; set; }

        public int Cantidad { get; set; }

        public decimal Monto { get; set; }

        public virtual Carrito Carrito { get; set; }
    }
}
