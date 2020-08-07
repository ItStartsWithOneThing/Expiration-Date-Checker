using ExpirationDateChecker.BL.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpirationDateChecker.CMD
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowWidth = 100; // Высота консоли
            Console.WindowHeight = 30; // Ширина консоли
            Console.BackgroundColor = ConsoleColor.Gray; //Цвет фона консоли
            Console.ForegroundColor = ConsoleColor.Black; // Цвет текста консоли
            Console.Clear(); // Это для того, чтобы настройки цвета применились
            Console.Title = "Expiration Date Checker"; // Надпись в шапке окна консоли
            string shopName = null;
            do
            {
                Console.WriteLine("Введите название магазина: ");
                shopName = Console.ReadLine();
                shopName = shopName.Trim();
            }
            while (shopName.Trim().Length == 0);

            Console.Clear(); // Для красоты удаляется предыдущй текст

            var shopcontroller = new ShopController(shopName); // Определяет текущий магазин. Если такого нет, то создает новый.


            Console.WriteLine($"Текущий магазин - {shopcontroller.CurrentShop}\n");
            IsNewOrOld(shopcontroller.IsNewShop); // Пишет новый это магазин или уже существующий


            while (true) // Меню
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("Q - Добавить новый продукт в текущий магазин"); // Работает корректно +
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("W - Добавить новый магазин / Выбрать другой магазин"); // Работает корректно +
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("E - Проверить просрочку в текущем магазине"); // Работает корректно +
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("R - Просмотреть список магазинов"); // Работает корректно +
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("T - Просмотреть список продуктов в текущем магазине"); // Работает корректно +
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Y - Удалить текущий магазин из списка"); // Работает корректно +
                Console.WriteLine("U - Удалить продукт из текущего магазина"); // Работает корректно +

                var key = Console.ReadKey();
                Console.WriteLine();
                switch(key.Key)
                {
                    case ConsoleKey.Q:
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Clear(); // Для красоты удаляется предыдущй текст
                        Console.WriteLine("Введите название продукта: ");
                        var productname = Console.ReadLine();
                        DateTime expdate = ParseDateTime("конечную дату срока годности");
                        int amount = ParseInt("количество");
                        shopcontroller.AddNewProduct(productname, expdate, amount);
                        Console.Clear(); // Для красоты удаляется предыдущй текст
                        Console.WriteLine($"{shopcontroller.CurrentShop.Name} - {productname}: годен до {expdate} ({amount} шт.)\n");
                        break;
                    case ConsoleKey.W:
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Clear(); // Для красоты удаляется предыдущй текст
                        Console.WriteLine("Введите название Магазина: ");
                        var n = Console.ReadLine();
                        shopcontroller.AddNewShop(n);
                        Console.WriteLine($"Текущий магазин - {shopcontroller.CurrentShop}");
                        IsNewOrOld(shopcontroller.IsNewShop);
                        shopcontroller.SetToFalse();
                        break;
                    case ConsoleKey.E:
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Clear(); // Для красоты удаляется предыдущй текст
                        shopcontroller.ExpDateCheck(shopcontroller.CurrentShop);
                        break;
                    case ConsoleKey.R:
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Clear(); // Для красоты удаляется предыдущй текст
                        shopcontroller.CheckAllShops();
                        break;
                    case ConsoleKey.T:
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Clear(); // Для красоты удаляется предыдущй текст
                        shopcontroller.CheckAllProducts();
                        break;
                    case ConsoleKey.Y:
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Clear(); // Для красоты удаляется предыдущй текст
                        shopcontroller.DeleteShop(shopcontroller.CurrentShop);
                        Console.WriteLine("Сохранить изменения?\nY - Сохранить\tN - Отмена");
                        var deleteShopKey = Console.ReadKey();
                        switch (deleteShopKey.Key)
                        {
                            case ConsoleKey.Y:
                                shopcontroller.SaveShops();
                                break;
                            case ConsoleKey.N:
                                break;
                        }
                        Console.Clear();
                        Console.WriteLine("Нажмите W для выбора текущего магазина.");
                        break;
                    case ConsoleKey.U:
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Clear(); // Для красоты удаляется предыдущй текст
                        Console.WriteLine("Введите введите название продукта: ");
                        var i = Console.ReadLine(); // TODO : Проверка
                        shopcontroller.DeleteProduct(shopcontroller.CurrentShop, i);
                        Console.WriteLine("Сохранить изменения?\nY - Сохранить\tN - Отмена");
                        var deleteProductKey = Console.ReadKey();
                        switch (deleteProductKey.Key)
                        {
                            case ConsoleKey.Y:
                                shopcontroller.SaveProducts();
                                break;
                            case ConsoleKey.N:
                                break;
                        }
                        Console.Clear();
                        break;
                }
            }

        }

        private static DateTime ParseDateTime(string value)
        {
            DateTime expDate;
            while (true)
            {
                Console.Write($"Ввести {value} (dd.MM.yyyy): ");
                if (DateTime.TryParse(Console.ReadLine(), out expDate))
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"Неверный формат {value}");
                }
            }

            return expDate;
        }

        private static int ParseInt(string name)
        {
            while (true)
            {
                Console.Write($"Ввести {name}: ");
                if (int.TryParse(Console.ReadLine(), out int value))
                {
                    return value;
                }
                else
                {
                    Console.WriteLine($"Неверный формат поля {name}");
                }
            }
        }

        public static void IsNewOrOld(bool isnew)
        {
            if (isnew == true)
            {
                Console.WriteLine("Добавлен как новый\n");
            }
            else
            {
                Console.WriteLine("Существующий магазин\n");
            }
        }
    }
}
