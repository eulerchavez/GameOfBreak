using GameOfBreak.Models.GoB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameOfBreak.Areas.Admin.Models {

    public class EmpleadoViewModel {

        public AspNetUsers Usuario { get; set; }

        public Tienda Tienda { get; set; }

        public AspNetRoles Role { get; set; }

    }

}