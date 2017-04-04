namespace GameOfBreak.Models.GoB {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Estado")]
    public partial class Estado {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Estado () {
            //CodigoPostal = new HashSet<CodigoPostal>();
        }

        [Key]
        public int ID_ESTADO { get; set; }

        [Column("Estado")]
        [Required]
        [StringLength(45)]
        [Display(Name = "Estado")]
        public string Estado1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public ICollection<CodigoPostal> CodigoPostal { get; set; }
    }
}
