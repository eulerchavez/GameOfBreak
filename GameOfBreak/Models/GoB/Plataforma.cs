namespace GameOfBreak.Models.GoB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Plataforma")]
    public partial class Plataforma
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Plataforma()
        {
            //RelProductoPlataforma = new HashSet<RelProductoPlataforma>();
        }

        [Key]
        [Display(Name = "Plataforma")]
        public int ID_PLATAFORMA { get; set; }

        [Column("Plataforma")]
        [Required]
        [StringLength(30)]
        [Display(Name = "Plataforma")]
        public string Plataforma1 { get; set; }

        public bool Activo { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public ICollection<RelProductoPlataforma> RelProductoPlataforma { get; set; }

        public static IEnumerable<Plataforma> GetPlataformas() {

            IEnumerable<Plataforma> plataformas;

            using (var ctx = new GameOfBreakModel()) {

                plataformas = ctx.Plataforma.ToList();

            }

            return plataformas;

        }

    }

}
