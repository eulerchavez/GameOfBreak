namespace GameOfBreak.Models.GoB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Apartado")]
    public partial class Apartado
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Apartado()
        {
            //DetalleApartado = new HashSet<DetalleApartado>();
        }

        [Key]
        public int ID_APARTADO { get; set; }

        public int ID_TIENDA { get; set; }

        [Required]
        [StringLength(128)]
        public string ID_EMPLEADO { get; set; }

        [Required]
        [StringLength(128)]
        public string ID_USUARIO { get; set; }

        public DateTime Fecha { get; set; }

        public int ID_TIPO_PAGO { get; set; }

        public int? ID_DESCUENTO { get; set; }

        public Descuento Descuento { get; set; }

        public AspNetUsers AspNetUsers { get; set; }

        public Tienda Tienda { get; set; }

        public TipoPago TipoPago { get; set; }

        public AspNetUsers AspNetUsers1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<DetalleApartado> DetalleApartado { get; set; }
    }
}
