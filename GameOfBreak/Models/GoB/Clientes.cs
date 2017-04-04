using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameOfBreak.Models.GoB {

    [Table("Clientes")]
    public class Clientes {

        [Key]
        public string ID_CLIENTE { get; set; }

        public string UserName { get; set; }

        public string PrimerNombre { get; set; }

        public string SegundoNombre { get; set; }

        public string ApellidoPaterno { get; set; }

        public string ApellidoMaterno { get; set; }

        public string Email { get; set; }

    }

}