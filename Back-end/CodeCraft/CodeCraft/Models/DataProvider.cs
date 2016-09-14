using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeCraft.Models.Nodes;
using Neo4jClient;

namespace CodeCraft.Models
{
    public class DataProvider
    {
        private GraphClient client = DataLayer.getClient();
        private IEnumerable<Meal> queryMeal;
        private IEnumerable<Order> queryOrder;
        private IEnumerable<User> queryUser;

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

        public Meal getMeal(string name)
        {
            queryMeal = client.Cypher
                             .Match("(meal:Meal)")
                             .Where("meal.name='" + name + "'")
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
                name = recivedMeal.name,
                description = recivedMeal.description,
                picture = recivedMeal.picture
            };

            client.Cypher
                        .Create("(" + newMeal.name + ":Meal {newMeal})")
                        .WithParam("newMeal", newMeal)
                        .ExecuteWithoutResults();

            Meal check = getMeal(recivedMeal.name);
            if (check != null)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        public int deleteMeal(string name)
        {
            client.Cypher
                       .Match("(meal:Meal)")
                       .Where("meal.name='" + name + "'")
                       .Delete("meal")
                       .ExecuteWithoutResults();

            Meal check = getMeal(name);
            if (check != null)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }

        public int updateMeal(Meal mealObj)
        {
            client.Cypher
                            .Match("(meal:Meal)")
                            .Where("meal.name='" + mealObj.name + "'")
                            .Set("meal.name='" + mealObj.name + "'")
                            .Set("meal.description='" + mealObj.description + "'")
                            .Set("meal.picture='" + mealObj.picture + "'")
                            .ExecuteWithoutResults();

            Meal check = getMeal(mealObj.name);
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

        #region Users
        public int registerUser(User recivedUser)
        {
            if (getUser(recivedUser.username) == null)
            {
                var newUser = new
                {
                    username = recivedUser.username,
                    password = recivedUser.password,
                    name = recivedUser.name,
                    type = "client"
                };

                client.Cypher
                            .Create("(" + newUser.username + ":User {newUser})")
                            .WithParam("newUser", newUser)
                            .ExecuteWithoutResults();

                User check = getUser(recivedUser.username);
                if (check != null)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            else
                return -2;
        }

        public User getUser(string username)
        {
            queryUser = client.Cypher
                             .Match("(user:User)")
                             .Where("user.username='" + username + "'")
                           .Return(user => user.As<User>())
                          .Results;
            User oneUser = null;
            foreach (User result in queryUser)
            {
                oneUser = result;
            }

            return oneUser;
        }

        public int deleteUser(string username)
        {
            client.Cypher
                       .Match("(user:User)")
                       .Where("user.username='" + username + "'")
                       .Delete("user")
                       .ExecuteWithoutResults();

            User check = getUser(username);
            if (check != null)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }

        public User[] getAllUsers()
        {
            queryUser = client.Cypher
                             .Match("(user:User)")
                           .Return(user => user.As<User>())
                          .Results;

            User[] array = new User[queryUser.Count()];
            int i = 0;
            foreach (User result in queryUser)
            {
                array[i] = result;
                i++;
            }
            return array;
        }

        public int updateUser(User userObj)
        {
            client.Cypher
                            .Match("(user:User)")
                            .Where("user.username='" + userObj.username + "'")
                            .Set("user.username='" + userObj.username + "'")
                            .Set("user.password='" + userObj.password + "'")
                            .Set("user.name='" + userObj.name + "'")
                            .Set("user.type='" + userObj.type + "'")
                            .ExecuteWithoutResults();

            User check = getUser(userObj.username);
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
        public OrderResult getAllOrders()
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
            return null;
        }

        public Order getOrder(string user, string date)
        {
            queryOrder = client.Cypher
                             .Match("(order:Order)")
                             .Where("order.user='" + user + "'")
                             .AndWhere("order.date='" + date + "'")
                           .Return(order => order.As<Order>())
                          .Results;
            Order oneOrder = null;
            foreach (Order result in queryOrder)
            {
                oneOrder = result;
            }

            return oneOrder;
        }

        public void createOrder(OrderResult recivedOrder)
        {
            var newOrder = new
            {
                user = recivedOrder.order.user,
                date = recivedOrder.order.date,
                status = recivedOrder.order.status,
                note = recivedOrder.order.note
            };

            client.Cypher
           .Match("(user:User)")
           .Where("user.username='" + recivedOrder.order.user + "'")
           .Create("user-[:MAKES]->(order:Order {newOrder})")
           .WithParam("newOrder", newOrder)
           .ExecuteWithoutResults();

            foreach (KeyValuePair<string, int> item in recivedOrder.allMeals)
            {
                client.Cypher
           .Match("(order:Order)")
           .Where("order.user='" + recivedOrder.order.user + "'")
           .AndWhere("order.date='" + recivedOrder.order.date + "'")
           .Match("(meal:Meal)")
           .Where("meal.name='" + item.Key + "'")
           .Create("order-[:INCLUDES {quantity}]->meal")
           .WithParam("quantity", new { quantity = item.Value })  //property of relationship
           .ExecuteWithoutResults();
            }

        }

        public int deleteOrder(string user, string date)
        {
            client.Cypher
                       .Match("(order:Order)")
                       .Where("order.user='" + user + "'")
                       .AndWhere("order.id='" + date + "'")
                       .Delete("order")
                       .ExecuteWithoutResults();

            Order check = getOrder(user, date);
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
                            .Set("order.date='" + orderObj.date + "'")
                            .Set("order.status='" + orderObj.status + "'")
                            .Set("order.note='" + orderObj.note + "'")
                            .ExecuteWithoutResults();

            Order check = getOrder(orderObj.user, orderObj.date);
            if (check != null)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        public string DateTimeToMilliseconds(DateTime time)
        {
            DateTime startTime = new DateTime(2016, 9, 14);
            TimeSpan unixtime = startTime - time;
            String date = unixtime.TotalMilliseconds.ToString();

            return date;
        }

        public DateTime MillisecondsToDateTime(string result)
        {
            DateTime startTime = new DateTime(2016, 9, 14);
            TimeSpan ts = TimeSpan.FromMilliseconds(Convert.ToDouble(result));
            DateTime date = startTime + ts;

            return date;
        }
        #endregion

    }
}