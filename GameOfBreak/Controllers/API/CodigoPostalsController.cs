using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Xml.Linq;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using GameOfBreak.Models.GoB;
using GameOfBreak.Dtos;
using Newtonsoft.Json.Linq;

namespace GameOfBreak.Controllers.API
{
    public class CodigoPostalsController : ApiController
    {
        private GameOfBreakModel db;

        public CodigoPostalsController () {
            this.db = new GameOfBreakModel();
        }

        [HttpGet]
        // GET: api/CodigoPostals
        public IQueryable<CodigoPostal> GetCodigoPostal ()
        {
            var codigosPostales = db.CodigoPostal
                .Include(col => col.Colonia)
                .Include(md => md.MunicipioDelegacion)
                .Include(e => e.Estado)
                .Include(c => c.Ciudad)
                .OrderBy(o => o.ID_CP)
                .Skip(100).Take(100);
                
                //.Select(AutoMapper.Mapper.Map<CodigoPostal, CodigoPostalDto>)

            return codigosPostales;
        }

        // GET: api/CodigoPostals/5
        [ResponseType(typeof(CodigoPostal))]
        public IHttpActionResult GetCodigoPostal(int id)
        {
            CodigoPostal codigoPostal = db.CodigoPostal.Find(id);
            if (codigoPostal == null)
            {
                return NotFound();
            }

            return Ok(codigoPostal);
        }

        // PUT: api/CodigoPostals/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCodigoPostal(int id, CodigoPostal codigoPostal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != codigoPostal.ID_CP)
            {
                return BadRequest();
            }

            db.Entry(codigoPostal).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CodigoPostalExists(id))
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

        // POST: api/CodigoPostals
        [ResponseType(typeof(CodigoPostal))]
        public IHttpActionResult PostCodigoPostal(CodigoPostal codigoPostal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CodigoPostal.Add(codigoPostal);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = codigoPostal.ID_CP }, codigoPostal);
        }

        // DELETE: api/CodigoPostals/5
        [ResponseType(typeof(CodigoPostal))]
        public IHttpActionResult DeleteCodigoPostal(int id)
        {
            CodigoPostal codigoPostal = db.CodigoPostal.Find(id);
            if (codigoPostal == null)
            {
                return NotFound();
            }

            db.CodigoPostal.Remove(codigoPostal);
            db.SaveChanges();

            return Ok(codigoPostal);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CodigoPostalExists(int id)
        {
            return db.CodigoPostal.Count(e => e.ID_CP == id) > 0;
        }
    }
}