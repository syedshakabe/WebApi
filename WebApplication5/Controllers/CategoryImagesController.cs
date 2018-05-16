using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Store2DoorDataAccess;

namespace WebApplication5.Controllers
{
    public class CategoryImagesController : ApiController
    {




        public IEnumerable<CategoryImage> Get()
        {
            using (Store2DoorEntities entities = new Store2DoorEntities())
            {
                return entities.CategoryImages.ToList();
            }
        }





        public HttpResponseMessage Get(int id)
        {
            using (Store2DoorEntities entities = new Store2DoorEntities())
            {
                var entity = entities.CategoryImages.FirstOrDefault(e => e.category_id == id);
                {
                    if (entity != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Category image with id " + id.ToString() + " not found");
                    }
                }
            }
        }






        public HttpResponseMessage Put(int id, [FromBody]CategoryImage category)
        {
            try
            {
                using (Store2DoorEntities entities = new Store2DoorEntities())
                {
                    var entity = entities.CategoryImages.FirstOrDefault(e => e.category_id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Category image with id " + id.ToString() + " not found to edit");
                    }
                    else
                    {
                        entity.images = category.images;




                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.OK, ex);
            }
        }






        public HttpResponseMessage Post([FromBody]CategoryImage category)
        {
            try
            {
                using (Store2DoorEntities entities = new Store2DoorEntities())
                {
                    entities.CategoryImages.Add(category);
                    entities.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, category);
                    message.Headers.Location = new Uri(Request.RequestUri + category.category_id.ToString());
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
                    var entity = entities.CategoryImages.FirstOrDefault(e => e.category_id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Category image with id " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        entities.CategoryImages.Remove(entity);
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
