namespace GameOfBreak.Models.GoB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Descuento")]
    public partial class Descuento
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Descuento()
        {
            /*
            Almacen = new HashSet<Almacen>();
            Apartado = new HashSet<Apartado>();
            Carrito = new HashSet<Carrito>();
            Venta = new HashSet<Venta>();
            */
        }

        [Key]
        [Display(Name = "Descuento")]
        public int ID_DESCUENTO { get; set; }

        [Column("Descuento")]
        [Required]
        [StringLength(50)]
        [Display(Name = "Descuento")]
        public string Descuento1 { get; set; }

        [Display(Name = "Monto de descuento")]
        public decimal MontoDescuento { get; set; }

        [Display(Name = "Fecha de vigencia")]
        public DateTime? FechaVigencia { get; set; }

        /*
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Almacen> Almacen { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Apartado> Apartado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Carrito> Carrito { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Venta> Venta { get; set; }
        */
    }
}
