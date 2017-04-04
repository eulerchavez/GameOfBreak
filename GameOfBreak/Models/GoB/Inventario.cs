using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameOfBreak.Models.GoB {

    [Table("Inventario")]
    public class Inventario {

        [Key]
        public int ID_ALMACEN { get; set; }

        public int NumeroTienda { get; set; }

        public string Nombre { get; set; }

        public string UPC { get; set; }

        public string Estatus { get; set; }

        public int Cantidad { get; set; }

        public decimal Precio { get; set; }

        public string Titulo { get; set; }

        public string Plataforma { get; set; }

    }

}