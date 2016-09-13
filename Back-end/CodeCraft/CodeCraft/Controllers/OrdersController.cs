using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using CodeCraft.Models;
using CodeCraft.Models.Nodes;
using Newtonsoft.Json;

namespace CodeCraft.Controllers
{
    public class OrdersController : Controller
    {
        //
        // GET: /Orders/
        /// Get all available orders.
        /// </summary>
        /// <returns>JsonArray of all available orders.</returns>
        public JsonResult Get()
        {
            DataProvider provider = new DataProvider();
            OrderResult orderResult = provider.getAllOrders();
            JsonResult result = new JsonResult();
            string data = "{";
            data += "{\n\t\"id\":\"" + orderResult.order.id + "\"" + "\n\t\"user\":\"" + orderResult.order.user + "\"" +
                    "\n\t\"date\":\"" + orderResult.order.date + "\"" + "\n\t\"status\":\"" + orderResult.order.status +
                    "\"\n\tmeals:[";
            foreach (KeyValuePair<string, string> order in orderResult.allOrders)
            {
                {
                    data += "{\n\t\"meal name\":\"" + order.Key + "\"" + "\n\t\"quantity\":\"" + order.Value + "\"\n},";
                }
            }
            data = data.Substring(0, data.Length - 1);
            data += "]\n}";
            result.Data = data;
            return result;
        }
        /// <summary>
        /// Get an order with specified id from database.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult Get(int Id)
        {
            return new JsonResult();
        }
        /// <summary>
        /// Adds new orders to the database and returns status.
        /// </summary>
        /// <param name="orders">New orders.</param>
        /// <returns>Status of a transaction.</returns>
        public int Add([FromBody] JsonResult orders)
        {
            Meal orderObj = JsonConvert.DeserializeObject<Meal>(orders.Data.ToString());
            DataProvider provider = new DataProvider();
            return provider.createOrder(orderObj); ;
        }
        /// <summary>
        /// Deletes specifies orders from database and returns status.
        /// </summary>
        /// <param name="Id">Id of a orders to be deleted.</param>
        /// <returns>Status of a transaction.</returns>
        public int Delete(int Id)
        {
            DataProvider provider = new DataProvider();
            return provider.deleteOrder(Id);
        }

    }
}
