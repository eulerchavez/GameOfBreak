namespace GameOfBreak.Models.GoB {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Accesorio")]
    public partial class Accesorio {

        [Required]
        [Key]
        [StringLength(30)]
        public string UPC { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nombre del accesorio")]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Required]
        [Display(Name = "Fecha de salida")]
        [DataType(DataType.Date)]
        public DateTime FechaSalida { get; set; }

    }

}
