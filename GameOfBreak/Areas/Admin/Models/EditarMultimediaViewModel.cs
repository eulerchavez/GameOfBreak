using GameOfBreak.Models.GoB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameOfBreak.Areas.Admin.Models {
    public class EditarMultimediaViewModel {

        public RepositorioMultimedia Repositorio { get; set; }

        public IEnumerable<TipoMultimedia> TipoMultimedia { get; set; }

    }

}