namespace GameOfBreak.Models.GoB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Tienda")]
    public partial class Tienda
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tienda()
        {
            //Almacen = new HashSet<Almacen>();
            //Apartado = new HashSet<Apartado>();
            //Carrito = new HashSet<Carrito>();
            //RelTiendaEmpleado = new HashSet<RelTiendaEmpleado>();
            //Venta = new HashSet<Venta>();
        }

        [Key]
        public int ID_TIENDA { get; set; }

        [Display(Name = "Número de tienda")]
        public int NumeroTienda { get; set; }

        [Required]
        [StringLength(60)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(30)]
        public string Calle { get; set; }

        [Required]
        [StringLength(5)]
        [Display(Name = "Número interior")]
        public string NumInt { get; set; }

        [StringLength(5)]
        [Display(Name = "Número exterior")]
        public string NumExt { get; set; }

        [Display(Name = "Codigo Postal")]
        public int ID_CP { get; set; }

        public CodigoPostal CP { get; set; }

        /*
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Almacen> Almacen { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Apartado> Apartado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Carrito> Carrito { get; set; }

        public CodigoPostal CodigoPostal { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<RelTiendaEmpleado> RelTiendaEmpleado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Venta> Venta { get; set; }
        */
    }
}
