using AutoMapper;
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
    public class VentaController : Controller {

        #region Context
        private GameOfBreakModel _context;

        public VentaController () {
            this._context = new GameOfBreakModel();
        }

        protected override void Dispose (bool disposing) {
            this._context.Dispose();
        }
        #endregion

        // GET: Admin/Venta/VerVentas
        [Authorize(Roles = "Administrador, Gerente, Subgerente, Vendedor")]
        public ActionResult VerVentas (int idTienda, int? page, int? count) {

            if (idTienda <= 0) {

                // Obtenemos el ID de la tiendar a partir del usuario logeado
                var userName = User.Identity.Name;

                var idEmpleado = this._context.AspNetUsers.Where(usr => usr.UserName.Equals(userName)).Select(usr => usr.Id).FirstOrDefault();

                idTienda = this._context.RelTiendaEmpleado.Where(tienda => tienda.ID_EMPLEADO.Equals(idEmpleado)).Select(tienda => tienda.ID_TIENDA).FirstOrDefault();

            }

            // Validamos el idTienda
            var tiendaBD = this._context.Tienda.Where(tienda => tienda.ID_TIENDA == idTienda).FirstOrDefault();

            if (tiendaBD == null) {
                return HttpNotFound("No se encuentra la tienda que se especifico");
            }

            // Validamos el numero de la pagina
            if (!page.HasValue) {
                page = 1;
            } else if (page <= 0) {
                page = 1;
            }

            // Obtenemos el total de registros
            if (!count.HasValue) {
                count = this._context.Venta.Where(venta => venta.ID_TIENDA == idTienda).Count();
            }

            var pager = new Pager(count.Value, page, 15);

            var ventas = this._context.Venta
                                         .OrderBy(venta => venta.Fecha)
                                         .Where(venta => venta.ID_TIENDA == idTienda)
                                         .Skip((pager.CurrentPage - 1) * 15 ).Take(15)
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
                                                                                }).FirstOrDefault()
                                         })
                                         .AsEnumerable();

            var viewModel = new PaginationViewModel<VentaViewModel>() {
                Items = ventas,
                Pager = pager
            };

            ViewBag.Tienda = tiendaBD;

            return View(viewModel);

        }

        [Authorize(Roles = "Administrador, Gerente, Subgerente, Vendedor")]
        public ActionResult DetalleVenta (int idVenta) {

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

        public ActionResult VerCompras (string idCliente, int? page, int? count) {

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
                                         .Skip((pager.CurrentPage - 1) * 15 ).Take(15)
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

            ViewBag.IdCliente = idCliente;

            return View(viewModel);

        }

    }

}