namespace GameOfBreak.Models.GoB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TipoMultimedia")]
    public partial class TipoMultimedia
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipoMultimedia()
        {
            //RepositorioMultimedia = new HashSet<RepositorioMultimedia>();
        }

        [Key]
        public int ID_TIPO_MULTIMEDIA { get; set; }

        [Required]
        [StringLength(30)]
        public string Multimedia { get; set; }

        /*
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RepositorioMultimedia> RepositorioMultimedia { get; set; } */
    }
}
