using AccountBook.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBook.Data
{
    class service
    {
        private static List<Item> _myList;
        public static List<Item> GetData()
        {
            Debug.WriteLine("GET results.");

            // instantiate the list - _myList
            _myList = Globals.items;
            //Globals.items.Clear();

            Debug.WriteLine(_myList.ToString());
            return _myList;
        }

        public static void Write(Item item)
        {
            Debug.WriteLine("INSERT person with name " + item.detail);
        }

        public static void Delete(Item item)
        {
            Debug.WriteLine("DELETE person with name " + item.detail);
        }

    }
}
