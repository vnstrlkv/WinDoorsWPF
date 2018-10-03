using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WinDoorsWPF.Model 
{
   public class Material : INotifyPropertyChanged
    {
        private string type;
        private string name;
        private string metr;
        private double price;
        public string Type
        {
           get { return type; }
            set { type = value; OnPropertyChanged("Type"); }
        }
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }

        public string Metr
        {
            get { return metr; }
            set { metr = value; OnPropertyChanged("Metr"); }
        }

        public double Price
        {
            get { return price; }
            set { price = value; OnPropertyChanged("Price"); }
        }

        public Material()
        {
            type = ""; // 0 - материал, 1 - фрунитра, 2 - работа
            Name = "";
            Metr = "";
            Price = 0;
        }

        public Material (Material mat)
        {
            type = mat.Type;
            Name = mat.Name;
            Metr = mat.Metr;
            Price = mat.Price;

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }

    public class MaterialClient : Material
    {

    }

}
