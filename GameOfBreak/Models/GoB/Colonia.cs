namespace GameOfBreak.Models.GoB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Colonia")]
    public partial class Colonia
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Colonia()
        {
            //CodigoPostal = new HashSet<CodigoPostal>();
        }

        [Key]
        public int ID_COLONIA { get; set; }

        [Column("Colonia")]
        [StringLength(150)]
        [Display(Name = "Colonia")]
        public string Colonia1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public ICollection<CodigoPostal> CodigoPostal { get; set; }
    }
}
