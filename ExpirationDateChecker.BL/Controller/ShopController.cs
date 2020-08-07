using ExpirationDateChecker.BL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ExpirationDateChecker.BL.Controller
{
    public class ShopController
    {
        public List<Shop> Shops { get; }

        public Shop CurrentShop { get; set; }

        public bool IsNewShop { get; set; } = false;



        public ShopController() { }
        public ShopController(string shopName) // Определяет текущий магазин. Если такого нет, то создает новый.
        {

            Shops = GetShopsData();

            CurrentShop = Shops.SingleOrDefault(s => s.Name == shopName);

            if(CurrentShop == null)
            {
                CurrentShop = new Shop(shopName);
                Shops.Add(CurrentShop);
                IsNewShop = true;
                SaveShops();
            }

            else
            {
                CurrentShop.Products = GetAllProducts();
            }
        }

        private List<Shop> GetShopsData() // Подгружает список всех магазов из файла. Если такого нет, то создает новый
        {
            var formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream("Shops.dat", FileMode.OpenOrCreate))
            {
                if(fs.Length > 0 && formatter.Deserialize(fs) is List<Shop> shops)
                {
                    return shops;
                }
                else
                {
                    return new List<Shop>();
                }
            }
        }

        private List<Product> GetAllProducts()
        {
            var formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream($"Products - {CurrentShop.Name}.dat", FileMode.OpenOrCreate))
            {
                if (fs.Length > 0 && formatter.Deserialize(fs) is List<Product> products)
                {
                    return products;
                }
                else
                {
                    return new List<Product>();
                }
            }
        }

        public void AddNewShop(string name) // Добавляю новый магазин. Либо, если такой уже есть, то делаю его текущим.
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Название магазина не заполнено. Вернитесь в меню и повторите попытку.\n");
            }
            else
            {
                CurrentShop = Shops.SingleOrDefault(s => s.Name == name);
                if (CurrentShop == null)
                {
                    CurrentShop = new Shop(name);
                    Shops.Add(CurrentShop);
                    IsNewShop = true;
                    SaveShops();
                }
                else
                {
                    CurrentShop.Products = GetAllProducts();
                }
            }
        }

        public void AddNewProduct(string productName, DateTime expDate, int amount)
        {
            if (String.IsNullOrWhiteSpace(productName))
            {
                Console.WriteLine("Название продукта не заполнено. Вернитесь в меню и повторите попытку.\n");
            }
            else
            {
                CurrentShop.Products = GetAllProducts();
                var P = CurrentShop.Products.SingleOrDefault(p => p.Name == productName);
                if (P == null)
                {
                    var NewProduct = new Product(productName, expDate, amount);
                    CurrentShop.Products.Add(NewProduct);
                    SaveProducts();
                }

                else
                {
                    Console.WriteLine("Такой продукт уже существует, введите другой");
                }
            }

        }

        public void CheckAllShops()
        {
            var ShopsList = GetShopsData();
            foreach(var shop in ShopsList)
            {
                Console.WriteLine(shop);
            }

        }

        public void CheckAllProducts()
        {
            var products = GetAllProducts();
            foreach (var p in products)
            {
                var i = products.IndexOf(p);
                Console.Write($"{i}. ");
                if (p.ExpDate.AddMonths(-2) < DateTime.Today)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"{p.Name}: годен до {p.ExpDate}, ({p.Amount} шт)\n");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"{p.Name}: годен до {p.ExpDate}, ({p.Amount} шт)\n");
                }
            }
            Console.WriteLine("\n");
        }

        public void SetToFalse() // Обнуляю это св-во, чтобы узнать новый ли магазин.
        {
            IsNewShop = false; 
        }

        public void ExpDateCheck(Shop currentShop)
        {
            var Checked = currentShop.Products.Where(x => (x.ExpDate.AddMonths(-2) < DateTime.Today));
            if (Checked == null)
            {
                Console.WriteLine("Можно спать спокойно ближайшие 2 месяца. :)\n");
            }
            else
            {
                foreach (var c in Checked)
                {
                    Console.WriteLine($"{c.Name}: годен до {c.ExpDate} ({c.Amount} шт.)\n");
                }
            }
        }

        public void DeleteShop(Shop currentshop)
        {
            Shops.Remove(currentshop);
        }

        public void DeleteProduct(Shop currshop, string i)
        {
            if (String.IsNullOrWhiteSpace(i))
            {
                Console.WriteLine("Название продукта не заполнено. Вернитесь в меню и повторите попытку.\n");
            }
            else
            {
                Product currprod = currshop.Products.SingleOrDefault(x => x.Name == i);
                if (currprod == null)
                {
                    Console.WriteLine("\nТакого продукта не существует. Вернитесь в меню и повторите попытку.\n");
                }
                else
                {
                    currshop.Products.Remove(currprod);
                }
            }
        }

        public void SaveShops()
        {
            var formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream("Shops.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, Shops);  
            }       
        }

        public void SaveProducts()
        {
            var formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream($"Products - {CurrentShop.Name}.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, CurrentShop.Products);
            }
        }
    }
}
