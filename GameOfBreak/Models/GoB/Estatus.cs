namespace GameOfBreak.Models.GoB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Estatus
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Estatus()
        {
            //Almacen = new HashSet<Almacen>();
        }

        [Key]
        [Display(Name = "Estatus del producto")]
        public int ID_ESTATUS { get; set; }

        [Column("Estatus")]
        [Required]
        [StringLength(30)]
        [Display(Name = "Estatus del producto")]
        public string Estatus1 { get; set; }

        /*
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Almacen> Almacen { get; set; }
        */
    }
}
