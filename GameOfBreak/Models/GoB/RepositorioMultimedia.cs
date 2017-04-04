namespace GameOfBreak.Models.GoB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RepositorioMultimedia")]
    public partial class RepositorioMultimedia
    {
        [Key]
        public int ID_REPOSITORIO_PRODUCTO { get; set; }

        [Required]
        [StringLength(30)]
        public string UPC { get; set; }

        [Required]
        [StringLength(500)]
        public string Ruta { get; set; }

        [Display(Name = "Tipo de multimedia")]
        public int ID_TIPO_MULTIMEDIA { get; set; }

        public TipoMultimedia TipoMultimedia { get; set; }

    }
}
