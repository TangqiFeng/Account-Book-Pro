using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBook.Models
{
    class User
    {
        public string username { get; set; }
        public string password { get; set; }

        public User() { }

        public User(string usetname, string password)
        {
            this.username = username;
            this.password = password;
        }
    }
}
