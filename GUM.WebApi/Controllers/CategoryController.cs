using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GUM.DAL;
using GUM.ViewModels;

namespace GUM.WebApi.Controllers
{
    public class CategoryController : ApiController
    {
        CategoryManager mgr = null;
        public CategoryController()
        {
            mgr = new CategoryManager();
        }

        // GET: api/Category
        public HttpResponseMessage Get()
        {
            var categories=mgr.GetAll();
            categories.ForEach(x => { x.Image = String.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(File.ReadAllBytes(x.Image))); });
            if (categories != null)
                return this.Request.CreateResponse(HttpStatusCode.OK, categories);
            else
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError);
        }

        // GET: api/Category/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Category
        public HttpResponseMessage Post(Category category)
        {
            var fileName=Helpers.ImageHelper.SaveImage(category.Image);
            if (fileName != null)
            {
                category.Image = fileName;
                mgr.Add(category);
                var categories = mgr.GetAll();
                categories.ForEach(x => 
                { 
                    x.Image = String.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(File.ReadAllBytes(x.Image))); 
                });
                return this.Request.CreateResponse(HttpStatusCode.OK,categories);
            }
            else
            {
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        // PUT: api/Category/5
        [Route("api/Category/Update")]
        public HttpResponseMessage Update(Category category)
        {
            string fileName = "";
            if (category.Image != null)
            {
                var cate = mgr.GetByID(category.CategoryID);
                GUM.WebApi.Helpers.ImageHelper.DeleteImage(cate.Image);
                fileName = Helpers.ImageHelper.SaveImage(category.Image);
                category.Image = fileName;
                mgr.Update(category);
            }
            else
            {
                mgr.Update(category);
            }
            
            var categories = mgr.GetAll();
            categories.ForEach(x => { x.Image = String.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(File.ReadAllBytes(x.Image))); });
            return this.Request.CreateResponse(HttpStatusCode.OK,categories);
        }

        // DELETE: api/Category/5
        [Route("api/Category/DeleteCategory")]
        public HttpResponseMessage GetDeleteCategory([FromUri]int id)
        {
            var cate = mgr.GetByID(id);
            GUM.WebApi.Helpers.ImageHelper.DeleteImage(cate.Image);
            if (mgr.Delete(id)) 
            {
                var categories = mgr.GetAll();
                categories.ForEach(x => { x.Image = String.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(File.ReadAllBytes(x.Image))); });
                return this.Request.CreateResponse(HttpStatusCode.OK,categories);
            }
            else
            {
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        }
    }
}
