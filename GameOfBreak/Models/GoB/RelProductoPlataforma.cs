namespace GameOfBreak.Models.GoB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RelProductoPlataforma")]
    public partial class RelProductoPlataforma
    {
        [Key]
        public int ID_REL_PP { get; set; }

        [Required]
        [StringLength(30)]
        public string UPC { get; set; }

        public int ID_PLATAFORMA { get; set; }

        public Plataforma Plataforma { get; set; }
    }
}
