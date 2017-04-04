using GameOfBreak.Models.GoB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameOfBreak.Controllers {

    public class HomeController : Controller {

        private GameOfBreakModel _context;

        public HomeController () {
            this._context = new GameOfBreakModel();
        }

        protected override void Dispose (bool disposing) {
            this._context.Dispose();
        }

        public ActionResult Index () {
            return View();
        }

        public ActionResult About () {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact () {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Buscar (string TextoBusqueda) {

            var productos = this._context.Productos.Where(prod => prod.Titulo.Contains(TextoBusqueda) || prod.Descripcion.Contains(TextoBusqueda)).AsEnumerable();

            return View(productos);

        }

    }

}