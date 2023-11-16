using System.Diagnostics;

namespace Online_store
{
    public class Program
    {
        static void Main(string[] args)
        {
            DBContext? dbContext = new DBContext();
            Service? service = new Service(dbContext);

            // Creating categories
            Category category1 = new Category(1, "Телефоны");
            Category category2 = new Category(2, "Планшеты");
            Category category3 = new Category(3, "Ноутбуки");

            // Creation of producers
            Manufacturer manufacturer1 = new Manufacturer(1, "Apple");
            Manufacturer manufacturer2 = new Manufacturer(2, "Samsung");
            Manufacturer manufacturer3 = new Manufacturer(3, "Xiaomi");
            Manufacturer manufacturer4 = new Manufacturer(4, "ASUS");

            // Product creation
            Product product1 = new Product(1, "iPhone 14 Pro Max", 1, 1, 53499.00, 10);
            Product product2 = new Product(2, "Galaxy Tab S9", 2, 2, 69999.00, 10);
            Product product3 = new Product(3, "13T Pro", 3, 1, 35999.00, 10);
            Product product4 = new Product(4, "ROG Zephyrus Duo 16", 4, 3, 165999.00, 10);
            Product product5 = new Product(5, "Galaxy Fold5", 2, 1, 86999.00, 10);
            Product product6 = new Product(6, "iPad Pro 11", 1, 2, 102999.00, 10);
            Product product7 = new Product(7, "MacBook Pro 16", 1, 3, 228999.00, 10);
            Product product8 = new Product(8, "Zenbook Pro 14 Duo", 4, 3, 99999.50, 10);
            Product product9 = new Product(9, "Mi RedmiBook 15", 3, 3, 21999.00, 10);
            Product product10 = new Product(10, "Galaxy Book3 Ultra", 2, 3, 139999.00, 10);

            // Linking categories and manufacturers to a product
            product1.ProductCategory = category1;
            product1.ProductManufacturer = manufacturer1;
            product2.ProductCategory = category2;
            product2.ProductManufacturer = manufacturer2;
            product3.ProductCategory = category3;
            product3.ProductManufacturer = manufacturer3;
            product4.ProductCategory = category1;
            product4.ProductManufacturer = manufacturer4;
            product5.ProductCategory = category2;
            product5.ProductManufacturer = manufacturer1;
            product6.ProductCategory = category3;
            product6.ProductManufacturer = manufacturer2;
            product7.ProductCategory = category1;
            product7.ProductManufacturer = manufacturer3;
            product8.ProductCategory = category2;
            product8.ProductManufacturer = manufacturer4;
            product9.ProductCategory = category3;
            product9.ProductManufacturer = manufacturer1;
            product10.ProductCategory = category1;
            product10.ProductManufacturer = manufacturer2;

            // Add to collections
            dbContext.Categories.Add(category1);
            dbContext.Categories.Add(category2);
            dbContext.Categories.Add(category3);

            dbContext.Manufacturers.Add(manufacturer1);
            dbContext.Manufacturers.Add(manufacturer2);
            dbContext.Manufacturers.Add(manufacturer3);
            dbContext.Manufacturers.Add(manufacturer4);

            dbContext.Products.Add(product1.ProductId, product1);
            dbContext.Products.Add(product2.ProductId, product2);
            dbContext.Products.Add(product3.ProductId, product3);
            dbContext.Products.Add(product4.ProductId, product4);
            dbContext.Products.Add(product5.ProductId, product5);
            dbContext.Products.Add(product6.ProductId, product6);
            dbContext.Products.Add(product7.ProductId, product7);
            dbContext.Products.Add(product8.ProductId, product8);
            dbContext.Products.Add(product9.ProductId, product9);
            dbContext.Products.Add(product10.ProductId, product10);

            while (true)
            {                
                Console.WriteLine("1 - Product search by ID");
                Console.WriteLine("2 - Search for a product by name");
                Console.WriteLine("3 - Sorting products by ascending price");
                Console.WriteLine("4 - Sorting products by descending price");
                Console.WriteLine("5 - Add product to warehouse");
                Console.WriteLine("6 - Remove product from warehouse");
                Console.WriteLine("7 - Display of all products");
                Console.WriteLine("8 - Purchase of goods");
                Console.WriteLine("9 - Displaying information about orders");
                Console.WriteLine("10 - Analysis of store functionality");
                Console.WriteLine("0 - Exit the program");

                Console.Write("Choose an action: ");
                string? choice = Console.ReadLine();

                Console.Clear();

                switch (choice)
                {
                    case "1": // Product search by ID
                        Console.Write("Enter product ID to search: ");
                        if (int.TryParse(Console.ReadLine(), out int productId))
                        {
                            Product? foundProduct = service.GetProductById(productId);
                            if (foundProduct != null)
                            {
                                Console.WriteLine("Found product:");
                                Console.WriteLine("------------------------------------------");
                                service.PrintProduct(foundProduct);
                            }
                            else
                            {
                                Console.WriteLine("Product not found.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Incorrect entry of product ID.");
                        }
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "2": // Search for a product by name
                        Console.Write("Enter the product name to search: ");
                        string productName = Console.ReadLine();

                        List<Product> foundProductsByName = service.SearchProductsByName(productName);

                        if (foundProductsByName.Count > 0)
                        {
                            Console.WriteLine("Found products:");
                            Console.WriteLine("------------------------------------------");
                            foreach (var product in foundProductsByName)
                            {
                                service.PrintProduct(product);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No products with this name were found.");
                        }
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "3": // Sorting products by ascending price
                        var sortedProducts = service.SortProductsByPrice();
                        if (sortedProducts.Count > 0)
                        {
                            Console.WriteLine("Sorted products by ascending price:");
                            Console.WriteLine("------------------------------------------");
                            foreach (var product in sortedProducts)
                            {
                                service.PrintProduct(product);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No products available.");
                        }
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "4": // Sorting products by descending price
                        sortedProducts = service.SortProductsByPriceDescending();
                        if (sortedProducts.Count > 0)
                        {
                            Console.WriteLine("Sorted products by descending price:");
                            Console.WriteLine("------------------------------------------");
                            foreach (var product in sortedProducts)
                            {
                                service.PrintProduct(product);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No products available.");
                        }
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "5": //Add product to warehouse
                        Console.WriteLine("Adding a product");
                        Console.WriteLine("------------------------------------------");
                        service.AddNewProduct(dbContext);
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "6": //Remove product from warehouse
                        Console.WriteLine("Removing a product");
                        Console.WriteLine("------------------------------------------");
                        service.RemoveProduct(dbContext);
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "7": // Display of all products
                        service.PrintAllProducts();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "8": // Purchase of goods
                        Console.WriteLine("Purchase of goods");
                        Console.WriteLine("------------------------------------------");
                        service.PurchaseProduct(dbContext);
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "9": // Displaying information about orders
                        Console.WriteLine("Displaying information about orders");
                        Console.WriteLine("------------------------------------------");
                        service.PrintOrderDetails(dbContext);
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "10": // Analysis of store functionality
                        Console.WriteLine("Analysis of store functionality:");
                        Console.WriteLine("------------------------------------------");
                        Console.WriteLine($"Количество категорий товаров: {dbContext.Categories.Count}");
                        Console.WriteLine($"Количество производителей: {dbContext.Manufacturers.Count}");
                        Console.WriteLine($"Общее количество товаров на складе: {dbContext.Products.Count}");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "0":
                        return;
                        
                }
            }
        }
    }
}