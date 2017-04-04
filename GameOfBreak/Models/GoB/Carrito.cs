namespace GameOfBreak.Models.GoB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Carrito")]
    public partial class Carrito
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Carrito()
        {
            //DetalleCarrito = new HashSet<DetalleCarrito>();
        }

        [Key]
        public int ID_CARRITO { get; set; }

        public int ID_TIENDA { get; set; }

        [Required]
        [StringLength(128)]
        public string ID_USUARIO { get; set; }

        public int ID_TIPO_PAGO { get; set; }

        public DateTime Fecha { get; set; }

        public int? ID_DESCUENTO { get; set; }

        // public AspNetUsers AspNetUsers { get; set; }

        public Descuento Descuento { get; set; }

        public Tienda Tienda { get; set; }

        public TipoPago TipoPago { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<DetalleCarrito> DetalleCarrito { get; set; }

    }

}
