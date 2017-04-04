using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameOfBreak.Models.GoB {

    [Table("DetallesVentas")]
    public class DetallesVentas {

        [Key]
        public int ID_DETALLE_VENTA { get; set; }

        public int ID_VENTA { get; set; }

        public int NumeroTienda { get; set; }

        public string Nombre { get; set; }

        public string PrimerNombreEmpleado { get; set; }

        public string SegundoNombreEmpleado { get; set; }

        public string ApellidoPaternoEmpleado { get; set; }

        public string ApellidoMaternoEmpleado { get; set; }

        public string UsuarioEmpleado { get; set; }

        public string PrimerNombreCliente { get; set; }

        public string SegundoNombreCliente { get; set; }

        public string ApellidoPaternoCliente { get; set; }

        public string ApellidoMaternoCliente { get; set; }

        public string Usuario_Cliente { get; set; }

        public DateTime Fecha { get; set; }

        public string UPC { get; set; }

        public string Titulo { get; set; }

        public string Plataforma { get; set; }

        public int Cantidad { get; set; }

        public decimal Monto { get; set; }

        public decimal SUBTOTAL { get; set; }

    }

}