using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpirationDateChecker.BL.Model
{
    [Serializable]
    public class Product
    {
       
        public string Name { get; }

       
        public DateTime ExpDate { get; }

        
        public int Amount { get; }

        public virtual Shop Shop { get; set; }

        public Product(string name, DateTime expDate, int amount, Shop shop)
        {
            //TODO: Proverka.

            Name = name;
            ExpDate = expDate;
            Amount = amount;
            Shop = shop;
        }

        public Product(string name, DateTime expDate, int amount)
        {
            //TODO: Proverka.

            Name = name;
            ExpDate = expDate;
            Amount = amount;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
