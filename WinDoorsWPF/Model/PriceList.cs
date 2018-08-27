using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinDoorsWPF.Model
{
    class PriceList
    {
      public  List<Material> Materials { get; set; }


        public PriceList()
        {
            Materials = new List<Material>();
        }
                     
        
    }
}
