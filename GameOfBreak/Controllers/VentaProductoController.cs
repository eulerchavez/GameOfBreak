using GameOfBreak.Areas.Admin.Models;
using GameOfBreak.Models;
using GameOfBreak.Models.GoB;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameOfBreak.Controllers {

    public class VentaProductoController : Controller {

        private GameOfBreakModel _context;

        public VentaProductoController() {
            this._context = new GameOfBreakModel();
        }

        protected override void Dispose(bool disposing) {
            this._context = new GameOfBreakModel();
        }

        #region Videojuegos

        // GET: Producto
        public ActionResult Videojuegos(int idConsola, int? idGenero, int? page, int? count) {

            var existeConsola = this._context.Plataforma
                                        .Where(plat => plat.ID_PLATAFORMA == idConsola)
                                        .FirstOrDefault();

            if (existeConsola == null) {
                return HttpNotFound("No existe la consola especificada");
            }

            // Validamos el numero de la pagina
            if (!page.HasValue) {
                page = 1;
            } else if (page <= 0) {
                page = 1;
            }

            // Se obtienen los UPC de los productos con los que cuenta la tienda En Linea en su almacen
            var upc = this._context.Almacen.Where(almacen => almacen.ID_TIENDA == 1).Select(almacen => almacen.UPC).AsEnumerable();

            // Se seleccionan aquellos que son solamente videojuegos
            var listaUpcVideojuegos = this._context.VideoJuego.Where(vj => upc.Contains(vj.UPC))
                                                              .Select(vj => vj.UPC)
                                                              .ToList();

            // Se selecciona aquellos que son de la plataforma especificada
            IEnumerable<string> upcProductos = this._context.RelProductoPlataforma.Where(rel => rel.ID_PLATAFORMA == idConsola
                                                                            && listaUpcVideojuegos.Contains(rel.UPC))
                                                                 .Select(rel => rel.UPC)
                                                                 .ToList();

            count = upcProductos.Count();

            // Obtenemos el total de registros
            //if (!count.HasValue) {
            //}

            // Creamos el Pager
            var pager = new Pager(count.Value, page, 6);

            //Obtenemos el detalle de cada producto, en este caso, videojuegos

            var videojuegos = this._context.VideoJuego
                                           .Include(vj => vj.Clasificacion)
                                           .Include(vj => vj.Genero)
                                           .Include(vj => vj.Desarrolladora)
                                           .Where(videojuego => upcProductos.Contains(videojuego.UPC))
                                           .OrderByDescending(videojuego => videojuego.FechaSalida)
                                           .Skip((pager.CurrentPage - 1) * 6)
                                           .Take(6)
                                           //.Select(videojuego => new ProductoVentaViewModel() {
                                           //    UPC = videojuego.UPC,
                                           //    Nombre = videojuego.Nombre,
                                           //    Estatus = this._context.Estatus.Where(estatus => estatus.ID_ESTATUS == (this._context.Almacen
                                           //                                                                                         .Where(almacen => almacen.UPC.Equals(videojuego.UPC)
                                           //                                                                                                        && almacen.ID_TIENDA == 1)
                                           //                                                                                         .Select(almacen => almacen.ID_ESTATUS)
                                           //                                                                                         .FirstOrDefault()))
                                           //                                   .FirstOrDefault(),
                                           //    Precio = this._context.Almacen.Where(almacen => almacen.UPC.Equals(videojuego.UPC) && almacen.ID_TIENDA == 1).Select(almacen => almacen.Precio).FirstOrDefault(),
                                           //    Clasificacion = videojuego.Clasificacion.Clasificacion1,
                                           //    Genero = videojuego.Genero.Genero1,
                                           //    Desarrolladora = videojuego.Desarrolladora.Desarrolladora1,
                                           //    FechaSalida = videojuego.FechaSalida,
                                           //    Ruta = this._context.RepositorioMultimedia.Where(rep => rep.ID_TIPO_MULTIMEDIA == 1 && rep.UPC.Equals(videojuego.UPC)).FirstOrDefault().Ruta
                                           //})
                                           .ToList();

            var vjSelectos = videojuegos.Select(vj => new ProductoVentaViewModel() {
                UPC = vj.UPC,
                Nombre = vj.Nombre,
                Estatus = this._context.Almacen.Include(a => a.Estatus).FirstOrDefault(a => a.UPC.Equals(vj.UPC) && a.ID_TIENDA == 1).Estatus.Estatus1,
                Precio = this._context.Almacen.FirstOrDefault(a => a.UPC.Equals(vj.UPC) && a.ID_TIENDA == 1).Precio,
                Clasificacion = vj.Clasificacion.Clasificacion1,
                Genero = vj.Genero.Genero1,
                Desarrolladora = vj.Desarrolladora.Desarrolladora1,
                FechaSalida = vj.FechaSalida,
                Ruta = this._context.RepositorioMultimedia.FirstOrDefault(rep => rep.ID_TIPO_MULTIMEDIA == 1 && rep.UPC.Equals(vj.UPC))?.Ruta
            }).ToList();

            var viewModel = new PaginationViewModel<ProductoVentaViewModel>() {
                Items = vjSelectos,
                Pager = pager
            };

            ViewBag.Plataforma = existeConsola.Plataforma1;
            ViewBag.IdConsola = idConsola;

            return View(viewModel);

        }

        public ActionResult VerDetalleVideojuego(string upc) {

            var videojuego = this._context.VideoJuego
                                          .Include(vj => vj.Genero)
                                          .Include(vj => vj.Clasificacion)
                                          .Include(vj => vj.Desarrolladora)
                                          .Where(vj => vj.UPC.Equals(upc))
                                          .FirstOrDefault();

            if (videojuego == null) {
                return HttpNotFound("No se ha encontrado el producto especificado.");
            }

            var viewModel = new VideojuegoDetalleViewModel() {
                Videojuego = videojuego,
                Precio = this._context.Almacen.Where(almacen => almacen.UPC.Equals(videojuego.UPC) && almacen.ID_TIENDA == 1).Select(almacen => almacen.Precio).FirstOrDefault(),
                Rating = this._context.Rating.Where(rating => rating.UPC.Equals(videojuego.UPC)).FirstOrDefault(),
                Repositorio = this._context.RepositorioMultimedia.Include(rep => rep.TipoMultimedia).Where(rep => rep.UPC.Equals(videojuego.UPC)).AsEnumerable(),
                Consola = this._context.Plataforma.Where(plat => plat.ID_PLATAFORMA == (this._context.RelProductoPlataforma.Where(rel => rel.UPC.Equals(videojuego.UPC)).Select(rel => rel.ID_PLATAFORMA).FirstOrDefault())).FirstOrDefault(),
                Estatus = this._context.Estatus.Where(estatus => estatus.ID_ESTATUS == (this._context.Almacen.Where(al => al.UPC.Equals(videojuego.UPC) && al.ID_TIENDA == 1).Select(al => al.ID_ESTATUS).FirstOrDefault())).FirstOrDefault()
            };

            return View(viewModel);

        }

        #endregion

        #region Accesorios

        public ActionResult Accesorios(int idConsola, int? idGenero, int? page, int? count) {

            var existeConsola = this._context.Plataforma
                                        .Where(plat => plat.ID_PLATAFORMA == idConsola)
                                        .FirstOrDefault();

            if (existeConsola == null) {
                return HttpNotFound("No existe la consola especificada");
            }

            // Validamos el numero de la pagina
            if (!page.HasValue) {
                page = 1;
            } else if (page <= 0) {
                page = 1;
            }

            // Se obtienen los UPC de los productos con los que cuenta la tienda En Linea en su almacen
            var upc = this._context.Almacen.Where(almacen => almacen.ID_TIENDA == 1).Select(almacen => almacen.UPC).AsEnumerable();

            // Se seleccionan aquellos que son solamente videojuegos
            var listaUpcAccesorios = this._context.Accesorio.Where(vj => upc.Contains(vj.UPC)).Select(vj => vj.UPC).AsEnumerable();

            // Se selecciona aquellos que son de la plataforma especificada
            IEnumerable<string> upcPoductos = this._context.RelProductoPlataforma.Where(rel => rel.ID_PLATAFORMA == idConsola
                                                                            && listaUpcAccesorios.Contains(rel.UPC))
                                                                 .Select(rel => rel.UPC)
                                                                 .AsEnumerable(); ;

            count = upcPoductos.Count();

            // Obtenemos el total de registros
            //if (!count.HasValue) {
            //}

            // Creamos el Pager
            var pager = new Pager(count.Value, page, 6);

            // Obtenemos el detalle de cada producto, en este caso, videojuegos
            var accesorios = this._context.Accesorio
                                           .Where(accesorio => upcPoductos.Contains(accesorio.UPC))
                                           .OrderByDescending(videojuego => videojuego.FechaSalida)
                                           .Skip((pager.CurrentPage - 1) * 6)
                                           .Take(6)
                                           //.Select(accesorio => new ProductoVentaViewModel() {
                                           //    UPC = accesorio.UPC,
                                           //    Nombre = accesorio.Nombre,
                                           //    //Estatus = this._context.Estatus.Where(estatus => estatus.ID_ESTATUS == (this._context.Almacen
                                           //    //                                                                                     .Where(almacen => almacen.UPC.Equals(accesorio.UPC)
                                           //    //                                                                                                    && almacen.ID_TIENDA == 1)
                                           //    //                                                                                     .Select(almacen => almacen.ID_ESTATUS)
                                           //    //                                                                                     .FirstOrDefault()))
                                           //    //                               .FirstOrDefault(),
                                           //    Estatus = this._context.Almacen.Include(a => a.Estatus).FirstOrDefault(a => a.UPC.Equals(accesorio.UPC) && a.ID_TIENDA == 1).Estatus.Estatus1,
                                           //    //Precio = this._context.Almacen.Where(almacen => almacen.UPC.Equals(accesorio.UPC) && almacen.ID_TIENDA == 1).Select(almacen => almacen.Precio).FirstOrDefault(),
                                           //    Precio = this._context.Almacen.FirstOrDefault(a => a.UPC.Equals(accesorio.UPC) && a.ID_TIENDA == 1).Precio,
                                           //    FechaSalida = accesorio.FechaSalida,
                                           //    Ruta = this._context.RepositorioMultimedia.Where(rep => rep.ID_TIPO_MULTIMEDIA == 1 && rep.UPC.Equals(accesorio.UPC)).FirstOrDefault().Ruta
                                           //})
                                           .ToList();

            var accesoriosSelectos = accesorios.Select(accesorio => new ProductoVentaViewModel() {
                UPC = accesorio.UPC,
                Nombre = accesorio.Nombre,
                Estatus = this._context.Almacen.Include(a => a.Estatus).FirstOrDefault(a => a.UPC.Equals(accesorio.UPC) && a.ID_TIENDA == 1).Estatus.Estatus1,
                Precio = this._context.Almacen.FirstOrDefault(a => a.UPC.Equals(accesorio.UPC) && a.ID_TIENDA == 1).Precio,
                FechaSalida = accesorio.FechaSalida,
                Ruta = this._context.RepositorioMultimedia.FirstOrDefault(rep => rep.ID_TIPO_MULTIMEDIA == 1 && rep.UPC.Equals(accesorio.UPC))?.Ruta
            }).ToList();

            var viewModel = new PaginationViewModel<ProductoVentaViewModel>() {
                Items = accesoriosSelectos,
                //Items = accesorios,
                Pager = pager
            };

            ViewBag.Plataforma = existeConsola.Plataforma1;
            ViewBag.IdConsola = idConsola;

            return View(viewModel);

        }

        public ActionResult VerDetalleAccesorio(string upc) {

            var accesorio = this._context.Accesorio
                                          .Where(vj => vj.UPC.Equals(upc))
                                          .FirstOrDefault();

            if (accesorio == null) {
                return HttpNotFound("No se encuetnra el videojuego especificado.");
            }

            var viewModel = new AccesorioDetalleViewModel() {
                Videojuego = accesorio,
                Precio = this._context.Almacen.Where(almacen => almacen.UPC.Equals(accesorio.UPC) && almacen.ID_TIENDA == 1).Select(almacen => almacen.Precio).FirstOrDefault(),
                Repositorio = this._context.RepositorioMultimedia.Include(rep => rep.TipoMultimedia).Where(rep => rep.UPC.Equals(accesorio.UPC)).AsEnumerable(),
                Estatus = this._context.Estatus.Where(estatus => estatus.ID_ESTATUS == (this._context.Almacen.Where(al => al.UPC.Equals(accesorio.UPC) && al.ID_TIENDA == 1).Select(al => al.ID_ESTATUS).FirstOrDefault())).FirstOrDefault(),
                Consola = this._context.Plataforma.Where(plat => plat.ID_PLATAFORMA == (this._context.RelProductoPlataforma.Where(rel => rel.UPC.Equals(accesorio.UPC)).Select(rel => rel.ID_PLATAFORMA).FirstOrDefault())).FirstOrDefault(),
            };

            return View(viewModel);

        }

        #endregion

        #region Genero

        public ActionResult PorGenero(int idConsola, int idGenero, int? page, int? count) {

            var consola = this._context.Plataforma.FirstOrDefault(plat => plat.ID_PLATAFORMA == idConsola);

            if (consola == null) {
                return HttpNotFound("No se encuentra la plataforma.");
            }

            var genero = this._context.Genero.FirstOrDefault(gen => gen.ID_GENERO == idGenero);

            if (genero == null) {
                return HttpNotFound("No se encuentra el género.");
            }

            // Validamos el numero de la pagina
            if (!page.HasValue) {
                page = 1;
            } else if (page <= 0) {
                page = 1;
            }

            var upcPlataforma = this._context.RelProductoPlataforma.Where(p => p.ID_PLATAFORMA == idConsola).Select(p => p.UPC).ToList();

            var upcAlmacen = this._context.Almacen.Where(al => al.ID_TIENDA == 1 && upcPlataforma.Contains(al.UPC)).Select(al => al.UPC).ToList();

            var upcProductos = this._context.VideoJuego.Where(vj => upcAlmacen.Contains(vj.UPC) && vj.ID_GENERO == idGenero).Select(vj => vj.UPC).AsEnumerable();

            count = upcProductos.Count();

            var pager = new Pager(count.Value, page, 6);

            // Obtenemos el detalle de cada producto, en este caso, videojuegos
            var videojuegos = this._context.VideoJuego
                                           .Include(vj => vj.Clasificacion)
                                           .Include(vj => vj.Genero)
                                           .Include(vj => vj.Desarrolladora)
                                           .Where(videojuego => upcProductos.Contains(videojuego.UPC) && videojuego.ID_GENERO == idGenero)
                                           .OrderByDescending(videojuego => videojuego.FechaSalida)
                                           .Skip((pager.CurrentPage - 1) * 6).Take(6)
                                           //.Select(videojuego => new ProductoVentaViewModel() {
                                           //    UPC = videojuego.UPC,
                                           //    Nombre = videojuego.Nombre,
                                           //    Estatus = this._context.Estatus.Where(estatus => estatus.ID_ESTATUS == (this._context.Almacen
                                           //                                                                                         .Where(almacen => almacen.UPC.Equals(videojuego.UPC)
                                           //                                                                                                        && almacen.ID_TIENDA == 1)
                                           //                                                                                         .Select(almacen => almacen.ID_ESTATUS)
                                           //                                                                                         .FirstOrDefault()))
                                           //                                   .FirstOrDefault(),
                                           //    Precio = this._context.Almacen.Where(almacen => almacen.UPC.Equals(videojuego.UPC) && almacen.ID_TIENDA == 1).Select(almacen => almacen.Precio).FirstOrDefault(),
                                           //    Clasificacion = this._context.Clasificacion.Where(clasificacion => clasificacion.ID_CLASIFICACION == videojuego.ID_CLASIFICACION).FirstOrDefault(),
                                           //    Genero = this._context.Genero.Where(gen => gen.ID_GENERO == idGenero).FirstOrDefault(),
                                           //    Desarrolladora = this._context.Desarrolladora.Where(des => des.ID_DESARROLLADORA == videojuego.ID_DESARROLLADORA).FirstOrDefault(),
                                           //    FechaSalida = videojuego.FechaSalida,
                                           //    Ruta = this._context.RepositorioMultimedia.Where(rep => rep.ID_TIPO_MULTIMEDIA == 1 && rep.UPC.Equals(videojuego.UPC)).FirstOrDefault().Ruta
                                           //})
                                           .ToList();

            var vjSelectos = videojuegos.Select(vj => new ProductoVentaViewModel() {
                UPC = vj.UPC,
                Nombre = vj.Nombre,
                Estatus = this._context.Almacen.Include(a => a.Estatus).FirstOrDefault(a => a.UPC.Equals(vj.UPC) && a.ID_TIENDA == 1).Estatus.Estatus1,
                Precio = this._context.Almacen.FirstOrDefault(a => a.UPC.Equals(vj.UPC) && a.ID_TIENDA == 1).Precio,
                Clasificacion = vj.Clasificacion.Clasificacion1,
                Genero = vj.Genero.Genero1,
                Desarrolladora = vj.Desarrolladora.Desarrolladora1,
                FechaSalida = vj.FechaSalida,
                Ruta = this._context.RepositorioMultimedia.FirstOrDefault(rep => rep.ID_TIPO_MULTIMEDIA == 1 && rep.UPC.Equals(vj.UPC))?.Ruta
            }).ToList();


            var viewModel = new PaginationViewModel<ProductoVentaViewModel>() {
                //Items = videojuegos,
                Items = vjSelectos,
                Pager = pager
            };

            ViewBag.Plataforma = genero.Genero1;
            ViewBag.IdConsola = 0;

            return View("Videojuegos", viewModel);

        }

        #endregion

        [Authorize(Roles = "Cliente")]
        public ActionResult VerCompras(int? page, int? count) {

            var idCliente = User.Identity.GetUserId();

            // Validamos el numero de la pagina
            if (!page.HasValue) {
                page = 1;
            } else if (page <= 0) {
                page = 1;
            }

            // Obtenemos el total de registros
            if (!count.HasValue) {
                count = this._context.Venta.Where(venta => venta.ID_USUARIO == idCliente).Count();
            }

            var pager = new Pager(count.Value, page, 15);

            var ventas = this._context.Venta
                                         .OrderBy(venta => venta.Fecha)
                                         .Where(venta => venta.ID_USUARIO == idCliente)
                                         .Skip((pager.CurrentPage - 1) * 15).Take(15)
                                         .Select(venta => new VentaViewModel() {
                                             Venta = venta,
                                             Empleado = this._context.AspNetUsers.Where(usr => usr.Id.Equals(venta.ID_EMPLEADO))
                                                                                 .Select(usr => new Usuario() {
                                                                                     Id = usr.Id,
                                                                                     Email = usr.Email,
                                                                                     PhoneNumber = usr.PhoneNumber,
                                                                                     UserName = usr.UserName,
                                                                                     PrimerNombre = usr.PrimerNombre,
                                                                                     SegundoNombre = usr.SegundoNombre,
                                                                                     ApellidoPaterno = usr.ApellidoPaterno,
                                                                                     ApellidoMaterno = usr.ApellidoMaterno,
                                                                                     Calle = usr.Calle,
                                                                                     NumInt = usr.NumInt,
                                                                                     ID_CP = usr.ID_CP,
                                                                                     FechaNacimiento = usr.FechaNacimiento,
                                                                                     TelefonoCasa = usr.TelefonoCasa
                                                                                 }).FirstOrDefault(),
                                             Cliente = this._context.AspNetUsers.Where(usr => usr.Id.Equals(venta.ID_USUARIO))
                                                                                .Select(usr => new Usuario() {
                                                                                    Id = usr.Id,
                                                                                    Email = usr.Email,
                                                                                    PhoneNumber = usr.PhoneNumber,
                                                                                    UserName = usr.UserName,
                                                                                    PrimerNombre = usr.PrimerNombre,
                                                                                    SegundoNombre = usr.SegundoNombre,
                                                                                    ApellidoPaterno = usr.ApellidoPaterno,
                                                                                    ApellidoMaterno = usr.ApellidoMaterno,
                                                                                    Calle = usr.Calle,
                                                                                    NumInt = usr.NumInt,
                                                                                    ID_CP = usr.ID_CP,
                                                                                    FechaNacimiento = usr.FechaNacimiento,
                                                                                    TelefonoCasa = usr.TelefonoCasa
                                                                                }).FirstOrDefault(),
                                             Tienda = this._context.Tienda.Where(t => t.ID_TIENDA == venta.ID_TIENDA).FirstOrDefault()
                                         })
                                         .AsEnumerable();

            var viewModel = new PaginationViewModel<VentaViewModel>() {
                Items = ventas,
                Pager = pager
            };

            return View(viewModel);

        }

        [Authorize(Roles = "Cliente")]
        public ActionResult DetalleCompra(int idVenta) {

            var venta = this._context.Venta
                                         .Where(v => v.ID_VENTA == idVenta)
                                         .Select(v => new VentaViewModel() {
                                             Venta = v,
                                             Empleado = this._context.AspNetUsers.Where(usr => usr.Id.Equals(v.ID_EMPLEADO))
                                                                                 .Select(usr => new Usuario() {
                                                                                     Id = usr.Id,
                                                                                     Email = usr.Email,
                                                                                     PhoneNumber = usr.PhoneNumber,
                                                                                     UserName = usr.UserName,
                                                                                     PrimerNombre = usr.PrimerNombre,
                                                                                     SegundoNombre = usr.SegundoNombre,
                                                                                     ApellidoPaterno = usr.ApellidoPaterno,
                                                                                     ApellidoMaterno = usr.ApellidoMaterno,
                                                                                     Calle = usr.Calle,
                                                                                     NumInt = usr.NumInt,
                                                                                     ID_CP = usr.ID_CP,
                                                                                     FechaNacimiento = usr.FechaNacimiento,
                                                                                     TelefonoCasa = usr.TelefonoCasa
                                                                                 }).FirstOrDefault(),
                                             Cliente = this._context.AspNetUsers.Where(usr => usr.Id.Equals(v.ID_USUARIO))
                                                                                .Select(usr => new Usuario() {
                                                                                    Id = usr.Id,
                                                                                    Email = usr.Email,
                                                                                    PhoneNumber = usr.PhoneNumber,
                                                                                    UserName = usr.UserName,
                                                                                    PrimerNombre = usr.PrimerNombre,
                                                                                    SegundoNombre = usr.SegundoNombre,
                                                                                    ApellidoPaterno = usr.ApellidoPaterno,
                                                                                    ApellidoMaterno = usr.ApellidoMaterno,
                                                                                    Calle = usr.Calle,
                                                                                    NumInt = usr.NumInt,
                                                                                    ID_CP = usr.ID_CP,
                                                                                    FechaNacimiento = usr.FechaNacimiento,
                                                                                    TelefonoCasa = usr.TelefonoCasa
                                                                                }).FirstOrDefault()
                                         }).FirstOrDefault();

            if (venta == null) {
                return HttpNotFound("No se encuentra la venta.");
            }

            var viewModel = this._context.DetalleVenta
                                         .Where(detVenta => detVenta.ID_VENTA == idVenta)
                                         .Select(detVenta => new DetalleVentaViewModel() {
                                             Producto = this._context.Productos.Where(prod => prod.UPC.Equals(detVenta.UPC)).FirstOrDefault(),
                                             Cantidad = detVenta.Cantidad,
                                             Monto = detVenta.Monto
                                         }).AsEnumerable();

            ViewBag.Venta = venta;

            return View(viewModel);

        }

    }
}