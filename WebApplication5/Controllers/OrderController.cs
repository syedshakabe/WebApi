using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Store2DoorDataAccess;

namespace WebApplication5.Controllers
{
    public class OrderController : ApiController
    {

        public IEnumerable<Order> Get()
        {
            using (OrderEntities entities = new OrderEntities())
            {
                return entities.Orders.ToList();
            }
        }



        public HttpResponseMessage Post([FromBody]Order order)
        {
            try
            {
                using (OrderEntities entities = new OrderEntities())
                {
                    entities.Orders.Add(order);
                    entities.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, order);
                    message.Headers.Location = new Uri(Request.RequestUri +order.order_id.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }





        public HttpResponseMessage Put(int id, [FromBody]Order order)
        {
            try
            {
                using (OrderEntities entities = new OrderEntities())
                {
                    var entity = entities.Orders.FirstOrDefault(e => e.order_id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Order with id " + id.ToString() + " not found to edit");
                    }
                    else
                    {
                        entity.status = order.status;
                        
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
