using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeCraft.Models.Nodes
{
    public class Order
    {
        public string user { get; set; }
        public string date { get; set; }
        public string status { get; set; }
        public string note { get; set; }
    }
}