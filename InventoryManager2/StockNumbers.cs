using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager2
{
    class StockNumbers : List<String>
    {
        // This class is made to populate the numbers for the stock dropdown menu
        public StockNumbers(int Start, int End)
        {
            for(int i = Start; i < End; i++)
            {
                Add(i.ToString());
            }
        }
    }
}
