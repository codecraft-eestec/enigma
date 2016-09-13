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
    public class MealsController : Controller
    {
        //
        // GET: /Meals/
        /// <summary>
        /// Get all available meals.
        /// </summary>
        /// <returns>JsonArray of all available meals.</returns>
        public JsonResult Get()
        {
            DataProvider provider = new DataProvider();
            Meal[] meals = provider.getAllMeals();
            JsonResult result = new JsonResult();
            string data = "[";
            foreach (Meal meal in meals)
            {
                {
                    data += "{\n\t\"id\":\"" + meal.id + "\"" + "\n\t\"name\":\"" + meal.name + "\"" +
                            "\n\t\"description\":\"" + meal.description + "\"" + "\n\t\"picture\":\"" + meal.picture +
                            "\"\n},";
                }
            }
            data = data.Substring(0, data.Length - 1);
            data += "]";
            result.Data = data;
            return result;
        }
        
        /// <summary>
        /// Gets a meal with specified id from database.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult Get(int Id)
        {
            return new JsonResult();
        }
        /// <summary>
        /// Adds new meal to the database and returns status.
        /// </summary>
        /// <param name="meal">New meal.</param>
        /// <returns>Status of a transaction.</returns>
        public int Add([FromBody] JsonResult meal)
        {
            Meal mealObj = JsonConvert.DeserializeObject<Meal>(meal.Data.ToString());
            DataProvider provider = new DataProvider();
            return provider.createMeal(mealObj); ;
        }
        /// <summary>
        /// Deletes specifies meal from database and returns status.
        /// </summary>
        /// <param name="Id">Id of a meal to be deleted.</param>
        /// <returns>Status of a transaction.</returns>
        public int Delete(int Id)
        {;
            DataProvider provider = new DataProvider();
            return provider.deleteMeal(Id); ;
        }
        /// <summary>
        /// Updates specifies meal from database and returns status.
        /// </summary>
        /// <param name="meal"></param>
        /// <returns></returns>
        public int Update([FromBody] JsonResult meal)
        {
            Meal mealObj = JsonConvert.DeserializeObject<Meal>(meal.Data.ToString());
            DataProvider provider = new DataProvider();
            return provider.updateMeal(mealObj);
        }
    }
}
