using System;

namespace Online_store
{
    public class Product
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public int ManufacturerId { get; set; }
        public string? ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity {  get; set; }

        public Category ProductCategory { get; set; }
        public Manufacturer ProductManufacturer { get; set; }

        public Product(int productId, string? productName, int manufacturerId, int categoryId, double price, int quantity)
        {
            ProductId = productId;
            ProductName = productName;
            ManufacturerId = manufacturerId;
            CategoryId = categoryId;
            Price = price;
            Quantity = quantity;
        }
    }
}

