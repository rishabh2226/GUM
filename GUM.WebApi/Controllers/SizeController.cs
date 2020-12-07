using GUM.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GUM.WebApi.Controllers
{
    public class SizeController : ApiController
    {
        SizeManager mgr = null;
        public SizeController()
        {
            mgr = new SizeManager();
        }
        // GET: api/Size
        public HttpResponseMessage Get()
        {
            var sizes = mgr.GetAll();
            if (sizes != null)
            {
                return this.Request.CreateResponse(HttpStatusCode.OK,sizes);
            }
            else
            {
                return this.Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        // GET: api/Size/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Size
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Size/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Size/5
        public void Delete(int id)
        {
        }
    }
}
