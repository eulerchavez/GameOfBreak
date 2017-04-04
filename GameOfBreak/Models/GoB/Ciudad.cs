namespace GameOfBreak.Models.GoB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ciudad")]
    public partial class Ciudad
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ciudad()
        {
            //CodigoPostal = new HashSet<CodigoPostal>();
        }

        [Key]
        public int ID_CIUDAD { get; set; }

        [Column("Ciudad")]
        [Required]
        [StringLength(55)]
        [Display(Name = "Ciudad")]
        public string Ciudad1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public  ICollection<CodigoPostal> CodigoPostal { get; set; }
    }
}
