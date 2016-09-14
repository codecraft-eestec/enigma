using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
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
        public JsonResult GetAll()
        {
            DataProvider provider = new DataProvider();
            OrderResult orderResult = provider.getAllOrders();
            return Json(orderResult, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Get an order with specified id from database.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult GetOne(string name)
        {
            DataProvider provider = new DataProvider();
            Meal orderResult = provider.getMeal(name);
            return Json(orderResult, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Adds new orders to the database and returns status.
        /// </summary>
        /// <param name="orders">New orders.</param>
        /// <returns>Status of a transaction.</returns>
        public int Add([FromBody] OrderResult orders)
        {
            DataProvider provider = new DataProvider();
            provider.createOrder(orders); 
            return 0;
        }
        /// <summary>
        /// Deletes specifies orders from database and returns status.
        /// </summary>
        /// <param name="Id">Id of a orders to be deleted.</param>
        /// <returns>Status of a transaction.</returns>
        public int Delete(string name, string date)
        {
            DataProvider provider = new DataProvider();
            return provider.deleteOrder(name, date);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="meal"></param>
        /// <returns></returns>
        public int Update([FromBody] Order order)
        {
            DataProvider provider = new DataProvider();
            return provider.updateOrder(order);
        }

    }
}
