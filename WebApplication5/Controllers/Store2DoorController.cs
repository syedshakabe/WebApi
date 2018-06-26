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
               
                    if(entity.Count!=0)
                    {

                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product with category " + id.ToString() + " not found");
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
                    entities.ProductImages.Add(product.ProductImage);
                    entities.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, product);
                    message.Headers.Location = new Uri(Request.RequestUri + product.id.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, ex);
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            try { 
            using(Store2DoorEntities entities = new Store2DoorEntities())
            {
                var entity = entities.Products.FirstOrDefault(e => e.id == id);
                var cart = entities.Cart_Item.Where(x => x.product_id == id).Select(x=>x).ToList();
                var img = entities.ProductImages.FirstOrDefault(y => y.product_id== id);
               

                if(entity==null)
                {
                   return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product with id " + id.ToString() + " not found to delete");
                }
                else 
                {
                     if(cart.Count==0)
                     {
                         if(img!=null)
                         {
                             entities.ProductImages.Remove(img);
                             entities.SaveChanges();
                         }
                         entities.Products.Remove(entity);
                         entities.SaveChanges();
                         return Request.CreateResponse(HttpStatusCode.OK,entity);
                     }
                     else
                     {
                         foreach (var x in cart)
                         {
                             entities.Cart_Item.Remove(x);
                         }
                         
                         if (img != null)
                         {
                             entities.ProductImages.Remove(img);
                             entities.SaveChanges();
                         }
                         entities.Products.Remove(entity);
                         entities.SaveChanges();
                         return Request.CreateResponse(HttpStatusCode.OK, entity);
                     }
                }
               
                
                
               
            }
            }catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, ex);
            }
        }
        public HttpResponseMessage Put(int id ,[FromBody]Product product)
        {
            try
            {
                using (Store2DoorEntities entities = new Store2DoorEntities())
                {
                    var entity = entities.Products.FirstOrDefault(e => e.id == id);
                    entities.ProductImages.FirstOrDefault(x => x.product_id == id);
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
                        entity.ProductImage.image = product.ProductImage.image;

                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }

                }
            }catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, ex);
            }
        }
    }
}
