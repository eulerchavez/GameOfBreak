using GameOfBreak.Models;
using GameOfBreak.Models.GoB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameOfBreak.Areas.Admin.Models {

    public class AlmacenViewModels {

        public Tienda Tienda { get; set; }

        public IEnumerable<AlmacenViewModel> ListadoAlmacen { get; set; }

        public Pager Pager { get; set; }

    }

    public class AlmacenViewModel {

        public Almacen Almacen { get; set; }

        public VideoJuego Videojuego { get; set; }

        public Plataforma Plataforma { get; set; }

        public Accesorio Accesorio { get; set; }

        public Descuento Descuento { get; set; }

        public Estatus Estatus { get; set; }

    }


    public class AlmacenFormViewModel {

        public string UPC { get; set; }

        public Almacen Almacen { get; set; }

        public IEnumerable<Estatus> Estatus { get; set; }

        public IEnumerable<Descuento> Descuentos { get; set; }

    }

}