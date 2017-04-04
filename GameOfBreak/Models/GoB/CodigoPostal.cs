namespace GameOfBreak.Models.GoB
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Attributes;

    [Table("CodigoPostal")]
    public partial class CodigoPostal
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CodigoPostal()
        {
            //AspNetUsers = new HashSet<AspNetUsers>();
            //Tienda = new HashSet<Tienda>();
        }

        [Key]
        public int ID_CP { get; set; }

        [Required]
        [StringLength(8)]
        [Display(Name = "Codigo Postal")]
        public string CP { get; set; }

        [Display(Name = "Colonia")]
        public int ID_COLONIA { get; set; }

        [Display(Name = "Municipio o Delegación")]
        public int ID_MUNICIPIO_DELEGACION { get; set; }

        [Display(Name = "Estado")]
        public int ID_ESTADO { get; set; }

        [Display(Name = "Ciudad")]
        public int ID_CIUDAD { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public ICollection<AspNetUsers> AspNetUsers { get; set; }

        public Ciudad Ciudad { get; set; }

        public Colonia Colonia { get; set; }

        public Estado Estado { get; set; }

        public MunicipioDelegacion MunicipioDelegacion { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public ICollection<Tienda> Tienda { get; set; }

        public static bool ExisteCP (int idCP) {

            CodigoPostal codigoPostal;

            using (var ctx = new GameOfBreakModel()) {

                codigoPostal = ctx.CodigoPostal.Where(cp => cp.ID_CP == idCP).SingleOrDefault();

            }

            return codigoPostal != null;

        }

    }
}
