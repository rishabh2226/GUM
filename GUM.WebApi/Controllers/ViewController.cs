using GUM.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GUM.WebApi.Controllers
{
    public class ViewController : ApiController
    {
        CategoryManager mgrCate = null;
        SubcategoryManager mgrSubcate = null;
        ProductManager mgrPro = null;
        public ViewController()
        {
            mgrCate = new CategoryManager();
            mgrSubcate = new SubcategoryManager();
            mgrPro = new ProductManager();
        }
        // GET: api/View
        [Route("api/View/getCategoryAndSubcategory")]
        public HttpResponseMessage getCategoryAndSubcategory()
        {
            var categories = mgrCate.GetAll();
            var subcategories = mgrSubcate.GetAll();
            var products = mgrPro.getProducts();
            categories.ForEach(x =>
            {
                x.Image = "/Content/images/" + x.Image;
            });
            subcategories.ForEach(x =>
            {
                x.Image = "/Content/images/" + x.Image;
            });
            products.ForEach(x =>
            {
                x.ProductImages.ForEach(y => 
                {
                    y.Image = "/Content/images/" + y.Image;
                });
               
            });
            var obj = new { categories = categories, subcategories =subcategories, products = products };
            return this.Request.CreateResponse(HttpStatusCode.OK,obj);
        }

        [Route("api/View/getProducts")]
        public HttpResponseMessage getProducts()
        {
            var products = mgrPro.getProducts();
            products.ForEach(x =>
            {
                x.ProductImages.ForEach(y =>
                {
                    y.Image = "/Content/images/" + y.Image;
                });

            });
            return this.Request.CreateResponse(HttpStatusCode.OK, products);
        }

        [Route("api/View/getProductFromCategory")]
        public HttpResponseMessage getProductFromCategory([FromUri] int id)
        {
            var products = mgrPro.getProducts(id,0);
            products.ForEach(x =>
            {
                x.ProductImages.ForEach(y =>
                {
                    y.Image = "/Content/images/" + y.Image;
                });

            });
            return this.Request.CreateResponse(HttpStatusCode.OK, products);
        }

        [Route("api/View/getProductFromSubcategory")]
        public HttpResponseMessage getProductFromSubcategory([FromUri] int id)
        {
            var products = mgrPro.getProducts(0, id);
            products.ForEach(x =>
            {
                x.ProductImages.ForEach(y =>
                {
                    y.Image = "/Content/images/" + y.Image;
                });

            });
            return this.Request.CreateResponse(HttpStatusCode.OK, products);
        }
    }
}
