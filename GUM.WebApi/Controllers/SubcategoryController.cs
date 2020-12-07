using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GUM.DAL;
using GUM.ViewModels;

namespace GUM.WebApi.Controllers
{
    public class SubcategoryController : ApiController
    {
        SubcategoryManager mgr = null;
        public SubcategoryController()
        {
            mgr = new SubcategoryManager();
        }
        // GET: api/Subcategory
        public HttpResponseMessage Get()
        {
            var subcategories = mgr.GetAll();
            subcategories.ForEach(x => { x.Image = String.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(File.ReadAllBytes(x.Image))); });
            if (subcategories != null)
                return this.Request.CreateResponse(HttpStatusCode.OK, subcategories);
            else
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError);
        }

        // GET: api/Subcategory/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Subcategory
        public HttpResponseMessage Post(Subcategory subcategory)
        {
            var fileName = Helpers.ImageHelper.SaveImage(subcategory.Image);
            if (fileName != null)
            {
                subcategory.Image = fileName;
                mgr.Add(subcategory);
                var subcategories = mgr.GetAll();
                subcategories.ForEach(x =>
                {
                    x.Image = String.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(File.ReadAllBytes(x.Image)));
                });
                return this.Request.CreateResponse(HttpStatusCode.OK, subcategories);
            }
            else
            {
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        }

        // PUT: api/Subcategory/5
        [Route("api/Subcategory/Update")]
        public HttpResponseMessage Update(Subcategory subcategory)
        {
            string fileName = "";
            if (subcategory.Image != null)
            {
                var subcate = mgr.GetByID(subcategory.SubcategoryID);
                GUM.WebApi.Helpers.ImageHelper.DeleteImage(subcate.Image);
                fileName = Helpers.ImageHelper.SaveImage(subcategory.Image);
                subcategory.Image = fileName;
                mgr.Update(subcategory);
            }
            else
            {
                mgr.Update(subcategory);
            }

            var subcategories = mgr.GetAll();
            subcategories.ForEach(x => { x.Image = String.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(File.ReadAllBytes(x.Image))); });
            return this.Request.CreateResponse(HttpStatusCode.OK, subcategories);
        }

        // DELETE: api/Subcategory/5
        [Route("api/Subcategory/DeleteSubcategory")]
        public HttpResponseMessage GetDeleteSubcategory([FromUri] int id)
        {
            var subcate = mgr.GetByID(id);
            GUM.WebApi.Helpers.ImageHelper.DeleteImage(subcate.Image);
            if (mgr.Delete(id))
            {
                var subcategories = mgr.GetAll();
                subcategories.ForEach(x => { x.Image = String.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(File.ReadAllBytes(x.Image))); });
                return this.Request.CreateResponse(HttpStatusCode.OK, subcategories);
            }
            else
            {
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        }
    }
}
