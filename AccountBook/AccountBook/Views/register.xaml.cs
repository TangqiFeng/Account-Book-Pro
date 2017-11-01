using AccountBook.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
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
    public sealed partial class register : Page
    {
        public register()
        {
            this.InitializeComponent();
        }

        private void tbToLog_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(login));
        }

        private async void btnSignup_Click(object sender, RoutedEventArgs e)
        {
            //lock the button, avoid multi requests at the same time
            this.btnSignup.IsEnabled = false;

            //Create an HTTP client object
            HttpClient httpClient = new HttpClient();

            //Get User
            User user = new User();
            user.username = txtMail.Text;
            user.password = txtPassword.Password;

            //Set uri
            Uri requestUri = new Uri("http://54.246.215.224:8080/sign-up");

            //Set content
            var parameters = new Dictionary<string, string> { { "username", user.username }, { "password", user.password} };
            var encodedContent = new FormUrlEncodedContent(parameters);

            //Send the POST request asynchronously and retrieve the response as a string.
            HttpResponseMessage httpResponse;
            string httpResponseBody = "";
            
            try
            {
                //Send the POST request
                httpResponse = await httpClient.PostAsync(requestUri, encodedContent);
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message);
            }

            string msg;
            if (httpResponseBody == "registered")
            {
                msg = $"register success.";
                await new MessageDialog(msg).ShowAsync();
                this.Frame.Navigate(typeof(login));
            }
            else if (httpResponseBody == "duplicate_user")
            {
                msg = $"email exist.";
                await new MessageDialog(msg).ShowAsync();
            }
            else
            {
                msg = $"system error.";
                await new MessageDialog(msg).ShowAsync();
            }
            //clear User
            user.username = string.Empty;
            user.password = string.Empty;

            //clear password
            txtPassword.Password = string.Empty;

            //enable the button
            this.btnSignup.IsEnabled = true;
        }

    }
}
