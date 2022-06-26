using OjayFood.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OjayFood.Models.Home
{
    public class Item
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}