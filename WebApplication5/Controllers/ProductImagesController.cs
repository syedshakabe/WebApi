using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Store2DoorDataAccess;

namespace WebApplication5.Controllers
{
    public class ProductImagesController : ApiController
    {



         public IEnumerable<ProductImage>Get()
        {
            using(Store2DoorEntities entities = new Store2DoorEntities())
            {
                return entities.ProductImages.ToList();
            }
        }
        public HttpResponseMessage Get(int id)
        {
            using (Store2DoorEntities entities = new Store2DoorEntities())
            {
                var entity =  entities.ProductImages.FirstOrDefault(e => e.product_id == id);
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
        public HttpResponseMessage Post([FromBody] ProductImage product)
        {
            try
            {
                using (Store2DoorEntities entities = new Store2DoorEntities())
                {
                    entities.ProductImages.Add(product);
                    entities.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, product);
                    message.Headers.Location = new Uri(Request.RequestUri + product.product_id.ToString());
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
            try 
            { 
            using(Store2DoorEntities entities = new Store2DoorEntities())
            {
                var entity = entities.ProductImages.FirstOrDefault(e => e.product_id == id);
              
              
                    entities.ProductImages.Remove(entity);
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);

              
                
               
            }
            }catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
      
        
        
        
        
        
        
        
        
        
        public HttpResponseMessage Put(int id ,[FromBody]ProductImage product)
        {
            try
            {
                using (Store2DoorEntities entities = new Store2DoorEntities())
                {
                    var entity = entities.ProductImages.FirstOrDefault(e => e.product_id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product image with id " + id.ToString() + " not found to edit");
                    }
                    else
                    {
                       entity.image=product.image;

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












    
