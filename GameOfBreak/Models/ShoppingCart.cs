using GameOfBreak.Models.GoB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameOfBreak.Models {

    // Clase para el manejo de los datos del Carrito
    public class ShoppingCart {

        // Este propiedad pude ser un numero entero o un hash
        // Cuando es un numero, se debe a que hay un usuario loggeado
        // Cuando es un hash, se debe a que es un usuario temporal
        public string ID_CARRITO { get; set; }

        public const string KEY_ID_CARRITO = "idCarrito";

        public static ShoppingCart GetCarrito (HttpContextBase context) {

            var carrito = new ShoppingCart();

            // Obtenemos el idCarrito de la cookie
            carrito.ID_CARRITO = carrito.GetCarritoId(context);

            return carrito;

        }

        public static ShoppingCart GetCarrito (Controller controller) {

            return GetCarrito(controller.HttpContext);

        }

        // Se usa HttpContextBase para permitirnos el acceso a la cookies
        public string GetCarritoId (HttpContextBase context) {

            // Validamos que aun no haya un idCarrito
            if (context.Session[KEY_ID_CARRITO] == null) {

                // Validamos si hay LogIn
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name)) {

                    // Validamos si tiene carrido el cliente
                    using (var ctx = new GameOfBreakModel()) {

                        var idUsuario = ctx.AspNetUsers
                                           .Where(usr => usr.UserName.Equals( context.User.Identity.Name))
                                           .Select(usr => usr.Id)
                                           .FirstOrDefault();

                        // Obtenemos el idCarrito
                        var idCarrito = ctx.Carrito
                                           //.Include(carrito => carrito.AspNetUsers)
                                           .Where(carrito => carrito.ID_USUARIO.Equals(idUsuario))
                                           .Select(carrito => carrito.ID_CARRITO)
                                           .FirstOrDefault();

                        // Validamos si el carrito no existe
                        if (idCarrito <= 0) {

                            // Creamos un nuevo carrito
                            var carritoNuevo = new Carrito() {
                                ID_TIENDA = 1,
                                ID_USUARIO = idUsuario,
                                ID_TIPO_PAGO = 5,
                                Fecha = DateTime.Now
                            };

                            ctx.Carrito.Add(carritoNuevo);

                            ctx.SaveChanges();

                            idCarrito = carritoNuevo.ID_CARRITO;

                        }

                        context.Session[KEY_ID_CARRITO] = idCarrito;

                    }

                } else { // Si no hay login, creamos un idCarritoTemporal

                    using (var ctx = new GameOfBreakModel()) {

                        Guid tempCartId = Guid.NewGuid();

                        context.Session[KEY_ID_CARRITO] = tempCartId.ToString();

                        ctx.CarritoTemporal.Add(new CarritoTemporal() {
                            ID_CARRITO = tempCartId.ToString(),
                            ID_TIENDA = 1,
                            ID_USUARIO = tempCartId.ToString()
                        });

                        ctx.SaveChanges();

                    }

                }

            }

            return context.Session[KEY_ID_CARRITO].ToString();

        }

        public void AgregarProducto (string upc) {

            using (var ctx = new GameOfBreakModel()) {

                try {

                    var idCarrito = Int64.Parse(this.ID_CARRITO);

                    // Buscamos si ya existia el producto en el carrito 

                    var detCarrito = ctx.DetalleCarrito.Where(det => det.ID_CARRITO == idCarrito && det.UPC.Equals(upc)).FirstOrDefault();

                    if (detCarrito == null) {

                        ctx.DetalleCarrito.Add(new DetalleCarrito() {
                            UPC = upc,
                            Cantidad = 1,
                            ID_CARRITO = (int)idCarrito,
                            Monto = ctx.Almacen.Where(al => al.UPC.Equals(upc)).Select(al => al.Precio).FirstOrDefault()
                        });

                    } else {

                        detCarrito.Cantidad++;
                    }

                } catch (Exception) {

                    // Buscamos si ya existia el producto en el carrito 
                    var detCarrito = ctx.DetalleCarritoTemporal
                                        .Where(det => det.ID_CARRITO.Equals(ID_CARRITO) && det.UPC.Equals(upc)).FirstOrDefault();

                    if (detCarrito == null) {

                        ctx.DetalleCarritoTemporal.Add(new DetalleCarritoTemporal() {
                            UPC = upc,
                            Cantidad = 1,
                            ID_CARRITO = ID_CARRITO,
                            Monto = ctx.Almacen.Where(al => al.UPC.Equals(upc) && al.ID_TIENDA == 1).Select(al => al.Precio).FirstOrDefault()
                        });

                    } else {

                        detCarrito.Cantidad++;

                    }

                }

                ctx.SaveChanges();

            }

        }

        public void QuitarProducto (string upc) {

            using (var ctx = new GameOfBreakModel()) {

                try {

                    var idCar = Int64.Parse(ID_CARRITO);

                    var detalle = ctx.DetalleCarrito.Where(det => det.ID_CARRITO == idCar && det.UPC.Equals(upc)).FirstOrDefault();

                    if (detalle != null) {

                        if (detalle.Cantidad > 1) {

                            detalle.Cantidad--;

                        } else {

                            ctx.DetalleCarrito.Remove(detalle);

                        }

                    }

                } catch (Exception) {

                    var detalle = ctx.DetalleCarritoTemporal.Where(det => det.ID_CARRITO.Equals(ID_CARRITO) && det.UPC.Equals(upc)).FirstOrDefault();

                    if (detalle != null) {

                        if (detalle.Cantidad > 1) {

                            detalle.Cantidad--;

                        } else {

                            ctx.DetalleCarritoTemporal.Remove(detalle);

                        }

                    }

                }

                ctx.SaveChanges();

            }

        }

        // Cuando un cliente inicia sesion y hay un carrito temporal, se asocia al cliente
        public int MigrarCarrito (string userName) {

            using (var ctx = new GameOfBreakModel()) {

                // Validamos que ya tenga su carrito un usuario registrado
                var idCarrito = ctx.Carrito
                                   .Where(carrito => carrito.ID_USUARIO.Equals(ctx.AspNetUsers
                                                                                  .Where(usr => usr.UserName.Equals(userName))
                                                                                  .Select(usr => usr.Id).FirstOrDefault()))
                                   .Select(carrito =>carrito.ID_CARRITO)
                                   .FirstOrDefault();

                // No tiene carrito
                if (idCarrito <= 0) {

                    // Obtenemos el carrito asi como cualquier detalle carrito temporales
                    var carritoTemp = ctx.CarritoTemporal.Where(carTemp => carTemp.ID_CARRITO.Equals(ID_CARRITO))
                                                         .FirstOrDefault();

                    var detallesCarritoTemp = ctx.DetalleCarritoTemporal.Where(detTemp => detTemp.ID_CARRITO.Equals(ID_CARRITO))
                                                                        .AsEnumerable();

                    var nuevoCarrito = new Carrito() {
                        ID_TIENDA = 1,
                        ID_USUARIO = ctx.AspNetUsers.Where(usr => usr.UserName.Equals(userName)).Select(usr => usr.Id).FirstOrDefault(),
                        ID_TIPO_PAGO = 5,
                        Fecha = new DateTime(2000, 1, 1)
                    };

                    ctx.Carrito.Add(nuevoCarrito);

                    ctx.SaveChanges();

                    idCarrito = nuevoCarrito.ID_CARRITO;

                    foreach (var detalleTemp in detallesCarritoTemp) {

                        ctx.DetalleCarrito.Add(new DetalleCarrito() {
                            ID_CARRITO = idCarrito,
                            UPC = detalleTemp.UPC,
                            Cantidad = detalleTemp.Cantidad,
                            Monto = detalleTemp.Monto
                        });

                        // Removemos los elementos
                        ctx.DetalleCarritoTemporal.Remove(detalleTemp);

                    }

                    // Removemos el carrito
                    ctx.CarritoTemporal.Remove(carritoTemp);

                    ctx.SaveChanges();

                    return idCarrito;

                } else { // Tiene carrito

                    // Obtenemos su carrito
                    var carrito = ctx.Carrito.Where(car => car.ID_CARRITO == idCarrito).FirstOrDefault();

                    // Obtenemos el carrtio temporal
                    var carritoTemp = ctx.CarritoTemporal.Where(carTemp => carTemp.ID_CARRITO.Equals(ID_CARRITO)).FirstOrDefault();

                    // Obtebemos los detalles temporales
                    var detallesCarritoTemp = ctx.DetalleCarritoTemporal.Where(detTemp => detTemp.ID_CARRITO.Equals(ID_CARRITO))
                                                                        .AsEnumerable();

                    // Respaltamos el ID_CARRITO_TEMPORAL
                    string viejoId = ID_CARRITO;

                    foreach (var detTemp in detallesCarritoTemp) {

                        ID_CARRITO = idCarrito.ToString();

                        var i = 1;

                        do {

                            // Vinculamos los detalles temporales
                            AgregarProducto(detTemp.UPC);

                            // Removemos los detalles temporales
                            ctx.DetalleCarritoTemporal.Remove(detTemp);

                            i++;

                        } while (i <= detTemp.Cantidad);

                    }

                    ID_CARRITO = viejoId;

                    ctx.CarritoTemporal.Remove(carritoTemp);

                    ctx.SaveChanges();

                    return idCarrito;

                }

            }

        }

        public int GetCount () {

            int? count = 0;

            using (var ctx = new GameOfBreakModel()) {

                try {

                    var idCar = Int64.Parse(ID_CARRITO);

                    count = ctx.DetalleCarrito
                               .Where(detCarrito => detCarrito.ID_CARRITO == idCar)
                               .Select(detCarrtio => detCarrtio.Cantidad)
                               .Sum();

                } catch (Exception) {

                    count = ctx.DetalleCarritoTemporal
                               .Where(detCarTemp => detCarTemp.ID_CARRITO.Equals(ID_CARRITO))
                               .Select(detCarrtio => (int?)detCarrtio.Cantidad)
                               .Sum();

                }

            }

            return count ?? 0;

        }

        public void ConfirmarCompra () {

            using (var ctx = new GameOfBreakModel()) {

                var idCarrito = Int64.Parse(ID_CARRITO);

                var carrito = ctx.Carrito.Where(car => car.ID_CARRITO == idCarrito).FirstOrDefault();

                var detalleCarrito = ctx.DetalleCarrito.Where(detCar => detCar.ID_CARRITO == idCarrito).AsEnumerable();

                var venta = new Venta() {
                    ID_TIENDA = 1,
                    ID_EMPLEADO = Roles.UsuarioAdministradorID,
                    ID_USUARIO = carrito.ID_USUARIO,
                    Fecha = DateTime.Now,
                    ID_TIPO_PAGO = 5
                };

                ctx.Venta.Add(venta);

                ctx.SaveChanges();

                var idVenta = venta.ID_VENTA;

                foreach (var item in detalleCarrito) {

                    ctx.DetalleVenta.Add(new DetalleVenta() {
                        ID_VENTA = idVenta,
                        UPC = item.UPC,
                        Cantidad = item.Cantidad,
                        Monto = item.Monto
                    });

                    ctx.DetalleCarrito.Remove(item);

                }

                ctx.SaveChanges();

                ctx.Carrito.Remove(carrito);

                ctx.SaveChanges();

            }

        }

    }

    public class CarritoViewModel {

        public string idCarrito { get; set; }

        public IEnumerable<DetalleCarritoViewModel> DetalleCarrito { get; set; }

    }

    public class DetalleCarritoViewModel {

        public int idDetalleCarrito { get; set; }

        public string UPC { get; set; }

        public string Titulo { get; set; }

        public string Plataforma { get; set; }

        public int Cantidad { get; set; }

        public decimal Precio { get; set; }

        public decimal Total { get; set; }

    }

}