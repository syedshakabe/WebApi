using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Store2DoorDataAccess;
namespace WebApplication5.Controllers
{
    public class ProductsController : ApiController
    {
        public IEnumerable<Product> Get()
        {
            using (Store2DoorEntities entities = new Store2DoorEntities())
            {
                return entities.Products.ToList();
            }
        }



    }
}
