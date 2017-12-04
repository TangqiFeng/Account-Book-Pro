using AccountBook.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBook.Models
{
    class Organization
    {
        public List<Item> items { get; set; }

        public Organization()
        {
            items = service.GetData();
        }

        public void Add(Item item)
        {
            if (!items.Contains(item))
            {
                items.Add(item);
                service.Write(item);
            }
        }

        public void Delete(Item item)
        {
            if (items.Contains(item))
            {
                items.Remove(item);
                service.Delete(item);
            }
        }

        public void Update(Item item)
        {
            service.Write(item);
        }
    }
}
