using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinDoorsWPF.Model
{
    class Material
    {
        private int type;
        private string name;
        private string metr;
        private double price;
        public int Type
        {
            get { return type; }
            set { type = value; }
        }
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

        public Material()
        {
            type = 0; // 0 - материал, 1 - фрунитра, 2 - работа
            Name = "";
            Metr = "";
            Price = 0;
        }

    }

}
