using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeCraft.Models.Nodes
{
    public class OrderResult
    {
        public Order order { get; set; }
        public List<KeyValuePair<string, string>> allOrders = new List<KeyValuePair<string, string>>();
    }
}