using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpirationDateChecker.BL.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpirationDateChecker.BL.Model;

namespace ExpirationDateChecker.BL.Controller.Tests
{
    [TestClass()]
    public class ShopControllerTests
    {
        [TestMethod()]
        public void AddNewShopTest()
        {
            // Arrange - обьявление
            var shopName = "shop";

            // Act - действие
            var shopcontroller = new ShopController(shopName);
            shopcontroller.AddNewShop(shopName);

            // Assert - результат
            Assert.AreEqual(shopName, shopcontroller.CurrentShop.Name);
        }

        [TestMethod()]
        public void AddNewProductTest()
        {
            // Arrange - обьявление
            var shopName = "shop";
            string productName = Guid.NewGuid().ToString();
            DateTime expDate = DateTime.Now.AddDays(60);
            Random rnd = new Random();
            int amount = rnd.Next();
            bool contains = false;


            // Act - действие
            var shopcontroller = new ShopController(shopName);
            shopcontroller.AddNewShop(shopName);
            shopcontroller.AddNewProduct(productName, expDate, amount);
            foreach(Product P in shopcontroller.CurrentShop.Products)
            {
                 if(P.Name == productName)
                {
                    contains = true;
                }
            }
            

            // Assert - результат
            Assert.AreEqual(true, contains);
        }

        [TestMethod()]
        public void DeleteShopTest()
        {
            // Arrange - обьявление
            var shopName = Guid.NewGuid().ToString();
            bool expected = false;
            bool result = true;

            // Act - действие
            var shopcontroller = new ShopController(shopName);
            shopcontroller.DeleteShop(shopcontroller.CurrentShop);
            foreach(Shop S in shopcontroller.Shops)
            {
                if(S.Name != shopName)
                {
                    result = false;
                }
            }

            // Assert - результат
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void DeleteProductTest()
        {
            // Arrange - обьявлени
            var shopName = "shop";
            string productName = "grechka";
            DateTime expDate = DateTime.Now.AddDays(60);
            Random rnd = new Random();
            int amount = rnd.Next();
            bool contains = true;

            // Act - действие
            var shopcontroller = new ShopController(shopName);
            shopcontroller.AddNewProduct(productName, expDate, amount);
            shopcontroller.DeleteProduct(shopcontroller.CurrentShop, productName);
            foreach (Product P in shopcontroller.CurrentShop.Products)
            {
                if (P.Name != productName)
                {
                    contains = false;
                }
            }

            // Assert - результат
            Assert.AreEqual(false, contains);
        }

        [TestMethod()]
        public void SaveShopsTest()
        {
            // Arrange - обьявление
            var shopName = Guid.NewGuid().ToString();

            // Act - действие
            var shopcontroller = new ShopController(shopName);


            // Assert - результат
            Assert.AreEqual(shopName, shopcontroller.CurrentShop.Name);
        }

        [TestMethod()]
        public void SaveProductsTest()
        {
            // Arrange - обьявление
            var shopName = "shop";
            string productName = Guid.NewGuid().ToString();
            DateTime expDate = DateTime.Now.AddDays(60);
            Random rnd = new Random();
            int amount = rnd.Next();
            bool contains = false;


            // Act - действие
            var shopcontroller = new ShopController(shopName);
            shopcontroller.AddNewShop(shopName);
            shopcontroller.AddNewProduct(productName, expDate, amount);
            foreach (Product P in shopcontroller.CurrentShop.Products)
            {
                if (P.Name == productName)
                {
                    contains = true;
                }
            }


            // Assert - результат
            Assert.AreEqual(true, contains);
        }
    }
}