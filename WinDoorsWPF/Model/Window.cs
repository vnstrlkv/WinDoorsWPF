using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinDoorsWPF.Model
{
   public class Window
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
    }
}
