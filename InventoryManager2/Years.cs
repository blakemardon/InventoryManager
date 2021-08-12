using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager2
{
    class Years : List<String>
    {
        //This class is made to populate the years in the year input dropdown menus
        public Years()
        {
            for(int i = 0; i < 100; i++)
            {
                Add((DateTime.Now.Year - i + 1).ToString());
            }
        }
    }
}
