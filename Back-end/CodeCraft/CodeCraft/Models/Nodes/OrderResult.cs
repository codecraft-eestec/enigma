using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeCraft.Models.Nodes
{
    public class OrderResult
    {
        public Order order { get; set; }
        public List<KeyValuePair<string, int>> allMeals = new List<KeyValuePair<string, int>>();
    }
}