using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBook.Models
{
    class Item
    {
        public string username { get; set; }
        
        public string detail { get; set; }
        
        public string operate { get; set; }
        
        public double value { get; set; }
        
        public string currency { get; set; }
        
        public string location { get; set; }
        
        public string date { get; set; }

        public Item() { }

        public Item(string user, string detail, string operate, double value, string currency, string location, string date) {
            this.username = user;
            this.detail = detail;
            this.operate = operate;
            this.value = value;
            this.currency = currency;
            this.location = location;
            this.date = date;
        }
    }
}
