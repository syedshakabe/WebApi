using Store2DoorDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication5.Controllers
{
    public class CartController : ApiController
    {







        public HttpResponseMessage Get()
        {
            using (Store2DoorEntities entities = new Store2DoorEntities())
            {

                //recieving all carts

                var result = (
                    from p in entities.Products
                    join c in entities.Cart_Item
                    on p.id equals c.product_id

                    select new
                    {
                        id = c.cart_id,
                        productName = p.name,
                        productPrice = p.price
                    }).ToList();

                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "NO carts");
                }

            }
        }

        public HttpResponseMessage Get(int id)
        {
            using(Store2DoorEntities entities = new Store2DoorEntities())
            {



               
                var result =  (
                  from o in entities.Orders
                  join c in entities.Cart_Item
                  on o.order_id equals c.order_id
                  join p in entities.Products
                  on c.product_id equals p.id
                  where c.order_id==id && o.order_id==id 
                  select new
                  {
                      CartId = c.cart_id,
                      productId = c.product_id,

                      order_id = o.order_id,

                      productName = p.name,
                      productPrice = p.price
                  }).ToList();


                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "No cart items for this order");
                }


            }
        }



        public HttpResponseMessage Post([FromBody] Cart_Item[] cart)
        {
            try
            {
                using (Store2DoorEntities entities = new Store2DoorEntities())
                {
                    foreach(var x in cart)
                    {
                        entities.Cart_Item.Add(x);
                        entities.SaveChanges();
                    }
                    
                    var message = Request.CreateResponse(HttpStatusCode.Created, cart);
                    message.Headers.Location = new Uri(Request.RequestUri + cart.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}
