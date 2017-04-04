using GameOfBreak.Models.GoB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameOfBreak.Areas.Admin.Models {

    public class DirectorioViewModel {

        public CodigoPostal CodigoPostal { get; set; }

        public IEnumerable<Colonia> Colonias { get; set; }

        public IEnumerable<MunicipioDelegacion> MunicipiosDelegaciones { get; set; }

        public IEnumerable<Estado> Estados { get; set; }

        public IEnumerable<Ciudad> Ciudades { get; set; }

    }

}