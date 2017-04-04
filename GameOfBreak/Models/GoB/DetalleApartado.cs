namespace GameOfBreak.Models.GoB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DetalleApartado")]
    public partial class DetalleApartado
    {
        [Key]
        public int ID_DETALLE_APARTADO { get; set; }

        public int ID_APARTADO { get; set; }

        [Required]
        [StringLength(30)]
        public string UPC { get; set; }

        public int Cantidad { get; set; }

        public decimal? Monto { get; set; }

        [Display(Name = "Pago inicial")]
        public decimal PagoInicial { get; set; }

        public Apartado Apartado { get; set; }
    }
}
