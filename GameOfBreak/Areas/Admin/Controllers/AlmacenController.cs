using GameOfBreak.Areas.Admin.Models;
using GameOfBreak.Models;
using GameOfBreak.Models.GoB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameOfBreak.Areas.Admin.Controllers {

    [Authorize]
    public class AlmacenController : Controller {

        private GameOfBreakModel _context;

        public AlmacenController () {
            this._context = new GameOfBreakModel();
        }

        protected override void Dispose (bool disposing) {
            this._context.Dispose();
        }

        // GET: Admin/Almacen
        public ActionResult Almacen () {
            return View();
        }

        public ActionResult VerAlmacen (int? idTienda, int? page, int? count) {

            if (User.IsInRole("Administrador") && !idTienda.HasValue) {

                return RedirectToAction("Tiendas", "Tienda");

            } else if (!idTienda.HasValue) {

                // Se obtiene el idEmpleado
                var idEmpleado = this._context.AspNetUsers
                                                  .Where(usr => usr.UserName.Equals(User.Identity.Name))
                                                  .Select(usr => usr.Id)
                                                  .FirstOrDefault();

                // Se obtiene el idTienda
                idTienda = this._context.RelTiendaEmpleado
                                        .Where(rel => rel.ID_EMPLEADO.Equals(idEmpleado))
                                        .Select(rel => rel.ID_TIENDA)
                                        .FirstOrDefault();
            }

            // Se obtiene la Tienda
            var tienda = this._context.Tienda
                                      .Where(t => t.ID_TIENDA == idTienda)
                                      .FirstOrDefault();

            if (tienda == null) {
                return HttpNotFound("Tienda no existente o el usuario no esta asignado a una tienda.");
            }

            if (!page.HasValue) {
                page = 1;
            } else if (page.Value <= 0) {
                page = 1;
            }

            if (!count.HasValue) {
                count = this._context.Almacen
                    .Where(t => t.ID_TIENDA == idTienda)
                    .Count();
            }

            var pager = new Pager(count.Value, page, 60);

            var listadoAlmacen = this._context.Almacen
                                              .Where(al => al.ID_TIENDA == idTienda)
                                              .OrderBy(al => al.Cantidad)
                                              .Skip((pager.CurrentPage - 1) * 60).Take(60)
                                              .Select(al => new AlmacenViewModel() {
                                                  Almacen = al,
                                                  Videojuego = this._context.VideoJuego.Where(vj => vj.UPC.Equals(al.UPC)).FirstOrDefault(),
                                                  Plataforma = this._context.Plataforma.Where(plat => plat.ID_PLATAFORMA == this._context.RelProductoPlataforma.Where(rel => rel.UPC.Equals(al.UPC)).FirstOrDefault().ID_PLATAFORMA).FirstOrDefault(),
                                                  Accesorio  = this._context.Accesorio.Where(acc => acc.UPC.Equals(al.UPC)).FirstOrDefault(),
                                                  Descuento = this._context.Descuento.Where(desc => desc.ID_DESCUENTO == al.ID_DESCUENTO).FirstOrDefault(),
                                                  Estatus = this._context.Estatus.Where(est => est.ID_ESTATUS == al.ID_ESTATUS).FirstOrDefault()
                                              })
                                              .AsEnumerable();

            var almacenViewModel = new AlmacenViewModels() {
                Tienda = tienda,
                ListadoAlmacen = listadoAlmacen,
                Pager = pager
            };

            return View(almacenViewModel);

        }

        // NUEVO "PRODUCTO EN ALMACEN"
        public ActionResult NuevoRegistroAlmacen (int idTienda) {

            var tienda = this._context.Tienda
                                      .Where(t => t.ID_TIENDA == idTienda)
                                      .FirstOrDefault();

            if (tienda == null) {
                return HttpNotFound();
            }

            var almacen = new AlmacenFormViewModel() {
                UPC = String.Empty,
                Almacen = new Almacen() { ID_TIENDA = idTienda },
                Descuentos = this._context.Descuento.AsEnumerable(),
                Estatus = this._context.Estatus.AsEnumerable()
            };

            return View("AlmacenForm", almacen);

        }

        public ActionResult NuevoRegistroAlmacenDesdeProducto (int idTienda, string upc) {

            var tienda = this._context.Tienda
                                      .Where(t => t.ID_TIENDA == idTienda)
                                      .FirstOrDefault();

            var upcAccesorio = this._context.Accesorio.Where(acc => acc.UPC.Equals(upc)).Select(acc => acc.UPC).FirstOrDefault();
            var upcVideojuego = this._context.VideoJuego.Where(vj => vj.UPC.Equals(upc)).Select(vj => vj.UPC).FirstOrDefault();

            if (tienda == null) {
                return HttpNotFound("Tienda no enontrada");
            } else if (upcAccesorio == null && upcVideojuego == null) {
                return HttpNotFound("Producto no encontrado");
            }

            var almacen = new AlmacenFormViewModel() {
                UPC = String.Empty,
                Almacen = new Almacen() { ID_TIENDA = idTienda, UPC = upc, Cantidad = 10, Precio = 1000 },
                Descuentos = this._context.Descuento.AsEnumerable(),
                Estatus = this._context.Estatus.AsEnumerable()
            };

            return View("AlmacenForm", almacen);

        }

        public ActionResult EditarRegistroAlmacen (int idTienda, string upc) {

            var tienda = this._context.Tienda
                                      .Where(t => t.ID_TIENDA == idTienda)
                                      .FirstOrDefault();

            if (tienda == null) {
                return HttpNotFound();
            }

            var videojuego = this._context.VideoJuego.Where(vj => vj.UPC.Equals(upc)).Select(vj => vj.UPC).FirstOrDefault();
            var accesorio = this._context.Accesorio.Where(vj => vj.UPC.Equals(upc)).Select(vj => vj.UPC).FirstOrDefault();

            if (String.IsNullOrEmpty(videojuego) && String.IsNullOrEmpty(accesorio)) {
                return HttpNotFound();
            }

            var registroAlmacen = this._context.Almacen.Where(al => al.ID_TIENDA == idTienda && al.UPC.Equals(upc)).FirstOrDefault();

            var almacen = new AlmacenFormViewModel() {
                UPC = upc,
                Almacen = registroAlmacen,
                Descuentos = this._context.Descuento.AsEnumerable(),
                Estatus = this._context.Estatus.AsEnumerable()
            };

            return View("AlmacenForm", almacen);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GuardarRegistroAlmacen (AlmacenFormViewModel almacenFormVM) {

            #region Validacion de los campos del Form

            // Se valida que el exista el UPC
            var videojuego = this._context.VideoJuego
                                          .Where(vj => vj.UPC.Equals(almacenFormVM.Almacen.UPC))
                                          .FirstOrDefault();

            var accesorio = this._context.Accesorio
                                          .Where(acc => acc.UPC.Equals(almacenFormVM.Almacen.UPC))
                                          .FirstOrDefault();

            if (videojuego == null && accesorio == null) {

                ModelState.AddModelError("almacen.UPC", "El UPC ingresado no existe, favor de ingresar uno valido.");

                var viewModel = new AlmacenFormViewModel() {
                    UPC = almacenFormVM.UPC,
                    Almacen = almacenFormVM.Almacen,
                    Descuentos = this._context.Descuento.AsEnumerable(),
                    Estatus = this._context.Estatus.AsEnumerable()
                };

                return View("AlmacenForm", viewModel);

            } else if (!ModelState.IsValid) {

                var viewModel = new AlmacenFormViewModel() {
                    UPC = almacenFormVM.UPC,
                    Almacen = almacenFormVM.Almacen,
                    Descuentos = this._context.Descuento.AsEnumerable(),
                    Estatus = this._context.Estatus.AsEnumerable()
                };

                return View("AlmacenForm", viewModel);

            }

            // Se valida que no exista la Relacion UPC (Producto) con la tienda

            // Se obtiene, de ser asi, el registro del almacen
            var almacenBD = this._context.Almacen
                                         .Where(al => al.ID_TIENDA == almacenFormVM.Almacen.ID_TIENDA
                                                   && al.UPC.Equals(almacenFormVM.Almacen.UPC))
                                         .FirstOrDefault();

            // Si ya hay algun registro en el almacen
            if (almacenBD != null) {

                ModelState.AddModelError("almacen.UPC", "El UPC ingresado ya se encuentrea registrado, favor de ingresar uno valido.");

                var viewModel = new AlmacenFormViewModel() {
                    UPC = almacenFormVM.UPC,
                    Almacen = almacenFormVM.Almacen,
                    Descuentos = this._context.Descuento.AsEnumerable(),
                    Estatus = this._context.Estatus.AsEnumerable()
                };

                return View("AlmacenForm", viewModel);

            }

            #endregion

            if (almacenFormVM.Almacen.ID_ALMACEN <= 0) {

                this._context.Almacen.Add(almacenFormVM.Almacen);

            } else {

                almacenBD = this._context.Almacen
                                         .Where(al => al.ID_TIENDA == almacenFormVM.Almacen.ID_TIENDA
                                                   && al.UPC.Equals(almacenFormVM.UPC))
                                         .FirstOrDefault();

                almacenBD.UPC = almacenFormVM.Almacen.UPC;
                almacenBD.ID_ESTATUS = almacenFormVM.Almacen.ID_ESTATUS;
                almacenBD.ID_DESCUENTO = almacenFormVM.Almacen.ID_DESCUENTO;
                almacenBD.Cantidad = almacenFormVM.Almacen.Cantidad;
                almacenBD.Precio = almacenFormVM.Almacen.Precio;

            }

            this._context.SaveChanges();

            return RedirectToAction("VerAlmacen", "Almacen", new { idTienda = almacenFormVM.Almacen.ID_TIENDA });

        }

        public ActionResult EliminarRegistroAlmacen (int idTienda, int idAlmacen) {

            var db = this._context;

            Almacen almacen = db.Almacen.Find(idAlmacen);

            if (almacen == null) {
                return HttpNotFound();
            }

            db.Almacen.Remove(almacen);
            db.SaveChanges();

            return RedirectToAction("VerAlmacen", "Almacen", new { idTienda = idTienda });

        }

    }

}