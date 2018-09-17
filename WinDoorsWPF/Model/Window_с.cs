using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace WinDoorsWPF.Model
{
   public class Window_с : INotifyPropertyChanged
    {
        public double Height { get; set; }
        public double Width { get; set; }
        public Material material { get; set; }
        ObservableCollection<Material> furniture = new ObservableCollection<Material>();
        public ObservableCollection<Material> Furniture
        {
            get { return furniture; }
            set { furniture = value; OnPropertyChanged("Furniture"); }
        }
        public double Type { get; set; } // 0 - глухое, 1 - открывающееся, 2 - на молнии, 3 - полностью открывающееся
                      
        public bool Cutting { get; set; }
        
        public double Perimeter()
        {
            return (Height + Width) * 2;
        }

        public double Square()
        {
            return Height * Width;
        }

        public void SetFurniture(PriceList price)
        {
            switch (this.Type)
            {
                case 0:
                    var tmpfurn=price.Materials.First(x => x.Name=="Люверс 6мм");
                    furniture.Add(tmpfurn);
                    return;
                case 1:
                    return;
                case 2:
                    return;
                case 3:
                    return;



            }

        }

        public double GetDouble(string value, double defaultValue)
        {
            double result;

            //Try parsing in the current culture
            if (!double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out result) &&
                //Then try in US english
                !double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out result) &&
                //Then in neutral language
                !double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out result))
            {
                result = defaultValue;
            }

            return result;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
