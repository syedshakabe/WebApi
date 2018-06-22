using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Store2DoorDataAccess;
namespace WebApplication5.Controllers
{
    public class CategoryController : ApiController
    {
       
        
        
        public IEnumerable<Category> Get()
        {
            using (Store2DoorEntities entities = new Store2DoorEntities())
            {
                return entities.Categories.ToList();
            }
        }







        public HttpResponseMessage Get(int id)
        {
            using (Store2DoorEntities entities = new Store2DoorEntities())
            {
                var entity = entities.Categories.FirstOrDefault(e => e.id == id);
                {
                    if (entity != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Category with id " + id.ToString() + " not found");
                    }
                }
            }
        }











        public HttpResponseMessage Put(int id, [FromBody]Category category)
        {
            try
            {
                using (Store2DoorEntities entities = new Store2DoorEntities())
                {
                    var entity = entities.Categories.FirstOrDefault(e => e.id == id);
                    entities.CategoryImages.FirstOrDefault(e => e.category_id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Category with id " + id.ToString() + " not found to edit");
                    }
                    else
                    {
                        entity.category1 = category.category1;
                       entity.CategoryImage.images = category.CategoryImage.images;
                        //entity1.images= category.CategoryImage.images;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }

                }
            }
            catch (System.Data.Entity.Core.UpdateException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }

            catch (System.Data.Entity.Infrastructure.DbUpdateException exc) //DbContext
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exc);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }











        public HttpResponseMessage Post([FromBody]Category category)
        {
            try
            {
                using (Store2DoorEntities entities = new Store2DoorEntities())
                {
                    entities.Categories.Add(category);
                    entities.CategoryImages.Add(category.CategoryImage);
                    entities.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, category);
                    message.Headers.Location = new Uri(Request.RequestUri + category.id.ToString());
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
                using (Store2DoorEntities entities = new Store2DoorEntities())
                {
                    var entity = entities.Categories.FirstOrDefault(e => e.id == id);
                    var product = entities.Products.FirstOrDefault(x => x.category_id == id);
                    var image = entities.CategoryImages.FirstOrDefault(y => y.category_id == id);


                    if(image!=null)
                    {
                        entities.CategoryImages.Remove(image);
                    }

                    if (product == null)
                    {
                        if (entity == null)
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Category with id " + id.ToString() + " not found to delete");
                        }
                        else
                        {
                            entities.Categories.Remove(entity);
                            entities.SaveChanges();
                            return Request.CreateResponse(HttpStatusCode.OK);
                        }
                    }

                    else
                    {
                        entities.Products.Remove(product);
                        entities.Categories.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);

                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


    }
}
