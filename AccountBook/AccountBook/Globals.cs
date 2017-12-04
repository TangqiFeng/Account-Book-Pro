using AccountBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBook
{
    //a public static class and access it from anywhere
    //Global attribute
    class Globals
    {
        public static String userEmail = ""; // Modifiable
        public static List<Item> items;
        public static void clearItem() {
            items.Clear();
        }
    }
}
