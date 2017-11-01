using AccountBook.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class login : Page
    {
        public login()
        {
            this.InitializeComponent();
        }

        private void tbToReg_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(register));
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //lock the button, avoid multi requests at the same time
            this.btnLogin.IsEnabled = false;

            //Create an HTTP client object
            Windows.Web.Http.HttpClient httpClient = new Windows.Web.Http.HttpClient();

            //Get User
            User user = new User();
            user.username = txtUsername.Text;
            user.password = txtPassword.Password;

            //Set uri
            Uri requestUri = new Uri("http://54.246.215.224:8080/login?username="+user.username+"&password="+user.password);
            
            //Send the GET request asynchronously and retrieve the response as a string.
            Windows.Web.Http.HttpResponseMessage httpResponse = new Windows.Web.Http.HttpResponseMessage();
            string httpResponseBody = "";

            try
            {
                //Send the GET request
                httpResponse = await httpClient.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message);
            }

            string msg;
            if (httpResponseBody == "logged")
            {
                msg = $"login success.";
                await new MessageDialog(msg).ShowAsync();
                this.Frame.Navigate(typeof(MainPage));
            }
            else
            {
                msg = $"username/password is wrong.";
                await new MessageDialog(msg).ShowAsync();
            }
            //clear User
            user.username = string.Empty;
            user.password = string.Empty;

            //clear password
            txtPassword.Password = string.Empty;
            
            //enable the button
            this.btnLogin.IsEnabled = true;
        }
    }
}
