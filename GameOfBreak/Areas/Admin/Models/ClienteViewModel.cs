using GameOfBreak.Models.GoB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameOfBreak.Areas.Admin.Models {

    public class ClienteViewModel {

        public string ID_USUARIO { get; set; }

        public AspNetUsers Cliente { get; set; }

    }

}