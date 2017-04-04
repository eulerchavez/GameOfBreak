using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameOfBreak.Areas.Admin.Models {

    public class DetalleVentaViewModel {

        public GameOfBreak.Models.GoB.Productos Producto { get; set; }

        public int Cantidad { get; set; }

        public decimal Monto { get; set; }

    }

}