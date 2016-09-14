using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using CodeCraft.Models;
using CodeCraft.Models.Nodes;
using Newtonsoft.Json;

namespace CodeCraft.Controllers
{
    public class MealsController : Controller
    {
        //
        // GET: /Meals/
        /// <summary>
        /// Get all available meals.
        /// </summary>
        /// <returns>JsonArray of all available meals.</returns>
        public JsonResult GetAll()
        {
            DataProvider provider = new DataProvider();
            Meal[] meals = provider.getAllMeals();
            return Json(meals, JsonRequestBehavior.AllowGet);
        }
        
        /// <summary>
        /// Gets a meal with specified id from database.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult GetOne(string name)
        {
            DataProvider provider = new DataProvider();
            Meal meal = provider.getMeal(name);
            return Json(meal, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Adds new meal to the database and returns status.
        /// </summary>
        /// <param name="meal">New meal.</param>
        /// <returns>Status of a transaction.</returns>
        public int Add([FromBody] Meal meal)
        {
            DataProvider provider = new DataProvider();
            return provider.createMeal(meal); ;
        }
        /// <summary>
        /// Deletes specifies meal from database and returns status.
        /// </summary>
        /// <param name="Id">Id of a meal to be deleted.</param>
        /// <returns>Status of a transaction.</returns>
        public int Delete(string name)
        {;
            DataProvider provider = new DataProvider();
            return provider.deleteMeal(name); ;
        }
        /// <summary>
        /// Updates specifies meal from database and returns status.
        /// </summary>
        /// <param name="meal"></param>
        /// <returns></returns>
        public int Update([FromBody] Meal meal)
        {
            DataProvider provider = new DataProvider();
            return provider.updateMeal(meal);
        }
    }
}
