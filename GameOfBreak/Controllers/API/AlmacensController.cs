using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using GameOfBreak.Models.GoB;

namespace GameOfBreak.Controllers.API
{
    public class AlmacensController : ApiController
    {
        private GameOfBreakModel db = new GameOfBreakModel();

        // GET: api/Almacens
        public IQueryable<Almacen> GetAlmacen()
        {
            return db.Almacen;
        }

        // GET: api/Almacens/5
        [ResponseType(typeof(Almacen))]
        public IHttpActionResult GetAlmacen(int id)
        {
            Almacen almacen = db.Almacen.Find(id);
            if (almacen == null)
            {
                return NotFound();
            }

            return Ok(almacen);
        }

        // PUT: api/Almacens/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAlmacen(int id, Almacen almacen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != almacen.ID_ALMACEN)
            {
                return BadRequest();
            }

            db.Entry(almacen).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlmacenExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Almacens
        [ResponseType(typeof(Almacen))]
        public IHttpActionResult PostAlmacen(Almacen almacen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Almacen.Add(almacen);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = almacen.ID_ALMACEN }, almacen);
        }

        // DELETE: api/Almacens/5
        [ResponseType(typeof(Almacen))]
        public IHttpActionResult DeleteAlmacen(int id)
        {
            Almacen almacen = db.Almacen.Find(id);
            if (almacen == null)
            {
                return NotFound();
            }

            db.Almacen.Remove(almacen);
            db.SaveChanges();

            return Ok(almacen);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AlmacenExists(int id)
        {
            return db.Almacen.Count(e => e.ID_ALMACEN == id) > 0;
        }
    }
}