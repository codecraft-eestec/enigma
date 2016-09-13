using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace CodeCraft.Controllers
{
    public class UsersController : Controller
    {
        //
        // GET: /Users/
        /// Get all available users.
        /// </summary>
        /// <returns>JsonArray of all available users.</returns>
        public JsonResult Get()
        {

            return new JsonResult();
        }
        /// <summary>
        /// Get an user with specified id from database.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult Get(int Id)
        {
            return new JsonResult();
        }
        /// <summary>
        /// Adds new users to the database and returns status.
        /// </summary>
        /// <param name="users">New users.</param>
        /// <returns>Status of a transaction.</returns>
        public int Add([FromBody] JsonResult users)
        {
            return 1;
        }
        /// <summary>
        /// Deletes specifies users from database and returns status.
        /// </summary>
        /// <param name="Id">Id of a users to be deleted.</param>
        /// <returns>Status of a transaction.</returns>
        public int Delete(int Id)
        {
            return 1;
        }

    }
}
