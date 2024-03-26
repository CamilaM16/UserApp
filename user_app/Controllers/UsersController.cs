using Microsoft.AspNet.OData;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using user_app.Models;

namespace user_app.Controllers
{
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


    }
}