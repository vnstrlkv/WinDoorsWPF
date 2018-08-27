using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace WinDoorsWPF.Model
{
   public class Window_
    {
        public double Height { get; set; }
        public double Width { get; set; }
        public string Material { get; set; }
        

        public bool Deaf { get; set; }
        public bool OpenWindow { get; set; }
        public bool Pipe { get; set; }
        public bool PaintPipe { get; set; }
        public bool Flash { get; set; }

        public bool FullOpenWindow { get; set; }

        public bool Cutting { get; set; }



        public double Perimeter()
        {
            return (Height + Width) * 2;
        }

        public double Square()
        {
            return Height * Width;
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

    }
}
