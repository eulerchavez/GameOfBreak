namespace GameOfBreak.Models.GoB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RelTiendaEmpleado")]
    public partial class RelTiendaEmpleado
    {
        [Key]
        public int ID_REL_TIENDA_EMPLEADO { get; set; }

        public int ID_TIENDA { get; set; }

        [Required]
        [StringLength(128)]
        public string ID_EMPLEADO { get; set; }

        //public virtual AspNetUsers AspNetUsers { get; set; }

        //public virtual Tienda Tienda { get; set; }
    }
}
