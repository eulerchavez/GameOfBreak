using GameOfBreak.Models.GoB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameOfBreak.Areas.Admin.Models {

    public class TiendaViewModel {

        public Tienda Tienda { get; set; }

        //public IEnumerable<Tienda> Tiendas { get; set; }

        public CodigoPostal CP { get; set; }

        public IEnumerable<CodigoPostal> CPs { get; set; }

    }

}