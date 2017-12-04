using AccountBook.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using AccountBook.Views;

namespace AccountBook.ViewModels
{
    class OrganizationViewModel : NotificationBase
    {
        Organization organization;

        ObservableCollection<ItemViewModel> _item
           = new ObservableCollection<ItemViewModel>();

        int _SelectedIndex;

        public OrganizationViewModel()
        {
            organization = new Organization();
            _SelectedIndex = -1;
            // Load the database
            foreach (var item in organization.items)
            {
                var np = new ItemViewModel(item);
                np.PropertyChanged += items_OnNotifyPropertyChanged;
                _item.Add(np);
            }
        }

        public ObservableCollection<ItemViewModel> Item
        {
            get { return _item; }
            set { SetProperty(ref _item, value); }
        }

        public int SelectedIndex
        {
            get { return _SelectedIndex; }
            set
            {
                if (SetProperty(ref _SelectedIndex, value))
                { RaisePropertyChanged(nameof(SelectedItem)); }
            }
        }

        public ItemViewModel SelectedItem
        {
            get { return (_SelectedIndex >= 0) ? _item[_SelectedIndex] : null; }
        }
        
        public void Add()
        {
            var item = new ItemViewModel();
            item.PropertyChanged += items_OnNotifyPropertyChanged;
            Item.Add(item);
            organization.Add(item);
            SelectedIndex = Item.IndexOf(item);
        }

        public void Delete()
        {
            if (SelectedIndex != -1)
            {
                var item = Item[SelectedIndex];
                Item.RemoveAt(SelectedIndex);
                organization.Delete(item);
            }
        }

        private void items_OnNotifyPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            organization.Update((ItemViewModel)sender);
        }
    }
}
