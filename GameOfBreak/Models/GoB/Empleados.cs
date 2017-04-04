using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameOfBreak.Models.GoB {

    [Table("Empleados")]
    public class Empleados {

        [Key]
        public string ID_EMPLEADO { get; set; }

        public int NumeroTienda { get; set; }

        public string Nombre { get; set; }

        public string PrimerNombre { get; set; }

        public string SegundoNombre { get; set; }

        public string ApellidoPaterno { get; set; }

        public string ApellidoMaterno { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string ID_ROLE { get; set; }

        public string Puesto { get; set; }

    }

}