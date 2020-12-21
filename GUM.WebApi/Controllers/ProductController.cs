using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using GUM.DAL;
using GUM.ViewModels;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

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
                //foreach (var p in products)
                //{
                //   // p.ProductImages.ForEach(x => { x.Image = String.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(File.ReadAllBytes(x.Image))); });
                //    //p.Images = new string[p.ProductImages.Count];
                //    //for (var i=0; i< p.ProductImages.Count;i++)
                //    //{
                //    //    p.Images[i] = String.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(File.ReadAllBytes(p.ProductImages[i].Image)));
                //    //}
                //}
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

        [HttpGet]
        [Route("api/Product/GetImages")]
        public HttpResponseMessage GetImages([FromUri]int id)
        {
            var productImages=mgr.GetImagesUsingProductID(id);           
            for (var i = 0; i < productImages.Count; i++)
            {
                productImages[i].Image = String.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(File.ReadAllBytes(productImages[i].Image)));
            }
            if (productImages != null)
                return Request.CreateResponse(HttpStatusCode.OK, productImages);
            else
                return Request.CreateResponse(HttpStatusCode.NotFound);
        }
        
        [HttpPost]
        [Route("api/Product/PostAddImages/")]
        public HttpResponseMessage PostAddImages(Product product)
        {
            string[] images = new string[product.Images.Length];
            for (var i = 0; i < product.Images.Length; i++)
            {
                images[i] = Helpers.ImageHelper.SaveImage(product.Images[i]);
            }
            if (images != null)
            {
                product.Images = images;
                mgr.AddImagesToProduct(product);
                var productImages = mgr.GetImagesUsingProductID(product.ProductID);
                for (var i = 0; i < productImages.Count; i++)
                {
                    productImages[i].Image = String.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(File.ReadAllBytes(productImages[i].Image)));
                }
                return this.Request.CreateResponse(HttpStatusCode.OK,productImages);
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
        [Route("api/Product/GetDeleteImage")]
        public HttpResponseMessage GetDeleteImage([FromUri]int id)
        {
            var productImage = mgr.GetProductImageByID(id);
            Helpers.ImageHelper.DeleteImage(productImage.Image);
            if (mgr.DeleteProductImage(id))
            {
                var productImages = mgr.GetImagesUsingProductID(productImage.ProductID);
                for (var i = 0; i < productImages.Count; i++)
                {
                    productImages[i].Image = String.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(File.ReadAllBytes(productImages[i].Image)));
                }
                return this.Request.CreateResponse(HttpStatusCode.OK, productImages);
            }
            else
            {
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/Product/GetStocks")]
        public HttpResponseMessage GetStocks([FromUri] int id)
        {
            var stocks = mgr.GetStocks(id);
            if (stocks != null)
                return Request.CreateResponse(HttpStatusCode.OK, stocks);
            else
                return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        [HttpPost]
        [Route("api/Product/PostManageStock/")]
        public HttpResponseMessage PostManageStock(Product product)
        {

                mgr.UpdateStock(product);
                return this.Request.CreateResponse(HttpStatusCode.OK);

        }

        [HttpPost]
        [Route("api/Product/PostUpdateDetails/")]
        public HttpResponseMessage PostUpdateDetails(Product product)
            {

            if (mgr.Update(product))
            {
                var products = mgr.GetAll();
                return this.Request.CreateResponse(HttpStatusCode.OK,products);
            }
            else
            {
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        }
    }
}
