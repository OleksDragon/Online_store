using System;

namespace Online_store
{
    public class Order
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public string ShippingAddress { get; set; } = string.Empty;
    }
}


