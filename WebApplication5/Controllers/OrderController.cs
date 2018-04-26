using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Store2DoorDataAccess;
using System.Data.Entity;

namespace WebApplication5.Controllers
{
    public class OrderController : ApiController
    {
       Store2DoorEntities db = new Store2DoorEntities();


        public OrderController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

      



        public HttpResponseMessage Get()
        {
            using (Store2DoorEntities entities = new Store2DoorEntities())
            {

              //recieving all orders

                var result = (
                    from u in entities.AspNetUsers
                    join o in entities.Orders
                    on u.Id equals o.user_id
                    where o.status!="delivered"
                    select new
                    {
                       // userId = u.Id,
                        userPhoneNumber = u.PhoneNumber,
                        userName=u.DisplayName,
                        UserEmail = u.Email,
                        TotalBill = o.total_bill,
                        OrderStatus = o.status,
                        order_id = o.order_id,
                        orderDate = o.date
                    }).ToList();

                if(result!=null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "No pending orders");
                }
                
            }
        }

        public HttpResponseMessage Get(int id)
        {
            using (Store2DoorEntities entities = new Store2DoorEntities())
            {
               
              
                //recieving orders
                   return Request.CreateResponse(HttpStatusCode.OK, (
                    from u in entities.AspNetUsers
                    join o in entities.Orders
                    on u.Id equals o.user_id
                    where o.order_id==id
                    select new
                    {
                       // Userid = u.Id,
                        UserName = u.DisplayName,
                        UserEmail = u.Email,
                        userPhone = u.PhoneNumber,
                        TotalBill = o.total_bill,
                        OrderStatus = o.status,
                        order_id = o.order_id
                    }).ToList());

            }


             
                
            }


        [Authorize]
        public HttpResponseMessage Post([FromBody] Order order)
        {
            try
            {
                using (Store2DoorEntities entities = new Store2DoorEntities())
                {
                    Order n = new Order();
                    n.status = "pending";
                    n.total_bill = Convert.ToDecimal(order.total_bill);
                    n.user_id = order.user_id;
                    n.date = DateTime.Now.AddHours(5) ;
                             entities.Orders.Add(n);
                             entities.SaveChanges();
                             var message = Request.CreateResponse(HttpStatusCode.Created, n);
                             message.Headers.Location = new Uri(Request.RequestUri + n.order_id.ToString());
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
                using (Store2DoorEntities entities = new Store2DoorEntities())
                {
                    var entity = entities.Orders.FirstOrDefault(e => e.order_id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Order with id " + id.ToString() + " not found to edit");
                    }
                    else
                    {
                        entity.status =order.status;

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













        [Authorize]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (Store2DoorEntities entities = new Store2DoorEntities())
                {
                    var entity = entities.Orders.FirstOrDefault(e => e.order_id == id);
                    var cart = entities.Cart_Item.FirstOrDefault(x => x.order_id == id);
                    if (cart == null)
                    {
                        if (entity == null)
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Order with id " + id.ToString() + " not found to delete");
                        }
                        else
                        {
                            entities.Orders.Remove(entity);
                            entities.SaveChanges();
                            return Request.CreateResponse(HttpStatusCode.OK);
                        }
                    }
                    else
                    {
                        entities.Cart_Item.Remove(cart);
                        entities.Orders.Remove(entity);
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

