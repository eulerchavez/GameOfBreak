namespace GameOfBreak.Models.GoB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Genero")]
    public partial class Genero
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Genero()
        {
            //VideoJuego = new HashSet<VideoJuego>();
        }

        [Key]
        public int ID_GENERO { get; set; }

        [Column("Genero")]
        [Required]
        [StringLength(30)]
        [Display(Name = "Género")]
        public string Genero1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<VideoJuego> VideoJuego { get; set; }

        public static IEnumerable<Genero> GetGeneros() {

            IEnumerable<Genero> generos;

            using (var ctx = new GameOfBreakModel()) {

                generos = ctx.Genero.ToList();

            }

            return generos;

        }

    }
}
