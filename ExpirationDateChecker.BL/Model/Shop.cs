using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExpirationDateChecker.BL.Model
{
    [Serializable]
    public class Shop
    {
       
        public string Name { get; set; }


        public virtual List<Product> Products { get; set; }

        public Shop(string name)
        {
            Name = name;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
