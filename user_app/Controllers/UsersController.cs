using Microsoft.AspNet.OData;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using user_app.Models;

namespace user_app.Controllers
{
    [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "*")]
    public class UsersController : ODataController
    {
        readonly UsersContext db = new UsersContext();

        [HttpGet]
        [EnableQuery]
        public IQueryable<User> Get()
        {
            return db.Users;
        }

        [EnableQuery]
        public SingleResult<User> Get([FromODataUri] Guid key)
        {
            IQueryable<User> result = db.Users.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }

        public async Task<IHttpActionResult> Post(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return Created(new { User = user, ModelState = ModelState });
        }
        public async Task<IHttpActionResult> Patch([FromODataUri] Guid key, Delta<User> user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await db.Users.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            user.Patch(entity);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Updated(entity);
        }
        public async Task<IHttpActionResult> Put([FromODataUri] Guid key, User update)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (key != update.Id)
            {
                return BadRequest();
            }
            db.Entry(update).State = EntityState.Modified;
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Updated(update);
        }
        private bool UserExists(Guid key)
        {
            return db.Users.Any(u => u.Id == key);
        }
        public async Task<IHttpActionResult> Delete([FromODataUri] Guid key)
        {
            var product = await db.Users.FindAsync(key);
            if (product == null)
            {
                return NotFound();
            }
            db.Users.Remove(product);
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}