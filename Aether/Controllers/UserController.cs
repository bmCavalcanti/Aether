using Aether.Controllers.Context;
using Aether.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Aether.Controllers
{
    public class UserController : ApiController
    {
        private readonly DataBaseContext context;

        public UserController()
        {
            context = new DataBaseContext();
        }

        public IHttpActionResult Get(int Id)
        {
            try
            {
                User user = context.User.Find(Id);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
