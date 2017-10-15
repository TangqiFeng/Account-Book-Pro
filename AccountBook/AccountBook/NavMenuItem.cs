using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace AccountBook
{
    class NavMenuItem : INotifyPropertyChanged
    {
        // two-way bind, determin manu(rectangle) display or not 
        public event PropertyChangedEventHandler PropertyChanged;
        // record icon and font
        public FontFamily FontFamily { get; set; }
        // icon code
        public string Icon { get; set; }
        // item title
        public string Label { get; set; }
        // navigation page
        public Type DestPage { get; set; }
        // display manu
        private Visibility selected = Visibility.Collapsed;
        public Visibility Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                this.OnPropertyChanged("Selected");
            }
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
