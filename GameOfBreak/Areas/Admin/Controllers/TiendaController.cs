using GameOfBreak.Areas.Admin.Models;
using GameOfBreak.Models;
using GameOfBreak.Models.GoB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;

namespace GameOfBreak.Areas.Admin.Controllers {

    [Authorize(Roles = "Administrador")]
    public class TiendaController : Controller {

        private GameOfBreakModel _context;

        public TiendaController () {
            this._context = new GameOfBreakModel();
        }

        protected override void Dispose (bool disposing) {
            this._context.Dispose();
        }

        // GET: Admin/Tiendas
        public ActionResult Tiendas (int? page, int? count) {

            // Validamos el numero de la pagina
            if (!page.HasValue) {
                page = 1;
            } else if (page <= 0) {
                page = 1;
            }

            // Obtenemos el total de registros
            if (!count.HasValue) {
                count = this._context.Tienda.Count();
            }

            // Creamos el Pager
            var pager = new Pager(count.Value, page, 15);

            var tiendas = this._context.Tienda
                .OrderBy(ti => ti.Nombre)
                .Skip((pager.CurrentPage - 1) * 15).Take(15)
                .Select(t => new TiendaViewModel() {
                    Tienda = t,
                    CP = this._context.CodigoPostal.Where(cp => cp.ID_CP == t.ID_CP).FirstOrDefault()
                })
                .AsEnumerable();

            var viewModel = new PaginationViewModel<TiendaViewModel>
            {
                Items = tiendas,
                Pager = pager
            };

            return View(viewModel);

        }

        public ActionResult EditarTienda (int idTienda) {

            if (idTienda == 1) {
                return HttpNotFound();
            }

            var tienda = this._context.Tienda
                .Where(t => t.ID_TIENDA == idTienda)
                .Select(t => new TiendaViewModel() {
                    Tienda = t,
                    CP = this._context.CodigoPostal
                                      .Where(cp => cp.ID_CP == t.ID_CP)
                                      .FirstOrDefault()
                })
                .FirstOrDefault();

            if (tienda == null) {
                return HttpNotFound();
            }

            var codigosPostales = this._context.CodigoPostal.AsEnumerable();

            tienda.CPs = codigosPostales;

            return View("TiendaForm", tienda);

        }

        public ActionResult AgregarTienda () {

            var tienda = new Tienda();
            var codigoPostal = new CodigoPostal();

            var viewModel = new TiendaViewModel() {
                Tienda = tienda,
                CP = codigoPostal,
                CPs = this._context.CodigoPostal.AsEnumerable()
            };

            return View("TiendaForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GuardarTienda (TiendaViewModel tiendaVW) {

            if (!ModelState.IsValid) {
                tiendaVW.CPs = this._context.CodigoPostal.AsEnumerable();
                return View("TiendaForm", tiendaVW);
            }

            if (tiendaVW.Tienda.ID_TIENDA <= 0) {

                this._context.Tienda.Add(tiendaVW.Tienda);

            } else {

                var tiendaBD = this._context.Tienda.Where(t => t.ID_TIENDA == tiendaVW.Tienda.ID_TIENDA).FirstOrDefault();

                tiendaBD.NumeroTienda = tiendaVW.Tienda.NumeroTienda;
                tiendaBD.Nombre = tiendaVW.Tienda.Nombre;
                tiendaBD.Calle = tiendaVW.Tienda.Calle;
                tiendaBD.NumInt = tiendaVW.Tienda.NumInt;
                tiendaBD.NumExt = tiendaVW.Tienda.NumExt;
                tiendaBD.ID_CP = tiendaVW.Tienda.ID_CP;

            }

            this._context.SaveChanges();

            return RedirectToAction("Tiendas", "Tienda");

        }

    }

}