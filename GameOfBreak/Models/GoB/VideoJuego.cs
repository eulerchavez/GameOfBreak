namespace GameOfBreak.Models.GoB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VideoJuego")]
    public partial class VideoJuego
    {
        // [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VideoJuego()
        {
            // Rating = new HashSet<Rating>();
        }

        [Key]
        [StringLength(30)]
        public string UPC { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Titulo")]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        [Display(Name = "Desarrolladora")]
        public int ID_DESARROLLADORA { get; set; }

        [Display(Name = "Número de jugadores")]
        public int? NumeroJugadores { get; set; }

        [Display(Name = "Clasificación")]
        public int? ID_CLASIFICACION { get; set; }

        [Display(Name = "Género")]
        public int? ID_GENERO { get; set; }

        [Display(Name = "En Linea")]
        public bool EnLinea { get; set; }

        [Display(Name = "Fecha de salida")]
        [Column(TypeName = "date")]
        public DateTime FechaSalida { get; set; }

        public Clasificacion Clasificacion { get; set; }

        public Desarrolladora Desarrolladora { get; set; }

        public Genero Genero { get; set; }

        // [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        // public virtual ICollection<Rating> Rating { get; set; }

    }
}
