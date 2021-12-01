using System;

namespace TemplatePattern
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductFactory productFactory = new ProductFactory();
            WrapFactory wrapFactory = new WrapFactory();

            Logger logger = new Logger();
            Action<Product> log = logger.Log;

            Func<Product> func1 = productFactory.MakePizza;
            Func<Product> func2 = productFactory.MakeToyCar;

            var box1 = wrapFactory.WrapProduct(func1,log);
            var box2 = wrapFactory.WrapProduct(func2,log);

            Console.WriteLine(box1.Product.Name);
            Console.WriteLine(box2.Product.Name);
        }
    }

    class Logger
    {
        public void Log(Product product)
        {
            Console.WriteLine($"Product{product.Name} created at{DateTime.UtcNow},Price" +
                              $" is {product.Price}");
        }
    }

    class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }
    }

    class Box
    {
        public Product Product { get; set; }
    }

    class WrapFactory
    {
        public Box WrapProduct(Func<Product> getProduct,Action<Product> logCallback)
        {
            Box box = new Box();
            Product product = getProduct.Invoke();
            if (product.Price>=50)
            {
                logCallback(product);
            }
            box.Product = product;
            return box;
        }
    }

    class ProductFactory
    {
        public Product MakePizza()
        {
            Product product = new Product();
            product.Name = "Pizza";
            product.Price = 12;
            return product;
        }
        
        public Product MakeToyCar()
        {
            Product product = new Product();
            product.Name = "Toy Car";
            product.Price = 100;
            return product;
        }
    }
}