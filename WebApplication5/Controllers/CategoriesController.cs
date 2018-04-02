using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Store2DoorDataAccess;

namespace WebApplication5.Controllers
{
    public class CategoriesController : ApiController
    {


        public IEnumerable<Category> Get()
        {
            using (CategoryEntities entities = new CategoryEntities())
            {
                return entities.Categories.ToList();
            }
        }






        public HttpResponseMessage Put(int id, [FromBody]Category category)
        {
            try
            {
                using (CategoryEntities entities = new CategoryEntities())
                {
                    var entity = entities.Categories.FirstOrDefault(e => e.id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Category with id " + id.ToString() + " not found to edit");
                    }
                    else
                    {
                        entity.category1 = category.category1;
                        entity.image = category.image;


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
    }
}
