using GameOfBreak.Models;
using GameOfBreak.Models.GoB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using GameOfBreak.Areas.Admin.Models;

namespace GameOfBreak.Areas.Admin.Controllers {

    [Authorize(Roles = "Administrador, Gerente, Subgerente, Vendedor")]
    public class ProductoController : Controller {

        private GameOfBreakModel _context;

        public ProductoController () {
            this._context = new GameOfBreakModel();
        }

        protected override void Dispose (bool disposing) {
            this._context.Dispose();
        }

        #region Videojuego

        // GET: Admin/Producto/Videojuegos
        public ActionResult Videojuegos (int? page, int? count) {

            // Validamos el numero de la pagina
            if (!page.HasValue) {
                page = 1;
            } else if (page <= 0) {
                page = 1;
            }

            // Obtenemos el total de registros
            if (!count.HasValue) {
                count = this._context.VideoJuego.Count();
            }

            // Creamos el Pager
            var pager = new Pager(count.Value, page, 15);

            var videoJuegos = this._context.VideoJuego
                .OrderByDescending(vj => vj.FechaSalida)
                .Skip((pager.CurrentPage - 1) * 15).Take(15)
                .Select((vj) => new VideojuegoViewModel() {
                    UPC = vj.UPC,
                    Videojuego = vj,
                    Plataforma = this._context.Plataforma
                            .Where(p => p.ID_PLATAFORMA == this._context.RelProductoPlataforma
                                                                        .Where(relP => relP.UPC.Equals(vj.UPC))
                                                                        .FirstOrDefault().ID_PLATAFORMA)
                            .FirstOrDefault(),
                    Clasificacion = this._context.Clasificacion.Where(c => c.ID_CLASIFICACION == vj.ID_CLASIFICACION).FirstOrDefault(),
                    Desarrolladora =  this._context.Desarrolladora.Where(d => d.ID_DESARROLLADORA == vj.ID_DESARROLLADORA).FirstOrDefault(),
                    Genero = this._context.Genero.Where(g => g.ID_GENERO == vj.ID_GENERO).FirstOrDefault()

                })
            .ToList();

            var viewModel = new PaginationViewModel<VideojuegoViewModel>
            {
                Items = videoJuegos,
                Pager = pager
            };

            var idEmpleado = this._context.AspNetUsers.Where(usr => usr.UserName.Equals(User.Identity.Name)).Select(usr => usr.Id).FirstOrDefault();

            ViewBag.idTienda = this._context.RelTiendaEmpleado.Where(relT => relT.ID_EMPLEADO.Equals(idEmpleado)).Select(relT => relT.ID_TIENDA).FirstOrDefault();

            return View(viewModel);

        }

        [Authorize(Roles = "Administrador")]
        public ActionResult EditarVideojuego (string upc) {

            var videojuego = this._context.VideoJuego
                .Include(vj => vj.Desarrolladora)
                .Include(vj => vj.Clasificacion)
                .Include(vj => vj.Genero)
                .Select((vj) => new VideojuegoViewModel
                {
                    UPC = vj.UPC,
                    Videojuego = vj,
                    Plataforma = this._context.Plataforma.Where( plat => plat.ID_PLATAFORMA ==
                                (this._context.RelProductoPlataforma.Where(p => p.UPC.Equals(vj.UPC)).FirstOrDefault().ID_PLATAFORMA)).FirstOrDefault()
                })
                .Where(vj => vj.Videojuego.UPC.Equals(upc)).SingleOrDefault();

            if (videojuego == null) {
                return HttpNotFound();
            }

            var plataformas = this._context.Plataforma.AsEnumerable();
            var desarrolladoras = this._context.Desarrolladora.AsEnumerable();
            var clasificaciones = this._context.Clasificacion.AsEnumerable();
            var generos = this._context.Genero.AsEnumerable();

            videojuego.Plataformas = plataformas;
            videojuego.Desarrolladoras = desarrolladoras;
            videojuego.Clasificaciones = clasificaciones;
            videojuego.Generos = generos;

            return View("VideojuegoForm", videojuego);

        }

        [Authorize(Roles = "Administrador")]
        public ActionResult NuevoVideojuego () {

            var videojuego = new VideojuegoViewModel() {

                UPC = String.Empty,
                Videojuego = new VideoJuego(),
                Plataforma = new Plataforma()

            };

            var plataformas = this._context.Plataforma.AsEnumerable();
            var desarrolladoras = this._context.Desarrolladora.AsEnumerable();
            var clasificaciones = this._context.Clasificacion.AsEnumerable();
            var generos = this._context.Genero.AsEnumerable();

            videojuego.Plataformas = plataformas;
            videojuego.Desarrolladoras = desarrolladoras;
            videojuego.Clasificaciones = clasificaciones;
            videojuego.Generos = generos;


            return View("VideojuegoForm", videojuego);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GuardarVideojuego (VideojuegoViewModel videojuegoVM) {

            if (!ModelState.IsValid) {

                var plataformas = this._context.Plataforma.AsEnumerable();
                var desarrolladoras = this._context.Desarrolladora.AsEnumerable();
                var clasificaciones = this._context.Clasificacion.AsEnumerable();
                var generos = this._context.Genero.AsEnumerable();

                videojuegoVM.Plataformas = plataformas;
                videojuegoVM.Desarrolladoras = desarrolladoras;
                videojuegoVM.Clasificaciones = clasificaciones;
                videojuegoVM.Generos = generos;

                return View("VideojuegoForm", videojuegoVM);
            }

            if (String.IsNullOrEmpty(videojuegoVM.UPC)) {

                this._context.VideoJuego.Add(videojuegoVM.Videojuego);

                this._context.RelProductoPlataforma.Add(
                   new RelProductoPlataforma {
                       UPC = videojuegoVM.Videojuego.UPC,
                       ID_PLATAFORMA = videojuegoVM.Plataforma.ID_PLATAFORMA
                   });

            } else {

                var listaVideojuegoBD = this._context.VideoJuego
                        .Where(vj => vj.UPC.StartsWith(videojuegoVM.UPC))
                        .AsEnumerable();

                // Actualizamos el juego en todas las plataformas existentes
                foreach (var videojuegoBD in listaVideojuegoBD) {

                    videojuegoBD.UPC = videojuegoVM.UPC;
                    videojuegoBD.Nombre = videojuegoVM.Videojuego.Nombre;
                    videojuegoBD.Descripcion = videojuegoVM.Videojuego.Descripcion;

                    videojuegoBD.ID_DESARROLLADORA = videojuegoVM.Videojuego.ID_DESARROLLADORA;
                    videojuegoBD.NumeroJugadores = videojuegoVM.Videojuego.NumeroJugadores;
                    videojuegoBD.ID_CLASIFICACION = videojuegoVM.Videojuego.ID_CLASIFICACION;
                    videojuegoBD.ID_GENERO = videojuegoVM.Videojuego.ID_GENERO;

                    videojuegoBD.EnLinea = videojuegoVM.Videojuego.EnLinea;
                    videojuegoBD.FechaSalida = videojuegoVM.Videojuego.FechaSalida;

                }

                var relProdBD = this._context.RelProductoPlataforma
                    .Where(rpp => rpp.UPC.Equals(videojuegoVM.Videojuego.UPC))
                    .SingleOrDefault();

                relProdBD.ID_PLATAFORMA = videojuegoVM.Plataforma.ID_PLATAFORMA;
                relProdBD.UPC = videojuegoVM.Videojuego.UPC;

            }

            this._context.SaveChanges();

            return RedirectToAction("Videojuegos", "Producto");

        }

        #endregion

        #region Accesorio

        public ActionResult Accesorios (int? page, int? count) {

            // Validamos el numero de la pagina
            if (!page.HasValue) {
                page = 1;
            } else if (page <= 0) {
                page = 1;
            }

            // Obtenemos el total de registros
            if (!count.HasValue) {
                count = this._context.Accesorio.Count();
            }

            // Creamos el pager
            var pager = new Pager(count.Value, page, 15);

            var accesorios = this._context.Accesorio
                .OrderByDescending(acc => acc.FechaSalida)
                .Skip((pager.CurrentPage - 1) * 15).Take(15)
                .Select( acc => new AccesorioViewModel {
                    Accesorio = acc,
                    Plataforma = this._context.Plataforma
                                    .Where(plat => plat.ID_PLATAFORMA == this._context.RelProductoPlataforma
                                                                                        .Where(relP => relP.UPC.Equals(acc.UPC))
                                                                                        .FirstOrDefault()
                                                                          .ID_PLATAFORMA)
                                    .FirstOrDefault()
                })
                .AsEnumerable();

            var viewModel = new PaginationViewModel<AccesorioViewModel>
            {
                Items = accesorios,
                Pager = pager,
            };

            var idEmpleado = this._context.AspNetUsers.Where(usr => usr.UserName.Equals(User.Identity.Name)).Select(usr => usr.Id).FirstOrDefault();

            ViewBag.idTienda = this._context.RelTiendaEmpleado.Where(relT => relT.ID_EMPLEADO.Equals(idEmpleado)).Select(relT => relT.ID_TIENDA).FirstOrDefault();

            return View(viewModel);

        }

        [Authorize(Roles = "Administrador")]
        public ActionResult EditarAccesorio (string upc) {

            var accesorio = this._context.Accesorio
                .Where(acc => acc.UPC.Equals(upc))
                .FirstOrDefault();

            if (accesorio == null) {
                return HttpNotFound();
            }

            var consola = this._context.Plataforma.FirstOrDefault();
            var plataformas = this._context.Plataforma.AsEnumerable();

            var viewModel = new AccesorioViewModel() {
                UPC = upc,
                Accesorio = accesorio,
                Plataforma = consola,
                Plataformas = plataformas
            };

            return View("AccesorioForm", viewModel);

        }

        [Authorize(Roles = "Administrador")]
        public ActionResult AgregarAccesorio () {

            var consola = this._context.Plataforma.FirstOrDefault();
            var plataformas = this._context.Plataforma.AsEnumerable();

            var accesorioVM = new AccesorioViewModel() {
                UPC = String.Empty,
                Accesorio = new Accesorio(),
                Plataforma = consola,
                Plataformas = plataformas
            };

            return View("AccesorioForm", accesorioVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GuardarAccesorio (AccesorioViewModel accesorioVM) {

            if (!ModelState.IsValid) {

                var consola = this._context.Plataforma.FirstOrDefault();
                var plataformas = this._context.Plataforma.AsEnumerable();

                accesorioVM.Plataforma = consola;
                accesorioVM.Plataformas = plataformas;

                return View("AccesorioForm", accesorioVM);
            }

            if (String.IsNullOrEmpty(accesorioVM.UPC)) {

                this._context.Accesorio.Add(accesorioVM.Accesorio);

                this._context.RelProductoPlataforma.Add(new RelProductoPlataforma() {
                    UPC = accesorioVM.Accesorio.UPC,
                    ID_PLATAFORMA = accesorioVM.Plataforma.ID_PLATAFORMA
                });

            } else {

                var accesorioBD = this._context.Accesorio
                    .Where(acc => acc.UPC.Equals(accesorioVM.Accesorio.UPC))
                    .FirstOrDefault();

                var relAccPlatBD = this._context.RelProductoPlataforma
                    .Where(rel => rel.UPC.Equals(accesorioVM.Accesorio.UPC))
                    .FirstOrDefault();

                accesorioBD.UPC = accesorioVM.Accesorio.UPC;
                accesorioBD.Nombre = accesorioVM.Accesorio.Nombre;
                accesorioBD.Descripcion = accesorioVM.Accesorio.Descripcion;
                accesorioBD.FechaSalida = accesorioVM.Accesorio.FechaSalida;

                relAccPlatBD.ID_PLATAFORMA = accesorioVM.Plataforma.ID_PLATAFORMA;
                relAccPlatBD.UPC = accesorioVM.Accesorio.UPC;

            }

            this._context.SaveChanges();

            return RedirectToAction("Accesorios", "Producto");

        }

        #endregion

        public ActionResult EditarMultimedia (string upc) {

            var producto = this._context.Productos.Where(prod => prod.UPC.Equals(upc)).FirstOrDefault();

            if (producto == null) {
                return HttpNotFound("No se encuentra el producto especificado.");
            }

            var viewModel = new EditarMultimediaViewModel() {
                Repositorio = new RepositorioMultimedia() {
                    UPC = producto.UPC
                },
                TipoMultimedia = this._context.TipoMultimedia.AsEnumerable()
            };

            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GuardarMultimedia (RepositorioMultimedia repositorio) {

            if (!ModelState.IsValid) {

                var viewModel = new EditarMultimediaViewModel() {
                    Repositorio = repositorio,
                    TipoMultimedia = this._context.TipoMultimedia.AsEnumerable()
                };


                return View("EditarMultimedia", viewModel);
            }

            if (repositorio.ID_REPOSITORIO_PRODUCTO <= 0) {

                this._context.RepositorioMultimedia.Add(repositorio);

            } else {

                var repBD = this._context.RepositorioMultimedia.Where(rep => rep.ID_REPOSITORIO_PRODUCTO == repositorio.ID_REPOSITORIO_PRODUCTO).FirstOrDefault();

                repBD.ID_TIPO_MULTIMEDIA = repositorio.ID_TIPO_MULTIMEDIA;
                repBD.Ruta = repositorio.Ruta;

            }

            this._context.SaveChanges();

            return RedirectToAction("Index", "AdminHome");

        }

    }

}