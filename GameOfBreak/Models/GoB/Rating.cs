namespace GameOfBreak.Models.GoB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Rating")]
    public partial class Rating
    {
        [Key]
        public int ID_RATING { get; set; }

        [Required]
        [StringLength(30)]
        public string UPC { get; set; }

        public int? Vontantes { get; set; }

        public decimal? Estrellas { get; set; }

        public decimal? Promedio { get; set; }

        public virtual VideoJuego VideoJuego { get; set; }
    }
}
