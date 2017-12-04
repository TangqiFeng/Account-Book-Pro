using AccountBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBook.ViewModels
{
    class ItemViewModel : NotificationBase<Item>
    {
        public ItemViewModel(Item item = null) : base(item) { }

        public string username {
        get { return This.username; }
            set { SetProperty(This.username, value, () => This.username = value); }}

        public string detail
        {
            get { return This.detail; }
            set { SetProperty(This.detail, value, () => This.detail = value); }
        }

        public string operate
        {
            get { return This.operate; }
            set { SetProperty(This.operate, value, () => This.operate = value); }
        }
        public double value
        {
            get { return This.value; }
            set { SetProperty(This.value, value, () => This.value = value); }
        }

        public string currency
        {
            get { return This.currency; }
            set { SetProperty(This.currency, value, () => This.currency = value); }
        }

        public string location
        {
            get { return This.location; }
            set { SetProperty(This.location, value, () => This.location = value); }
        }
        public string date
        {
            get { return This.date; }
            set { SetProperty(This.date, value, () => This.date = value); }
        }
    }
}
