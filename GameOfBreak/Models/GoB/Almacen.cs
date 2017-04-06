namespace GameOfBreak.Models.GoB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Almacen")]
    public partial class Almacen
    {
        [Key]
        public int ID_ALMACEN { get; set; }

        public int ID_TIENDA { get; set; }

        [Required]
        [StringLength(30)]
        public string UPC { get; set; }

        [Display(Name = "Estaus del producto")]
        public int ID_ESTATUS { get; set; }

        [Display(Name = "Descuento")]
        public int? ID_DESCUENTO { get; set; }

        [Range(0, 25)]
        public int Cantidad { get; set; }

        [Range(0, 15000)]
        public decimal Precio { get; set; }

        public virtual Descuento Descuento { get; set; }

        public virtual Estatus Estatus { get; set; }

        public virtual Tienda Tienda { get; set; }

    }

}
