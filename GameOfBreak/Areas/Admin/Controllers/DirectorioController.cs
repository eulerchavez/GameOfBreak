using GameOfBreak.Models.GoB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using GameOfBreak.Models;
using GameOfBreak.Areas.Admin.Models;
using AutoMapper;

namespace GameOfBreak.Areas.Admin.Controllers {

    [Authorize(Roles = "Administrador")]
    public class DirectorioController : Controller {

        private GameOfBreakModel _context;

        public DirectorioController () {
            this._context = new GameOfBreakModel();
        }

        protected override void Dispose (bool disposing) {
            this._context.Dispose();
        }

        #region Relación CP

        // GET: Admin/Directorio
        public ActionResult Index (int? page, int? count) {

            // Validamos el numero de la pagina
            if (!page.HasValue) {
                page = 1;
            } else if (page <= 0) {
                page = 1;
            }

            // Obtenemos el total de registros
            if (!count.HasValue) {
                count = this._context.CodigoPostal.Count();
            }

            // Creamos el pager
            var pager = new Pager(count.Value, page, 15);

            // Obtenermos los registros
            var codigosPostales = _context.CodigoPostal
                .Include(col => col.Colonia)
                .Include(md => md.MunicipioDelegacion)
                .Include(e => e.Estado)
                .Include(c => c.Ciudad)
                .OrderBy(o => o.ID_CP)
                .AsEnumerable()
                .Skip((pager.CurrentPage - 1) * 15).Take(15);

            var viewModel = new PaginationViewModel<CodigoPostal>
            {
                Items = codigosPostales,
                Pager = pager
            };

            return View(viewModel);

        }

        // GET: Admin/Directorio/Agregar
        public ActionResult Agregar () {

            var codigoPostal = new CodigoPostal() { ID_CP = 0 };

            var colonias = this._context.Colonia.AsEnumerable();
            var municipios = this._context.MunicipioDelegacion.AsEnumerable();
            var estados = this._context.Estado.AsEnumerable();
            var ciudades = this._context.Ciudad.AsEnumerable();

            var viewModel = new DirectorioViewModel() {

                CodigoPostal = codigoPostal,
                Colonias = colonias,
                MunicipiosDelegaciones = municipios,
                Estados = estados,
                Ciudades = ciudades

            };

            return View("DirectorioForm", viewModel);

        }

        // GET: Admin/Directorio/Editar
        public ActionResult Editar (int idCP, int idCol, int idDelM, int idEdo, int idCi) {

            // Obtenemos lo que deberia ser un CP valido
            var codigoPostal = this._context.CodigoPostal
                                   .Where( cp =>
                                        cp.ID_CP == idCP &&
                                        cp.ID_COLONIA == idCol &&
                                        cp.ID_MUNICIPIO_DELEGACION == idDelM &&
                                        cp.ID_ESTADO == idEdo &&
                                        cp.ID_CIUDAD == idCi)
                                   .Include( col => col.Colonia)
                                   .Include( mun => mun.MunicipioDelegacion)
                                   .Include( edo => edo.Estado)
                                   .Include( ciu => ciu.Ciudad)
                                   .SingleOrDefault();

            // Validamos que los valores obtenidos sean los indicados
            if (codigoPostal == null) {
                return RedirectToAction("Index", "Directorio");
            }

            var colonias = this._context.Colonia.AsEnumerable();
            var municipios = this._context.MunicipioDelegacion.AsEnumerable();
            var estados = this._context.Estado.AsEnumerable();
            var ciudades = this._context.Ciudad.AsEnumerable();

            var viewModel = new DirectorioViewModel() {

                CodigoPostal = codigoPostal,
                Colonias = colonias,
                MunicipiosDelegaciones = municipios,
                Estados = estados,
                Ciudades = ciudades

            };

            return View("DirectorioForm", viewModel);

        }

        // GET: Admin/Directorio/Guardar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Guardar (CodigoPostal codigoPostal) {

            if (!ModelState.IsValid) {

                var colonias = this._context.Colonia.AsEnumerable();
                var municipios = this._context.MunicipioDelegacion.AsEnumerable();
                var estados = this._context.Estado.AsEnumerable();
                var ciudades = this._context.Ciudad.AsEnumerable();

                var viewModel = new DirectorioViewModel() {

                    CodigoPostal = codigoPostal,
                    Colonias = colonias,
                    MunicipiosDelegaciones = municipios,
                    Estados = estados,
                    Ciudades = ciudades

                };

                return View("DirectorioForm", viewModel);

            }

            if (codigoPostal.ID_CP == 0) {

                this._context.CodigoPostal.Add(codigoPostal);

            } else {

                var cpDB = this._context.CodigoPostal.Where(c => c.ID_CP == codigoPostal.ID_CP).SingleOrDefault();

                cpDB.ID_COLONIA = codigoPostal.ID_COLONIA;
                cpDB.ID_MUNICIPIO_DELEGACION = codigoPostal.ID_MUNICIPIO_DELEGACION;
                cpDB.ID_ESTADO = codigoPostal.ID_ESTADO;
                cpDB.ID_CIUDAD = codigoPostal.ID_CIUDAD;

            }

            this._context.SaveChanges();

            return RedirectToAction("Index", "Directorio");

        }

        #endregion

        #region Colonia

        // GET: Admin/Directorio/Colonias
        public ActionResult Colonias (int? page, int? count) {

            // Validamos el numero de la pagina
            if (!page.HasValue) {
                page = 1;
            } else if (page <= 0) {
                page = 1;
            }

            // Obtenemos el total de registros
            if (!count.HasValue) {
                count = this._context.Colonia.Count();
            }

            // Creamos el pager
            var pager = new Pager(count.Value, page, 15);

            var colonias = this._context.Colonia
                .OrderBy(c => c.ID_COLONIA)
                .Skip((pager.CurrentPage - 1) * 15).Take(15)
                .AsEnumerable();

            var viewModel = new PaginationViewModel<Colonia>
            {
                Items = colonias,
                Pager = pager
            };

            return View(viewModel);

        }

        // GET: Admin/Directorio/EditarColonia
        public ActionResult EditarColonia (int idColonia) {

            var colonia = this._context.Colonia.Where(col => col.ID_COLONIA == idColonia).SingleOrDefault();

            if (colonia == null) {
                return RedirectToAction("Colonias", "Directorio");
            }

            return View("ColoniaForm", colonia);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GuardarColonia (Colonia colonia) {

            if (!ModelState.IsValid) {
                return View("ColoniaForm", colonia);
            }

            if (colonia.ID_COLONIA <= 0) {

                this._context.Colonia.Add(colonia);

            } else {

                var coloniaDB = this._context.Colonia.Where(col => col.ID_COLONIA == colonia.ID_COLONIA).SingleOrDefault();
                coloniaDB.Colonia1 = colonia.Colonia1;

            }

            this._context.SaveChanges();

            return RedirectToAction("Colonias", "Directorio");
        }

        #endregion

        #region Municipio o Delegación

        // GET: Admin/Disrectorio/MunicipiosDelegaciones
        public ActionResult MunicipiosDelegaciones (int? page, int? count) {

            // Validamos el numero de la pagina
            if (!page.HasValue) {
                page = 1;
            } else if (page <= 0) {
                page = 1;
            }

            // Obtenemos el total de registros
            if (!count.HasValue) {
                count = this._context.MunicipioDelegacion.Count();
            }

            // Creamos el pager
            var pager = new Pager(count.Value, page);

            var municipioDelegacion = this._context.MunicipioDelegacion
                .OrderBy(c => c.ID_MUNICIPIO_DELEGACION)
                .Skip((pager.CurrentPage - 1) * 15).Take(15)
                .AsEnumerable();

            var viewModel = new PaginationViewModel<MunicipioDelegacion>
            {
                Items = municipioDelegacion,
                Pager = pager
            };

            return View(viewModel);

        }

        public ActionResult EditarMunicipioDelegacion (int idMunicipioDelegacion) {

            var municipioDelegacion = this._context.MunicipioDelegacion.Where(m => m.ID_MUNICIPIO_DELEGACION == idMunicipioDelegacion).SingleOrDefault();

            if (municipioDelegacion == null) {
                return RedirectToAction("MunicipiosDelegaciones", "Directorio");
            }

            return View("MunicipioDelegacionForm", municipioDelegacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GuardarMunicipioDelegacion (MunicipioDelegacion municipioDelegacion) {

            if (!ModelState.IsValid) {
                return View("MunicipioDelegacionForm", municipioDelegacion);
            }

            if (municipioDelegacion.ID_MUNICIPIO_DELEGACION <= 0) {

                this._context.MunicipioDelegacion.Add(municipioDelegacion);

            } else {

                var municipioDelegacionDB = this._context.MunicipioDelegacion
                    .Where(mun => mun.ID_MUNICIPIO_DELEGACION == municipioDelegacion.ID_MUNICIPIO_DELEGACION)
                    .SingleOrDefault();

                municipioDelegacionDB.MunicipioDelegacion1 = municipioDelegacion.MunicipioDelegacion1;

            }

            this._context.SaveChanges();

            return RedirectToAction("MunicipiosDelegaciones", "Directorio");

        }

        #endregion

        #region Estado

        public ActionResult Estados (int? page, int? count) {

            // Validamos el numero de la pagina
            if (!page.HasValue) {
                page = 1;
            } else if (page <= 0) {
                page = 1;
            }

            // Obtenemos el total de registros
            if (!count.HasValue) {
                count = this._context.Estado.Count();
            }

            // Creamos el pager
            var pager = new Pager(count.Value, page, 15);

            var estados = this._context.Estado
                .OrderBy(edo => edo.ID_ESTADO)
                .Skip((pager.CurrentPage - 1) * 15).Take(15)
                .AsEnumerable();

            var viewModel = new PaginationViewModel<Estado>
            {
                Items = estados,
                Pager = pager
            };

            return View(viewModel);
        }

        public ActionResult EditarEstado (int idEstado) {

            var estado = this._context.Estado.Where(edo => edo.ID_ESTADO == idEstado).SingleOrDefault();

            if (estado == null) {
                return RedirectToAction("Estados", "Directorio");
            }

            return View("EstadoForm", estado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GuardarEstado (Estado estado) {

            if (!ModelState.IsValid) {
                return View("EditarEstado", estado);
            }

            if (estado.ID_ESTADO <= 0) {

                this._context.Estado.Add(estado);

            } else {

                var estadoDB = this._context.Estado
                    .Where(edo => edo.ID_ESTADO == estado.ID_ESTADO)
                    .SingleOrDefault();

                estadoDB.Estado1 = estado.Estado1;

            }

            this._context.SaveChanges();

            return RedirectToAction("Estados", "Directorio");

        }

        #endregion

        #region Ciudad

        public ActionResult Ciudades (int? page, int? count) {

            // Validamos el numero de la pagina
            if (!page.HasValue) {
                page = 1;
            } else if (page <= 0) {
                page = 1;
            }

            // Obtenemos el total de registros
            if (!count.HasValue) {
                count = this._context.Ciudad.Count();
            }

            // Creamos el pager
            var pager = new Pager(count.Value, page, 15);

            var ciudades = this._context.Ciudad
                .OrderBy(ciu => ciu.Ciudad1)
                .Skip((pager.CurrentPage - 1) * 15).Take(15)
                .AsEnumerable();

            var viewModel = new PaginationViewModel<Ciudad>
            {
                Items = ciudades,
                Pager = pager
            };

            return View(viewModel);

        }

        public ActionResult EditarCiudad (int idCiudad) {

            var ciudad = this._context.Ciudad.Where(ciu => ciu.ID_CIUDAD == idCiudad).SingleOrDefault();

            if (ciudad == null) {
                return RedirectToAction("Ciudades", "Directorio");
            }

            return View("CiudadForm", ciudad);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GuardarCiudad (Ciudad ciudad) {

            if (!ModelState.IsValid) {
                return View("EditarCiudad", ciudad);
            }

            if (ciudad.ID_CIUDAD <= 0) {

                this._context.Ciudad.Add(ciudad);

            } else {

                var estadoDB = this._context.Ciudad
                    .Where(ciu => ciu.ID_CIUDAD == ciudad.ID_CIUDAD)
                    .SingleOrDefault();

                estadoDB.Ciudad1 = ciudad.Ciudad1;

            }

            this._context.SaveChanges();

            return RedirectToAction("Ciudades", "Directorio");
        }

        #endregion

    }

}