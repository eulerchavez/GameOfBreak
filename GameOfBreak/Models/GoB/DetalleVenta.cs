namespace GameOfBreak.Models.GoB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DetalleVenta")]
    public partial class DetalleVenta
    {
        [Key]
        public int ID_DETALLE_VENTA { get; set; }

        public int ID_VENTA { get; set; }

        [Required]
        [StringLength(30)]
        public string UPC { get; set; }

        public int Cantidad { get; set; }

        public decimal Monto { get; set; }

        public Venta Venta { get; set; }
    }
}
