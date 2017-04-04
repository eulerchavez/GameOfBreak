namespace GameOfBreak.Models.GoB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MunicipioDelegacion")]
    public partial class MunicipioDelegacion
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MunicipioDelegacion()
        {
            //CodigoPostal = new HashSet<CodigoPostal>();
        }

        [Key]
        public int ID_MUNICIPIO_DELEGACION { get; set; }

        [Column("MunicipioDelegacion")]
        [Required]
        [StringLength(55)]
        [Display(Name = "Municipio o Delegación")]
        public string MunicipioDelegacion1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public ICollection<CodigoPostal> CodigoPostal { get; set; }
    }
}
