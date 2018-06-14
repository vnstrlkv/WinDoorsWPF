using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinDoorsWPF.Model
{
    class Material
    {
        private string name;
        private string metr;
        private double price;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Metr
        {
            get { return metr; }
            set { metr = value; }
        }

        public double Price
        {
            get { return price; }
            set { price = value; }
        }
        
    }
}
