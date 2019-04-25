using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Clock.API.Helpers;
using Clock.Common.Models;
using Clock.Domain.Models;

namespace Clock.API.Controllers
{
    public class RegistersController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Registers
        public IQueryable<Register> GetRegisters()
        {
            return db.Registers;
        }

        // GET: api/Registers/5
        [ResponseType(typeof(Register))]
        public async Task<IHttpActionResult> GetRegister(int id)
        {
            Register register = await db.Registers.FindAsync(id);
            if (register == null)
            {
                return NotFound();
            }

            return Ok(register);
        }

        // PUT: api/Registers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRegister(int id, Register register)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != register.RegistroId)
            {
                return BadRequest();
            }

            db.Entry(register).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegisterExists(id))
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

        // POST: api/Registers
        [ResponseType(typeof(Register))]
        public async Task<IHttpActionResult> PostRegister(Register register)
        {
            register.Entrada = DateTime.Now;
            register.Tiempo = DateTime.Now;
            register.Salida = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(register.ImageArray !=null && register.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(register.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                var folder = "~/Content/Registers";
                var fullPath = $"{folder}/{file}";
                var response = FilesHelper.UploadPhoto(stream, folder, file);
                if (response)
                {
                    register.ImagePath = fullPath;
                }
            }

            this.db.Registers.Add(register);
            await this.db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = register.RegistroId }, register);
        }

        // DELETE: api/Registers/5
        [ResponseType(typeof(Register))]
        public async Task<IHttpActionResult> DeleteRegister(int id)
        {
            Register register = await db.Registers.FindAsync(id);
            if (register == null)
            {
                return NotFound();
            }

            db.Registers.Remove(register);
            await db.SaveChangesAsync();

            return Ok(register);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RegisterExists(int id)
        {
            return db.Registers.Count(e => e.RegistroId == id) > 0;
        }
    }
}