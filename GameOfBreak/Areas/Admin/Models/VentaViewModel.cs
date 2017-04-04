using GameOfBreak.Models.GoB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameOfBreak.Areas.Admin.Models {

    public class VentaViewModel {

        public Tienda Tienda { get; set; }

        public Usuario Cliente { get; set; }

        public Usuario Empleado { get; set; }

        public Venta Venta { get; set; }

    }

}