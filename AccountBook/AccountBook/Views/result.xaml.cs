using AccountBook.Models;
using AccountBook.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AccountBook.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class result : Page
    {
        public result()
        {
            this.InitializeComponent();
            Organization = new OrganizationViewModel();
        }

        OrganizationViewModel Organization { get; set; }

        private void txtCalculate_Click(object sender, RoutedEventArgs e)
        {
            List<ItemViewModel> items = Organization.Item.ToList<ItemViewModel>();
            int length = items.Count;
            double sumEURO = 0;
            double sumUSD = 0;
            double sumGBP = 0;
            double sumRMB = 0;


            for (int i = 0; i < length; i++)
            {
                string curr = items[i].currency;
                double value = items[i].value;
                string operate = items[i].operate;
                if (curr.Equals("EUR(€)"))
                {
                    if (operate.Equals("+"))
                    {
                        sumEURO += value;
                    }
                    if (operate.Equals("-"))
                    {
                        sumEURO -= value;
                    }
                }
                if (curr.Equals("USD($)"))
                {
                    if (operate.Equals("+"))
                    {
                        sumUSD += value;
                    }
                    if (operate.Equals("-"))
                    {
                        sumUSD -= value;
                    }
                }
                if (curr.Equals("GBP(￡)"))
                {
                    if (operate.Equals("+"))
                    {
                        sumGBP += value;
                    }
                    if (operate.Equals("-"))
                    {
                        sumGBP -= value;
                    }
                }
                if (curr.Equals("RMB(¥)"))
                {
                    if (operate.Equals("+"))
                    {
                        sumRMB += value;
                    }
                    if (operate.Equals("-"))
                    {
                        sumRMB -= value;
                    }
                }
            }


            resultEURO.Text = sumEURO + "";
            resultGBP.Text = sumGBP + "";
            resultRMB.Text = sumRMB + "";
            resultUSD.Text = sumUSD + "";

        }

        // Handles the Click event on the Button inside the Popup control and 
        // closes the Popup. 
        private void ClosePopupClicked(object sender, RoutedEventArgs e)
        {
            // if the Popup is open, then close it 
            if (StandardPopup.IsOpen) { StandardPopup.IsOpen = false; }
        }

        // Handles the Click event on the Button on the page and opens the Popup. 
        private void ShowPopupOffsetClicked(object sender, RoutedEventArgs e)
        {
            // open the Popup if it isn't open already 
            if (!StandardPopup.IsOpen) { StandardPopup.IsOpen = true; }
        }
    }
}
