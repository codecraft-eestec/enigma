using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using CodeCraft.Models;
using CodeCraft.Models.Nodes;

namespace CodeCraft.Controllers
{
    public class UsersController : Controller
    {
        /// <summary>
        /// Adds new users to the database and returns status.
        /// </summary>
        /// <param name="users">New users.</param>
        /// <returns>Status of a transaction.</returns>
        public int Register([FromBody] User user)
        {
            DataProvider provider = new DataProvider();
            return provider.registerUser(user);
        }
        //
        // GET: /Users/
        /// Get all available users.
        /// </summary>
        /// <returns>JsonArray of all available users.</returns>
        public JsonResult GetAll()
        {
            DataProvider provider = new DataProvider();
            return Json(provider.getAllUsers(), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Get an user with specified id from database.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult GetOne(string username)
        {
            DataProvider provider = new DataProvider();
            return Json(provider.getUser(username), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Deletes specifies users from database and returns status.
        /// </summary>
        /// <param name="Id">Id of a users to be deleted.</param>
        /// <returns>Status of a transaction.</returns>
        public int Delete(string username)
        {
            DataProvider provider = new DataProvider();
            return provider.deleteUser(username);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="meal"></param>
        /// <returns></returns>
        public int Update([FromBody] User user)
        {
            DataProvider provider = new DataProvider();
            return provider.updateUser(user);
        }
    }
}
