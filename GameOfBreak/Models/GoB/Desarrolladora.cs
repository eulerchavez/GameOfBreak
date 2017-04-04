namespace GameOfBreak.Models.GoB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Desarrolladora")]
    public partial class Desarrolladora
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Desarrolladora()
        {
            //VideoJuego = new HashSet<VideoJuego>();
        }

        [Key]
        public int ID_DESARROLLADORA { get; set; }

        [Column("Desarrolladora")]
        [Required]
        [StringLength(30)]
        [Display(Name = "Desarrolladora")]
        public string Desarrolladora1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<VideoJuego> VideoJuego { get; set; }

    }

}
