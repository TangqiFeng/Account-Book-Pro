using AccountBook.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
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
    public sealed partial class addItem : Page
    {
        public addItem()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        #region MainPage_Loaded (get currentlocation)
        Geolocator myGeo;
        Geoposition _pos;
        String CurrentLocation = "Loading... try again later!";
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            initialiseGeoLocation();
        }

        private async void initialiseGeoLocation()
        {
            var access = await Geolocator.RequestAccessAsync();
            switch (access)
            {
                case GeolocationAccessStatus.Allowed:
                    // set some stuff up now.
                    myGeo = new Geolocator();
                    //myGeo.PositionChanged not needed just now.
                    myGeo.DesiredAccuracy = PositionAccuracy.High;
                    //Save position to currentLocation
                    GetCurrentLocation();
                    break;
                case GeolocationAccessStatus.Denied:
                case GeolocationAccessStatus.Unspecified:
                    // ask the user for something here if things go wrong.
                    string msg = $"Can't access location services";
                    await new MessageDialog(msg).ShowAsync();
                    break;
                default:
                    break;
            }
        }

        private async void GetCurrentLocation()
        {
            try
            {
                _pos = await myGeo.GetGeopositionAsync();
            }
            catch (Exception ex)
            {
                string msg = $"Problem reading location" + ex.Message;
                await new MessageDialog(msg).ShowAsync();
                return;
            }
            BasicGeoposition myLocation = new BasicGeoposition
            {
                Longitude = _pos.Coordinate.Point.Position.Longitude,
                Latitude = _pos.Coordinate.Point.Position.Latitude
            };
            Geopoint pointToReverseGeocode = new Geopoint(myLocation);
            MapLocationFinderResult result = await MapLocationFinder.FindLocationsAtAsync(pointToReverseGeocode);
            CurrentLocation = result.Locations[0].Address.Town.ToString()
                               + ", " + result.Locations[0].Address.Country.ToString();
        }

        #endregion
        
        private async void btnAddSubmit_Click(object sender, RoutedEventArgs e)
        {
            //lock the button, avoid multi requests at the same time
            this.btnAddSubmit.IsEnabled = false;

            //ckeck all fields have value
            if (string.IsNullOrWhiteSpace(txtDescribe.Text) || string.IsNullOrWhiteSpace(txtValue.Text)
                || string.IsNullOrWhiteSpace(txtLocation.Text) || string.IsNullOrWhiteSpace(calendarDatePicker.Date.ToString()))
            {
                string msg = $"Please Input Item Correctly!";
                await new MessageDialog(msg).ShowAsync();
            }
            else
            {
                //get item details
                string des = txtDescribe.Text;
                double value = Convert.ToDouble(txtValue.Text);
                string date = calendarDatePicker.Date.Value.Day.ToString() + "/" +
                              calendarDatePicker.Date.Value.Month.ToString() + "/" +
                              calendarDatePicker.Date.Value.Year.ToString();
                string loc = txtLocation.Text;
                string currency = ((ComboBoxItem)cmbChooseCurrency.SelectedItem).Tag.ToString();

                //get new item
                var addItem = new Item() { username = Globals.userEmail, detail = des, operate = opt, value = value, location = loc, date = date, currency = currency };

                //Create an HTTP client object
                HttpClient httpClient = new HttpClient();

                //Set uri
                Uri requestUri = new Uri(App.URL + "/additem");

                // create dynamic object that will store values for json
                dynamic dynamicJson = new ExpandoObject();
                // create dynamic object that representing json; add todo task data to it 
                dynamicJson.username = addItem.username;
                dynamicJson.detail = addItem.detail;
                dynamicJson.operate = addItem.operate;
                dynamicJson.value = Convert.ToString(addItem.value);
                dynamicJson.currency = addItem.currency;
                dynamicJson.location = addItem.location;
                dynamicJson.date = addItem.date;

                // Store serialized object in string to include it as body of a request
                string json = "";
                json = JsonConvert.SerializeObject(dynamicJson); // serialize json object and store it in string 
                var objClient = new HttpClient(); // create Http Client to access http methods

                string responJsonText = "";
                // make a request
                try
                {
                    // send a post request to service and wait for respons; store response in HttpResponseMessage object
                    HttpResponseMessage respon = await objClient.PostAsync(requestUri,
                                 new StringContent(json, Encoding.UTF8, "application/json"));
                    responJsonText = await respon.Content.ReadAsStringAsync(); // read respons from service as string
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message);
                }

                string msg;
                if (responJsonText == "success")
                {
                    msg = $"add success.";
                    await new MessageDialog(msg).ShowAsync();
                }
                else
                {
                    msg = $"add fail.";
                    await new MessageDialog(msg).ShowAsync();
                }

                //clear additem obj
                addItem = new Item();
            }
            
            //clear textbox
            txtDescribe.Text = "";
            txtLocation.Text = "";
            txtValue.Text = "";

            //enable the button
            this.btnAddSubmit.IsEnabled = true;

        }

        private void btnAddReset_Click(object sender, RoutedEventArgs e)
        {
            txtDescribe.Text = "";
            txtLocation.Text = "";
            txtValue.Text = "";
        }

        #region Choose income / expenditure

        string opt;
        private void radPlus_Checked(object sender, RoutedEventArgs e)
        {
            opt = radPlus.Tag.ToString();
        }

        private void radMinus_Checked(object sender, RoutedEventArgs e)
        {
            opt = radMinus.Tag.ToString();
        }

        #endregion

        private void btnGetLocation_Click(object sender, RoutedEventArgs e)
        {
            txtLocation.Text = CurrentLocation;
        }
    }
}
