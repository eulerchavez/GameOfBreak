namespace GameOfBreak.Models.GoB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Clasificacion")]
    public partial class Clasificacion
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Clasificacion()
        {
            //VideoJuego = new HashSet<VideoJuego>();
        }

        [Key]
        public int ID_CLASIFICACION { get; set; }

        [Column("Clasificacion")]
        [Required]
        [StringLength(30)]
        [Display(Name = "Clasificación")]
        public string Clasificacion1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<VideoJuego> VideoJuego { get; set; }
    }
}
