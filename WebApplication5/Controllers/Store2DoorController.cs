using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Store2DoorDataAccess;
using System.Web.Http.Cors;
namespace WebApplication5.Controllers
{
      [EnableCorsAttribute("*", "*", "*")]
     
    public class Store2DoorController : ApiController
    {
       
       
        public IEnumerable<Product>Get()
        {
            using(Store2DoorEntities entities = new Store2DoorEntities())
            {
                return entities.Products.ToList();
            }
        }
        public HttpResponseMessage Get(int id)
        {
            using (Store2DoorEntities entities = new Store2DoorEntities())
            {
                var entity =  entities.Products.FirstOrDefault(e => e.id == id);
                {
                    if(entity!=null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product with id " + id.ToString() + " not found");
                    }
                }
            }
        }
        public HttpResponseMessage Post([FromBody] Product product)
        {
            try
            {
                using (Store2DoorEntities entities = new Store2DoorEntities())
                {
                    entities.Products.Add(product);
                    entities.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, product);
                    message.Headers.Location = new Uri(Request.RequestUri + product.id.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            try { 
            using(Store2DoorEntities entities = new Store2DoorEntities())
            {
                var entity = entities.Products.FirstOrDefault(e => e.id == id);
                if(entity==null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product with id " + id.ToString() + " not found to delete");
                }
                else
                {
                    entities.Products.Remove(entity);
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
               
            }
            }catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public HttpResponseMessage Put(int id ,[FromBody]Product product)
        {
            try
            {
                using (Store2DoorEntities entities = new Store2DoorEntities())
                {
                    var entity = entities.Products.FirstOrDefault(e => e.id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product with id " + id.ToString() + " not found to edit");
                    }
                    else
                    {
                        entity.name = product.name;
                        entity.price = product.price;
                        entity.image = product.image;
                        entity.product_type = product.product_type;
                        entity.quantity_type = product.quantity_type;
                        entity.stock = product.stock;

                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }

                }
            }catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.OK, ex);
            }
        }
    }
}
