using GameOfBreak.Models;
using GameOfBreak.Models.GoB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameOfBreak.Controllers {

    public class ShoppingCartController : Controller {

        private GameOfBreakModel _context;

        public ShoppingCartController () {
            this._context = new GameOfBreakModel();
        }

        // Se muestra el detalle del carrito
        // GET: ShoppingCart
        public ActionResult Index () {

            var shoppingCart = ShoppingCart.GetCarrito(this.HttpContext);

            CarritoViewModel viewModel = null;

            try {

                var idCarrito = Int64.Parse(shoppingCart.ID_CARRITO);

                viewModel = new CarritoViewModel() {
                    idCarrito = idCarrito.ToString(),
                    DetalleCarrito = this._context.DetalleCarrito
                                                  .Where(detCar => detCar.ID_CARRITO == idCarrito)
                                                  .Select(detCar => new DetalleCarritoViewModel() {
                                                      idDetalleCarrito = detCar.ID_DETALLE_CARRITO,
                                                      UPC = detCar.UPC,
                                                      Titulo = this._context.Productos.Where(prod => prod.UPC.Equals(detCar.UPC)).Select(prod => prod.Titulo).FirstOrDefault(),
                                                      Plataforma = this._context.Productos.Where(prod => prod.UPC.Equals(detCar.UPC)).Select(prod => prod.Plataforma).FirstOrDefault(),
                                                      Cantidad = detCar.Cantidad,
                                                      Precio = detCar.Monto,
                                                      Total = (detCar.Cantidad * detCar.Monto)
                                                  })
                                                  .AsEnumerable()
                };

            } catch (Exception) {

                var idCarrito = shoppingCart.ID_CARRITO;

                viewModel = new CarritoViewModel() {
                    idCarrito = idCarrito.ToString(),
                    DetalleCarrito = this._context.DetalleCarritoTemporal
                                                  .Where(detCar => detCar.ID_CARRITO.Equals(idCarrito))
                                                  .Select(detCar => new DetalleCarritoViewModel() {
                                                      idDetalleCarrito = detCar.ID_DETALLE_CARRITO,
                                                      UPC = detCar.UPC,
                                                      Titulo = this._context.Productos.Where(prod => prod.UPC.Equals(detCar.UPC)).Select(prod => prod.Titulo).FirstOrDefault(),
                                                      Plataforma = this._context.Productos.Where(prod => prod.UPC.Equals(detCar.UPC)).Select(prod => prod.Plataforma).FirstOrDefault(),
                                                      Cantidad = detCar.Cantidad,
                                                      Precio = detCar.Monto,
                                                      Total = (detCar.Cantidad * detCar.Monto)
                                                  })
                                                  .AsEnumerable()
                };

            }

            return View(viewModel);

        }

        public ActionResult AgregarAlCarrito (string upc) {

            var correctoUpc = this._context.Productos.Where(prod => prod.UPC.Equals(upc)).Select(prod => prod.UPC).FirstOrDefault();

            if (!String.IsNullOrEmpty(correctoUpc)) {

                var carrito = ShoppingCart.GetCarrito(this.HttpContext);

                carrito.AgregarProducto(upc);

            } else {
                return HttpNotFound("Producto no encontrado");
            }

            return RedirectToAction("Index");

        }

        public ActionResult RemoverDelCarrito (string upc) {

            var correctoUpc = this._context.Productos.Where(prod => prod.UPC.Equals(upc)).Select(prod => prod.UPC).FirstOrDefault();

            if (!String.IsNullOrEmpty(correctoUpc)) {

                var carrito = ShoppingCart.GetCarrito(this.HttpContext);

                carrito.QuitarProducto(upc);

            } else {
                return HttpNotFound("Producto no encontrado");
            }

            return RedirectToAction("Index");

        }

        public ActionResult ConfirmarCompra () {

            if (!User.Identity.IsAuthenticated) {

                return RedirectToAction("Login", "Account");

            }

            var carrito = ShoppingCart.GetCarrito(this.HttpContext);

            carrito.ConfirmarCompra();

            Session[ShoppingCart.KEY_ID_CARRITO] = null;

            return RedirectToAction("Index", "Home");

        }

        [ChildActionOnly]
        public ActionResult CartSummary () {

            var cart = ShoppingCart.GetCarrito(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();

            return PartialView("CartSummary");

        }

    }

}