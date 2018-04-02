using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Store2DoorDataAccess;

namespace WebApplication5.Controllers
{
    public class CartController : ApiController
    {
       public IEnumerable<Cart_Item>Get()
        {
            using (CartEntities entities = new CartEntities())
            {
                return entities.Cart_Item.ToList();
            }
        }



       public HttpResponseMessage Post([FromBody]Cart_Item cart)
       {
           try
           {
               using (CartEntities entities = new CartEntities())
               {
                   entities.Cart_Item.Add(cart);
                   entities.SaveChanges();
                   var message = Request.CreateResponse(HttpStatusCode.Created, cart);
                   message.Headers.Location = new Uri(Request.RequestUri + cart.cart_id.ToString());
                   return message;
               }
           }
           catch (Exception ex)
           {
               return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
           }
       }






       public HttpResponseMessage Get(int id)
       {
           using (CartEntities entities = new CartEntities())
           {
               var entity = entities.Cart_Item.FirstOrDefault(e => e.order_id == id);
               {
                   if (entity != null)
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




    }
}
