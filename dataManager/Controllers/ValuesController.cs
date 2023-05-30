using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace dataManager.Controllers
{
    [Authorize] /* All the end points in this controller are secured and require authentication */
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            /* With the code below, we are able to fetch the id and name of the user who is making the request from the users table in db. */
            string userId = RequestContext.Principal.Identity.GetUserId();
            string userName = RequestContext.Principal.Identity.GetUserName();
            return new string[] { "value1", "value2", userId+": it was the user id", $"{userName}" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
