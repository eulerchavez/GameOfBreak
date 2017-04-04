using GameOfBreak.Models.GoB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameOfBreak.Areas.Admin.Models {

    public class AccesorioViewModel {

        public string UPC { get; set; }

        public Accesorio Accesorio { get; set; }

        public Plataforma Plataforma { get; set; }

        public IEnumerable<Plataforma> Plataformas { get; set; }

    }

}