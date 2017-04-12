using AutoMapper;
using GameOfBreak.Areas.Admin.Models;
using GameOfBreak.Models;
using GameOfBreak.Models.GoB;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace GameOfBreak.Areas.Admin.Controllers {

    [Authorize]
    public class UsuarioController : Controller {

        private GameOfBreakModel _context;

        public UsuarioController() {
            this._context = new GameOfBreakModel();
        }

        protected override void Dispose(bool disposing) {
            this._context.Dispose();
        }
        private ApplicationUserManager _userManager; public ApplicationUserManager UserManager {
            get {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set {
                _userManager = value;
            }
        }

        #region Empleado

        [Authorize(Roles = "Administrador, Gerente, Subgerente")]
        // GET: Usuario/Empleado
        public ActionResult Empleados(int? page, int? count, int? idTienda) {

            // Validamos el numero de la pagina
            if (!page.HasValue) {
                page = 1;
            } else if (page <= 0) {
                page = 1;
            }

            // Si no hay un idTienda
            if (!idTienda.HasValue) {

                // Si es un administrador
                if (User.IsInRole("Administrador")) {

                    // Se reenvia al listado de tiendas para seleccionar la tienda deseada
                    return RedirectToAction("Tiendas", "Tienda");

                } else { // Se trata de un empleado

                    var idEmpleado = this._context.AspNetUsers
                                                  .Where(usr => usr.UserName.Equals(User.Identity.Name))
                                                  .Select(usr => usr.Id)
                                                  .FirstOrDefault();

                    idTienda = this._context.RelTiendaEmpleado
                                            .Where(rel => rel.ID_EMPLEADO.Equals(idEmpleado))
                                            .Select(rel => rel.ID_TIENDA)
                                            .FirstOrDefault();

                    if (idTienda == 0) {
                        return HttpNotFound("Tienda no existente o el usuario no esta asignado a una tienda.");
                    }

                }

            }

            // Obtenemos el total de registros de los empleados, de una especifica tienda
            if (!count.HasValue) {

                count = this._context.RelTiendaEmpleado
                                     .Where(rel => rel.ID_TIENDA == idTienda)
                                     .Count();
            }

            // Creamos el Pager
            var pager = new Pager(count.Value, page, 15);

            var relTiendaEmpleados = this._context.RelTiendaEmpleado
                .OrderBy(rel => rel.ID_REL_TIENDA_EMPLEADO)
                .Skip((pager.CurrentPage - 1) * 15)
                .Take(15)
                .Where(rel => rel.ID_TIENDA == idTienda)
                //.Select(rel => new EmpleadoViewModel() {
                //    Usuario = this._context.AspNetUsers.FirstOrDefault(usr => usr.Id.Equals(rel.ID_EMPLEADO)),
                //    //Role = this._context.AspNetRoles.Where(role => role.Id.Equals(this._context.AspNetUserRoles.FirstOrDefault(usrRole => usrRole.UserId.Equals(rel.ID_EMPLEADO)))).FirstOrDefault(),
                //    Role = this._context.AspNetRoles.
                //    //Tienda = this._context.Tienda.Where(t => t.ID_TIENDA == this._context.RelTiendaEmpleado.Where(relTE => relTE.ID_EMPLEADO.Equals(rel.ID_EMPLEADO)).FirstOrDefault().ID_TIENDA).FirstOrDefault()
                //})
                .ToList();

            var idEmpleados = relTiendaEmpleados.Select(relTE => relTE.ID_EMPLEADO).ToList();

            var usrRoles = this._context.AspNetUserRoles
                                                .Where(usrRole => idEmpleados.Contains(usrRole.UserId))
                                                //.Select(usrRole => usrRole.RoleId)
                                                .ToList();

            var rolesEmpleados = this._context.AspNetRoles
                                              //.Where(role => idRolesEmpleados.Contains(role.Id))
                                              .ToList();

            var roles = relTiendaEmpleados.Select(relTE => new EmpleadoViewModel() {
                Usuario = this._context.AspNetUsers.FirstOrDefault(usr => usr.Id.Equals(relTE.ID_EMPLEADO)),
                Role = rolesEmpleados.Where(role => {

                    var idRole = usrRoles.Where(r => r.UserId.Equals(relTE.ID_EMPLEADO)).FirstOrDefault().RoleId;

                    return role.Id.Equals(idRole);

                }).FirstOrDefault(),
                Tienda = this._context.Tienda.FirstOrDefault(t => relTE.ID_TIENDA == t.ID_TIENDA)
            }).ToList();

            ViewBag.IdTienda = idTienda;

            var viewModel = new PaginationViewModel<EmpleadoViewModel> {
                Items = roles,
                Pager = pager
            };

            return View(viewModel);

    }

    public ActionResult EditarDatos(string idUsuario) {

        var idRole = this._context.AspNetUserRoles
                                  .Where(usrRole => usrRole.UserId.Equals(idUsuario))
                                  .Select(usrRole => usrRole.RoleId)
                                  .FirstOrDefault();

        var role = this._context.AspNetRoles.FirstOrDefault(netRole => netRole.Id.Equals(idRole)).Name;

        var empleado = this._context.AspNetUsers
                                    .Where(usr => usr.Id.Equals(idUsuario))
                                    .Select(usr => new Usuario() {
                                        Role = role,
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
                                        NumExt = usr.NumExt,
                                        ID_CP = usr.ID_CP,
                                        CP = this._context.CodigoPostal.Where(cp => cp.ID_CP == usr.ID_CP).Select(cp => cp.CP).FirstOrDefault(),
                                        FechaNacimiento = usr.FechaNacimiento,
                                        TelefonoCasa = usr.TelefonoCasa,
                                    })
                                    .FirstOrDefault();

        if (empleado == null) {
            return HttpNotFound("Usuario no encontrado.");
        }

        return View(empleado);

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> GuardarDatoUsuario(Usuario usuario) {

        var idCP = this._context.CodigoPostal.Where(cp => cp.CP.Equals(usuario.CP)).Select(cp => cp.ID_CP).FirstOrDefault();

        if (idCP <= 0) {
            ModelState.AddModelError("CP", "No existe el Codigo Postal especificado, ingrese uno valido");
            return View("EditarDatos", usuario);
        }

        if (User.IsInRole("Cliente")) {
            ApplicationUser appUsr = AutoMapper.Mapper.Map<ApplicationUser>(usuario);

            var passHash = UserManager.PasswordHasher.HashPassword(usuario.Password);
            appUsr.PasswordHash = passHash;

            var resultadoPass = await UserManager.CheckPasswordAsync(appUsr, usuario.Password);

            if (!resultadoPass) {
                ModelState.AddModelError("Password", "La contraseña proporcionada no es valida.");
                ModelState.AddModelError("ConfirmPassword", "La contraseña proporcionada no es valida.");
                return View("EditarDatos", usuario);
            }
        }


        if (!ModelState.IsValid) {
            return View("EditarDatos", usuario);
        }

        var usuarioDB = this._context.AspNetUsers.Where(usr => usr.Id.Equals(usuario.Id)).FirstOrDefault();

        Mapper.Map<Usuario, AspNetUsers>(usuario, usuarioDB);

        this._context.SaveChanges();

        if (usuario.Role.Equals("Cliente")) {
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        return RedirectToAction("Empleados", "Usuario", new { area = "Admin" });

    }

    #endregion

    #region Cliente

    [Authorize(Roles = "Administrador, Gerente, Subgerente, Vendedor")]
    public ActionResult Clientes(int? page, int? count) {

        // Validamos el numero de la pagina
        if (!page.HasValue) {
            page = 1;
        } else if (page <= 0) {
            page = 1;
        }

        // Obtenemos el total de registros
        if (!count.HasValue) {
            count = this._context.AspNetUserRoles
                                 .Where(usrR => usrR.RoleId.Equals(Roles.ClienteId))
                                 .Count();
        }

        // Creamos el Pager
        var pager = new Pager(count.Value, page, 15);

        var clientes = this._context.AspNetUserRoles
                                    .OrderBy(usrR => usrR.RoleId)
                                    .Where(usrR => usrR.RoleId.Equals(Roles.ClienteId))
                                    .Skip((pager.CurrentPage - 1) * 15)
                                    .Take(15)
                                    .ToList();

        var clientesSeleccionados = clientes.Select(usrR => new ClienteViewModel() {
            ID_USUARIO = usrR.UserId,
            Cliente = this._context.AspNetUsers.FirstOrDefault(usr => usr.Id.Equals(usrR.UserId))
        });

        var viewModel = new PaginationViewModel<ClienteViewModel>() {
            Items = clientesSeleccionados,
            Pager = pager
        };

        return View(viewModel);

    }

    #endregion

    #region Roles

    [Authorize(Roles = "Administrador")]
    public ActionResult VerRoles(int? page, int? count) {

        // Validamos el numero de la pagina
        if (!page.HasValue) {
            page = 1;
        } else if (page <= 0) {
            page = 1;
        }

        // Obtenemos el total de registros
        if (!count.HasValue) {
            count = this._context.AspNetRoles.Count();
        }

        // Creamos el Pager
        var pager = new Pager(count.Value, page, 15);

        var roles = this._context.AspNetRoles
            .OrderBy(rol => rol.Name)
            .Skip((pager.CurrentPage - 1) * 15).Take(15)
            .Select(rol => new RoleViewModel() { Id = rol.Id, Name = rol.Name })
            .AsEnumerable();

        var viewModel = new PaginationViewModel<RoleViewModel> {
            Items = roles,
            Pager = pager
        };

        return View(viewModel);

    }

    #endregion

}

}