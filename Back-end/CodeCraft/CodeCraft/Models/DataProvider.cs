using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Neo4jClient;
using CodeCraft.Models.Nodes;

namespace CodeCraft.Models
{
    public class DataProvider
    {
        private GraphClient client = DataLayer.getClient();
        private IEnumerable<Meal> queryMeal;
        private IEnumerable<Order> queryOrder;
        #region Meal
        public Meal[] getAllMeals()
        {
            queryMeal = client.Cypher
                             .Match("(meal:Meal)")
                           .Return(meal => meal.As<Meal>())
                          .Results;
            Meal[] array = new Meal[queryMeal.Count()];
            int i = 0;
            foreach (Meal result in queryMeal)
            {
                array[i] = result;
                i++;
            }
            return array;
        }

        public Meal getMeal(int id)
        {
            queryMeal = client.Cypher
                             .Match("(meal:Meal)")
                             .Where("meal.id='" + id + "'")
                           .Return(meal => meal.As<Meal>())
                          .Results;
            Meal oneMeal = null;
            foreach (Meal result in queryMeal)
            {
                oneMeal = result;
            }

            return oneMeal;
        }

        public int createMeal(Meal recivedMeal)
        {
            var newMeal = new
            {
                id = recivedMeal.id,
                name = recivedMeal.name,
                description = recivedMeal.description,
                picture = recivedMeal.picture
            };

            client.Cypher
                        .Create("(" + newMeal.name + ":Meal {newMeal})")
                        .WithParam("newMeal", newMeal)
                        .ExecuteWithoutResults();

            Meal check = getMeal(recivedMeal.id);
            if (check != null)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        public int deleteMeal(int id)
        {
            client.Cypher
                       .Match("(meal:Meal)")
                       .Where("meal.id='" + id + "'")
                       .Delete("meal")
                       .ExecuteWithoutResults();

            Meal check = getMeal(id);
            if (check != null)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        public int updateMeal(Meal mealObj)
        {
            client.Cypher
                            .Match("(meal:Meal)")
                            .Where("meal.name='" + mealObj.name + "'")
                            .Set("meal.id='" + mealObj.id + "'")
                            .Set("meal.name='" + mealObj.name + "'")
                            .Set("meal.description='" + mealObj.description + "'")
                            .Set("meal.picture='" + mealObj.picture + "'")
                            .ExecuteWithoutResults();

            Meal check = getMeal(mealObj.id);
            if (check != null)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
        #endregion


        #region Order
        public Order[] getAllOrder()
        {
            queryOrder = client.Cypher
                             .Match("(order:Order)")
                           .Return(order => order.As<Order>())
                          .Results;
            Order[] array = new Order[queryOrder.Count()];
            int i = 0;
            foreach (Order result in queryOrder)
            {
                array[i] = result;
                i++;
            }
            return array;
        }

        public Order getOrder(int id)
        {
            queryOrder = client.Cypher
                             .Match("(order:Order)")
                             .Where("order.id='" + id + "'")
                           .Return(order => order.As<Order>())
                          .Results;
            Order oneOrder = null;
            foreach (Order result in queryOrder)
            {
                oneOrder = result;
            }

            return oneOrder;
        }

        public int createOrder(Order recivedOrder)
        {
            var newOrder = new
            {
                id = recivedOrder.id,
                user = recivedOrder.user,
                date = recivedOrder.date,
                status = recivedOrder.status
            };

            client.Cypher
                        .Create("(" + newOrder.user + ":Order {newOrder})")
                        .WithParam("newOrder", newOrder)
                        .ExecuteWithoutResults();

            Order check = getOrder(recivedOrder.id);
            if (check != null)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        public int deleteOrder(int id)
        {
            client.Cypher
                       .Match("(order:Order)")
                       .Where("order.id='" + id + "'")
                       .Delete("order")
                       .ExecuteWithoutResults();

            Order check = getOrder(id);
            if (check != null)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        public int updateOrder(Order orderObj)
        {
            client.Cypher
                            .Match("(order:Order)")
                            .Where("order.user='" + orderObj.user + "'")
                            .Set("order.user='" + orderObj.user + "'")
                            .Set("order.id='" + orderObj.id + "'")
                            .Set("order.date='" + orderObj.date + "'")
                            .Set("order.status='" + orderObj.status + "'")
                            .ExecuteWithoutResults();

            Order check = getOrder(orderObj.id);
            if (check != null)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
        #endregion
    }
}