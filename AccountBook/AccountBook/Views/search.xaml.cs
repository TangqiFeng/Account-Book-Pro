using AccountBook.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class search : Page
    {
        public search()
        {
            this.InitializeComponent();
        }

        // temporary list to return from method
        private static List<Item> itemList = new List<Item>();

        #region Choose search type (by year/month/location)
        //Three search conditions handler

        public string SearchType = "month";
        private void radSearchByMonth_Checked(object sender, RoutedEventArgs e)
        {
            spChooseByYear.Visibility = Visibility.Visible;
            spChooseByMonth.Visibility = Visibility.Visible;
            spChooseByLocation.Visibility = Visibility.Collapsed;
            SearchType = "month";
        }

        private void radSearchByYear_Checked(object sender, RoutedEventArgs e)
        {
            spChooseByYear.Visibility = Visibility.Visible;
            spChooseByMonth.Visibility = Visibility.Collapsed;
            spChooseByLocation.Visibility = Visibility.Collapsed;
            SearchType = "year";
        }

        private void radSearchByLocation_Checked(object sender, RoutedEventArgs e)
        {
            spChooseByYear.Visibility = Visibility.Collapsed;
            spChooseByMonth.Visibility = Visibility.Collapsed;
            spChooseByLocation.Visibility = Visibility.Visible;
            SearchType = "location";
        }

        #endregion

        private async void btnSearchSubmit_Click(object sender, RoutedEventArgs e)
        {
            //clear itemList
            itemList.Clear();

            String year = txtSearchYear.Text;
            String month = txtSearchMonth.Text;
            String location = txtSearchLocation.Text;

            //Create an HTTP client object
            var httpClient = new HttpClient();
            string temp; // to store response body read as string

            Uri requestUri = new Uri(App.URL + "/getallitem?username=" + Globals.userEmail);
            //Set uri
            if (SearchType == "month" || SearchType == "year")
            {
                if(month == "") { month = "0"; }
                requestUri = new Uri(App.URL + "/getitembydate?month=" + month + "&year=" + year + "&username=" + Globals.userEmail);
            }else if(SearchType == "location")
            {
                requestUri = new Uri(App.URL + "/getitembyloc?location=" + location + "&username=" + Globals.userEmail);
            }
            
            try
            {
                //Send the GET request
                HttpResponseMessage respon = await httpClient.GetAsync(requestUri);
                respon.EnsureSuccessStatusCode(); // throws exception if request has failed
                temp = await respon.Content.ReadAsStringAsync(); // read response to a string
                // parse temp to a List
                try
                {
                    var itemsJArray = JsonArray.Parse(temp);
                    createListOfItems(itemsJArray);
                    //tblTitle.Text = _myList.Count().ToString() + " Dog Breeds";
                }
                catch (Exception exJA)
                {
                    MessageDialog dialog = new MessageDialog(exJA.Message);
                    await dialog.ShowAsync();
                };

                //var msg = $" ." + temp;
                //await new MessageDialog(msg).ShowAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message);
            }
            
        }

        private void createListOfItems(JsonArray jsonData)
        {
            foreach (var item in jsonData)
            {
                // get the object
                var obj = item.GetObject();

                Item _item = new Item();

                // get each key value pair and sort it to the appropriate elements
                // of the class
                foreach (var key in obj.Keys)
                {
                    IJsonValue value;
                    if (!obj.TryGetValue(key, out value))
                        continue;

                    switch (key)
                    {
                        case "username": // based on generic object key
                            _item.username = value.GetString();
                            break;
                        case "detail":
                            _item.detail = value.GetString();
                            break;
                        case "operate":
                            _item.operate = value.GetString();
                            break;
                        case "value":
                            _item.value = Convert.ToDouble(value.GetString());
                            break;
                        case "currency":
                            _item.currency = value.GetString();
                            break;
                        case "location":
                            _item.location = value.GetString();
                            break;
                        case "date":
                            _item.date = value.GetString();
                            break;
                    }
                } // end foreach (var key in obj.Keys)

                itemList.Add(_item);

            } // end foreach (var item in array)
            Globals.items = itemList;
            this.Frame.Navigate(typeof(result));
            
        }

        private void btnSearchReset_Click(object sender, RoutedEventArgs e)
        {
            txtSearchYear.Text = "";
            txtSearchMonth.Text = "";
            txtSearchLocation.Text = "";
        }
    }
}
