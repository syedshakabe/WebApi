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

         
       Store2DoorEntities db = new Store2DoorEntities();


        public Store2DoorController()
        {
            db.Configuration.ProxyCreationEnabled = false;
            db.Configuration.AutoDetectChangesEnabled = false;
        }

       
       
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
                var entity = entities.Products.Where(e=>e.category_id==id).ToList();
                
                //foreach (var x in entities.Products)
                //{
                //    if (x.category_id == id)
                //    {
                //        entity.Add(x);
                //    }
                //}

             
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
                var cart = entities.Cart_Item.FirstOrDefault(x => x.product_id == id);
                if(cart==null)
                {
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
                else
                {
                    entities.Cart_Item.Remove(cart);
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
