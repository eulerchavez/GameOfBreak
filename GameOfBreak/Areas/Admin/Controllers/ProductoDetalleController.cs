using GameOfBreak.Models;
using GameOfBreak.Models.GoB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameOfBreak.Areas.Admin.Controllers {

    [Authorize(Roles = "Administrador")]
    public class ProductoDetalleController : Controller {

        private GameOfBreakModel _context;

        public ProductoDetalleController () {
            this._context = new GameOfBreakModel();
        }

        protected override void Dispose (bool disposing) {
            this._context.Dispose();
        }

        #region Desarrolladora

        // GET: Admin/ProductoDetalle/Desarrolladoras
        public ActionResult Desarrolladoras (int? page, int? count) {

            // Validamos el numero de la pagina
            if (!page.HasValue) {
                page = 1;
            } else if (page <= 0) {
                page = 1;
            }

            // Obtenemos el total de registros
            if (!count.HasValue) {
                count = this._context.Desarrolladora.Count();
            }

            // Creamos el pager
            var pager = new Pager(count.Value, page, 6);

            var desarrolladoras = this._context.Desarrolladora
                .OrderBy(des => des.Desarrolladora1)
                .Skip((pager.CurrentPage - 1) * 6).Take(6)
                .AsEnumerable();

            var viewModel = new PaginationViewModel<Desarrolladora>
            {
                Items = desarrolladoras,
                Pager = pager,
            };

            return View(viewModel);

        }

        public ActionResult EditarDesarrolladora (int idDesarrolladora) {

            var desarrolladora = this._context.Desarrolladora
                .Where(des => des.ID_DESARROLLADORA == idDesarrolladora)
                .FirstOrDefault();

            if (desarrolladora == null) {
                return HttpNotFound();
            }

            return View("DesarrolladoraForm", desarrolladora);
        }

        public ActionResult AgregarDesarrolladora () {

            var desarrolladora = new Desarrolladora();

            return View("DesarrolladoraForm", desarrolladora);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GuardarDesarrolladora (Desarrolladora desarrolladora) {

            if (!ModelState.IsValid) {
                return View("DesarrolladoraForm", desarrolladora);
            }

            if (desarrolladora.ID_DESARROLLADORA <= 0) {

                this._context.Desarrolladora.Add(desarrolladora);

            } else {

                var desarrolladoraBD = this._context.Desarrolladora
                                            .Where(des => des.ID_DESARROLLADORA == desarrolladora.ID_DESARROLLADORA).
                                            FirstOrDefault();

                desarrolladoraBD.Desarrolladora1 = desarrolladora.Desarrolladora1;

            }

            this._context.SaveChanges();

            return RedirectToAction("Desarrolladoras", "ProductoDetalle");
        }

        #endregion

        #region Plataforma

        public ActionResult Plataformas (int? page, int? count) {

            // Validamos el numero de la pagina
            if (!page.HasValue) {
                page = 1;
            } else if (page <= 0) {
                page = 1;
            }

            // Obtenemos el total de registros
            if (!count.HasValue) {
                count = this._context.Plataforma.Count();
            }

            // Creamos el pager
            var pager = new Pager(count.Value, page, 15);

            var plataformas = this._context.Plataforma
                .OrderBy(des => des.Plataforma1)
                .Skip((pager.CurrentPage - 1) * 15).Take(15)
                .AsEnumerable();

            var viewModel = new PaginationViewModel<Plataforma>
            {
                Items = plataformas,
                Pager = pager,
            };

            return View(viewModel);

        }

        public ActionResult EditarPlataforma (int idPlataforma) {

            var plataforma = this._context.Plataforma
                .Where(plat => plat.ID_PLATAFORMA == idPlataforma)
                .FirstOrDefault();

            if (plataforma == null) {
                return HttpNotFound();
            }

            return View("PlataformaForm", plataforma);
        }

        public ActionResult AgregarPlataforma () {

            var plataforma = new Plataforma();

            return View("PlataformaForm", plataforma);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GuardarPlataforma (Plataforma plataforma) {

            if (!ModelState.IsValid) {
                return View("PlataformaForm", plataforma);
            }

            if (plataforma.ID_PLATAFORMA <= 0) {

                this._context.Plataforma.Add(plataforma);

            } else {

                var plataformaBD = this._context.Plataforma
                                            .Where(plat => plat.ID_PLATAFORMA == plataforma.ID_PLATAFORMA).
                                            FirstOrDefault();

                plataformaBD.Plataforma1 = plataforma.Plataforma1;

            }

            this._context.SaveChanges();

            return RedirectToAction("Plataformas", "ProductoDetalle");

        }

        #endregion

        #region Clasificacion

        public ActionResult Clasificaciones (int? page, int? count) {

            // Validamos el numero de la pagina
            if (!page.HasValue) {
                page = 1;
            } else if (page <= 0) {
                page = 1;
            }

            // Obtenemos el total de registros
            if (!count.HasValue) {
                count = this._context.Clasificacion.Count();
            }

            // Creamos el pager
            var pager = new Pager(count.Value, page, 15);

            var clasificaciones = this._context.Clasificacion
                .OrderBy(clas => clas.Clasificacion1)
                .Skip((pager.CurrentPage - 1) * 15).Take(15)
                .AsEnumerable();

            var viewModel = new PaginationViewModel<Clasificacion>
            {
                Items = clasificaciones,
                Pager = pager,
            };

            return View(viewModel);

        }

        public ActionResult EditarClasificacion (int idClasificacion) {

            var clasificacion = this._context.Clasificacion
                .Where(clas => clas.ID_CLASIFICACION == idClasificacion)
                .FirstOrDefault();

            if (clasificacion == null) {
                return HttpNotFound();
            }

            return View("ClasificacionForm", clasificacion);
        }

        public ActionResult AgregarClasificacion () {

            var clasificacion = new Clasificacion();

            return View("ClasificacionForm", clasificacion);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GuardarClasificacion (Clasificacion clasificacion) {

            if (!ModelState.IsValid) {
                return View("ClasificacionForm", clasificacion);
            }

            if (clasificacion.ID_CLASIFICACION <= 0) {

                this._context.Clasificacion.Add(clasificacion);

            } else {

                var clasificacionBD = this._context.Clasificacion
                                            .Where(clas => clas.ID_CLASIFICACION == clasificacion.ID_CLASIFICACION).
                                            FirstOrDefault();

                clasificacionBD.Clasificacion1 = clasificacion.Clasificacion1;

            }

            this._context.SaveChanges();

            return RedirectToAction("Clasificaciones", "ProductoDetalle");

        }

        #endregion

        #region Genero

        public ActionResult Generos (int? page, int? count) {

            // Validamos el numero de la pagina
            if (!page.HasValue) {
                page = 1;
            } else if (page <= 0) {
                page = 1;
            }

            // Obtenemos el total de registros
            if (!count.HasValue) {
                count = this._context.Genero.Count();
            }

            // Creamos el pager
            var pager = new Pager(count.Value, page, 15);

            var generos = this._context.Genero
                .OrderBy(gen => gen.Genero1)
                .Skip((pager.CurrentPage - 1) * 15).Take(15)
                .AsEnumerable();

            var viewModel = new PaginationViewModel<Genero>
            {
                Items = generos,
                Pager = pager,
            };

            return View(viewModel);

        }

        public ActionResult EditarGenero (int idGenero) {

            var genero = this._context.Genero
                .Where(gen => gen.ID_GENERO == idGenero)
                .FirstOrDefault();

            if (genero == null) {
                return HttpNotFound();
            }

            return View("GeneroForm", genero);
        }

        public ActionResult AgregarGenero () {

            var genero = new Genero();

            return View("GeneroForm", genero);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GuardarGenero (Genero genero) {

            if (!ModelState.IsValid) {
                return View("GeneroForm", genero);
            }

            if (genero.ID_GENERO <= 0) {

                this._context.Genero.Add(genero);

            } else {

                var generoBD = this._context.Genero
                                            .Where(gen => gen.ID_GENERO == genero.ID_GENERO).
                                            FirstOrDefault();

                generoBD.Genero1 = genero.Genero1;

            }

            this._context.SaveChanges();

            return RedirectToAction("Generos", "ProductoDetalle");

        }

        #endregion
    }
}