using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Http;
using System.Web.Http;
using GUM.DAL;
using GUM.ViewModels;

namespace GUM.WebApi.Controllers
{
    public class ProductController : ApiController
    {
        ProductManager mgr = null;
        public ProductController()
        {
            mgr = new ProductManager();
        }
        // GET: api/Product
        public HttpResponseMessage Get()
        {
            var products = mgr.GetAll();
            if (products != null)
            {
                foreach (var p in products)
                {
                    p.ProductImages.ForEach(x => { x.Image = String.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(File.ReadAllBytes(x.Image))); });
                    //p.Images = new string[p.ProductImages.Count];
                    //for (var i=0; i< p.ProductImages.Count;i++)
                    //{
                    //    p.Images[i] = String.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(File.ReadAllBytes(p.ProductImages[i].Image)));
                    //}
                }
                return this.Request.CreateResponse(HttpStatusCode.OK,products);
            }
            else
            {
                return this.Request.CreateResponse(HttpStatusCode.NotFound);
            }

        }

        // GET: api/Product/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Product
        public HttpResponseMessage Post(Product product)
        {
            string[] images = new string[product.Images.Length];
            for (var i= 0;i < product.Images.Length;i++)
            {
                images[i]=Helpers.ImageHelper.SaveImage(product.Images[i]);
            }
            if (images != null)
            {
                product.Images = images;
                mgr.Add(product);
                return this.Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        // PUT: api/Product/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Product/5
        public void Delete(int id)
        {
        }
    }
}
