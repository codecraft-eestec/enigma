﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace CodeCraft.Models.Nodes
{
    public class Meal
    {
        [JsonProperty]
        public String name { get; set; }
        [JsonProperty]
        public String description { get; set; }
        [JsonProperty]
        public String picture { get; set; }
        [JsonProperty]
        public string price { get; set; }
    }
}