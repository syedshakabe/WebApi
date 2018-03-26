﻿using System;
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
            using (CategoryEntity entities = new CategoryEntity())
            {
                return entities.Categories.ToList();
            }
        }
         public HttpResponseMessage Post([FromBody] Category category)
        {
            try
            {
                using (CategoryEntity entities = new CategoryEntity())
                {
                    entities.Categories.Add(category);
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
            try { 
            using(CategoryEntity entities = new CategoryEntity())
            {
                var entity = entities.Categories.FirstOrDefault(e => e.id == id);
                if(entity==null)
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
            }catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public HttpResponseMessage Put(int id ,[FromBody]Category category)
        {
            try
            {
                using (CategoryEntity entities = new CategoryEntity())
                {
                    var entity = entities.Categories.FirstOrDefault(e => e.id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Category with id " + id.ToString() + " not found to edit");
                    }
                    else
                    {
                        entity.category1 = category.category1 ;
                        entity.image = category.image;
                       

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
}
