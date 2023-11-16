using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Online_store
{
    public class Service
    {
        private readonly DBContext dbContext;
        private int customerIdCounter = 1;
        private int orderIdCounter = 1;

        public Service(DBContext context)
        {
            dbContext = context;
        }

        // Method for searching for a product by ID
        public Product? GetProductById(int productId)
        {
            if (dbContext.Products.ContainsKey(productId))
            {
                var product = dbContext.Products[productId];
                return product;
            }
            else
            {
                return null;
            }
        }

        // Method for searching products by name
        public List<Product> SearchProductsByName(string productName)
        {
            List<Product> foundProducts = new List<Product>();

            foreach (var productEntry in dbContext.Products)
            {
                var product = productEntry.Value;

                if (product.ProductName == productName)
                {
                    foundProducts.Add(product);
                }
            }
            return foundProducts;
        }

        // Method for sorting goods by ascending price
        public List<Product> SortProductsByPrice()
        {
            return dbContext.Products.Values.OrderBy(p => p.Price).ToList();
        }

        // Method for sorting products in descending order of price
        public List<Product> SortProductsByPriceDescending()
        {
            return dbContext.Products.Values.OrderByDescending(p => p.Price).ToList();
        }

        // Method for adding a product
        public void AddNewProduct(DBContext dbContext)
        {
            Console.WriteLine("Enter information about the new product:");

            Console.Write("Product ID: ");
            int productId = int.Parse(Console.ReadLine());

            Console.Write("Product Name: ");
            string productName = Console.ReadLine();

            Console.Write("Manufacturer ID: ");
            int manufacturerId = int.Parse(Console.ReadLine());

            Console.Write("Category ID: ");
            int categoryId = int.Parse(Console.ReadLine());

            Console.Write("Price: ");
            double price = double.Parse(Console.ReadLine());

            Console.Write("Quantity: ");
            int quantity = int.Parse(Console.ReadLine());

            // Creating a new product
            Product newProduct = new Product(productId, productName, manufacturerId, categoryId, price, quantity);

            // Search for a manufacturer by ID and add it to a new product
            var manufacturer = dbContext.Manufacturers.FirstOrDefault(m => m.ManufacturerId == manufacturerId);
            if (manufacturer != null)
            {
                newProduct.ProductManufacturer = manufacturer;
            }
            else
            {
                Console.WriteLine("Manufacturer not found.");
                return;
            }

            // Search for a category by ID and add it to a new product
            if (dbContext.Categories.Any(cat => cat.CategoryId == categoryId))
            {
                newProduct.ProductCategory = dbContext.Categories.First(cat => cat.CategoryId == categoryId);
            }
            else
            {
                Console.WriteLine("Category not found.");
                return;
            }

            // Adding a product to the database (dbContext)
            dbContext.Products.Add(newProduct.ProductId, newProduct);
            Console.WriteLine("The product has been successfully added to the warehouse.");
        }

        // Method for deleting a product
        public void RemoveProduct(DBContext dbContext)
        {
            Console.Write("Enter the product ID to delete: ");
            int deleteProductId;
            try
            {
                deleteProductId = int.Parse(Console.ReadLine());

                if (dbContext.Products.ContainsKey(deleteProductId))
                {
                    dbContext.Products.Remove(deleteProductId);
                    Console.WriteLine("The product was successfully removed from the warehouse.");
                }
                else
                {
                    Console.WriteLine("Product with the specified ID was not found.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Incorrect entry of product ID.");
            }
        }

        // Method for displaying information about a product
        public void PrintProduct(Product product)
        {
            Console.WriteLine($"Product ID: {product.ProductId}");
            Console.WriteLine($"Product Name: {product.ProductName}");
            Console.WriteLine($"Price: {product.Price}");
            Console.WriteLine($"Quantity: {product.Quantity}");

            // Вывод информации о категории
            if (product.ProductCategory != null && product.ProductCategory.Name != null)
            {
                Console.WriteLine($"Category: {product.ProductCategory.Name}");
            }
            else
            {
                Console.WriteLine("Category: Unknown");
            }

            // Вывод информации о производителе
            if (product.ProductManufacturer != null)
            {
                Console.WriteLine($"Manufacturer: {product.ProductManufacturer.Name}");
            }
            else
            {
                Console.WriteLine("Manufacturer: Unknown");
            }
        }

        // Method for displaying information about all products
        public void PrintAllProducts()
        {
            foreach (var productEntry in dbContext.Products)
            {
                var product = productEntry.Value;
                Console.WriteLine($"Product ID: {product.ProductId}");
                Console.WriteLine($"Categor: {product.ProductCategory.Name}");
                Console.WriteLine($"Manufacturer: {product.ProductManufacturer.Name}");
                Console.WriteLine($"Name: {product.ProductName}");
                Console.WriteLine($"Price: {product.Price}");
                Console.WriteLine($"Quantity: {product.Quantity}");
                Console.WriteLine("------------------------------------------");
            }
        }

        // Method for completing the purchase of goods
        public void PurchaseProduct(DBContext dbContext)
        {
            int productIdForPurchase;
            Console.Write("Enter the item ID to purchase: ");
            if (int.TryParse(Console.ReadLine(), out productIdForPurchase))
            {
                // Checking the availability of goods with the specified ID
                if (dbContext.Products.ContainsKey(productIdForPurchase))
                {
                    Product productToPurchase = dbContext.Products[productIdForPurchase];

                    // Checking if the product is available
                    if (productToPurchase.Quantity > 0)
                    {
                        Console.Write($"Available quantity: {productToPurchase.Quantity}. Enter quantity to purchase: ");
                        if (int.TryParse(Console.ReadLine(), out int quantityToPurchase) && quantityToPurchase > 0 && quantityToPurchase <= productToPurchase.Quantity)
                        {
                            Customer newCustomer = new Customer();

                            Console.Write("Enter your name: ");
                            newCustomer.FirstName = Console.ReadLine();

                            Console.Write("Enter your last name: ");
                            newCustomer.LastName = Console.ReadLine();

                            Console.Write("Enter your phone number: ");
                            newCustomer.Phone = Console.ReadLine();

                            // Checking existing clients by last name and phone number
                            Customer existingCustomer = dbContext.Customers.Values.FirstOrDefault(c => c.LastName == newCustomer.LastName && c.Phone == newCustomer.Phone);

                            int customerIdForPurchase;

                            if (existingCustomer != null)
                            {
                                customerIdForPurchase = existingCustomer.CustomerId;
                                Console.WriteLine("You already exist in the system. Your ID: " + customerIdForPurchase);
                            }
                            else
                            {
                                newCustomer.CustomerId = customerIdCounter++;
                                customerIdForPurchase = newCustomer.CustomerId;

                                // Adding a new client to the database (dbContext)
                                dbContext.Customers.Add(newCustomer.CustomerId, newCustomer);
                            }

                            Order newOrder = new Order();

                            // Filling out order data
                            newOrder.OrderId = orderIdCounter++;
                            newOrder.ProductId = productIdForPurchase;
                            newOrder.CustomerId = customerIdForPurchase;

                            Console.Write("Enter delivery address: ");
                            newOrder.ShippingAddress = Console.ReadLine();

                            // Reducing the quantity of the purchased product
                            productToPurchase.Quantity -= quantityToPurchase;

                            // Adding a new order to the database (dbContext)
                            dbContext.Orders.Enqueue(newOrder);
                            Console.WriteLine("The order has been successfully completed.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid quantity or insufficient stock.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("The product is out of stock.");
                    }
                }
                else
                {
                    Console.WriteLine("Product with the specified ID was not found.");
                }
            }
            else
            {
                Console.WriteLine("Incorrect product ID.");
            }
        }

        // Method for displaying information about orders
        public void PrintOrderDetails(DBContext dbContext)
        {
            foreach (var order in dbContext.Orders)
            {
                Console.WriteLine($"Order ID: {order.OrderId}");
                Console.WriteLine($"Product ID: {order.ProductId}");
                Console.WriteLine($"Customer ID: {order.CustomerId}");
                Console.WriteLine($"Shipping Address: {order.ShippingAddress}");

                // Retrieving customer information from the database by CustomerId
                if (dbContext.Customers.TryGetValue(order.CustomerId, out var customer))
                {
                    Console.WriteLine($"Customer Name: {customer.FirstName} {customer.LastName}");
                    Console.WriteLine($"Phone: {customer.Phone}");
                }
                else
                {
                    Console.WriteLine("Buyer information not found.");
                }
                Console.WriteLine("------------------------------------------");
            }
        }

        // Method for calculating the quantity of goods by category
        public int GetTotalQuantityInCategory(string categoryName)
        {
            return dbContext.Products
                .Where(product => product.Value.ProductCategory.Name == categoryName)
                .Sum(product => product.Value.Quantity);
        }
    }
}
