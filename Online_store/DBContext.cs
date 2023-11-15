using System;
using System.Collections.Generic;

namespace Online_store
{
    public class DBContext
    {
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Manufacturer> Manufacturers { get; set; } = new List<Manufacturer>();
        public Dictionary<int, Customer> Customers { get; set; } = new Dictionary<int, Customer>();
        public Queue<Order> Orders { get; set; } = new Queue<Order>();
        public Dictionary<int, Product> Products { get; set; } = new Dictionary<int, Product>();
    }
}

