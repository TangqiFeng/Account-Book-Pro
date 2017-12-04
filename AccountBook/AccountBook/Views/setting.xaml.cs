﻿using System;
using System.Collections.Generic;
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
    public sealed partial class setting : Page
    {
        public setting()
        {
            this.InitializeComponent();
        }

        private async void btnChangePsd_Click(object sender, RoutedEventArgs e)
        {
            var msg = $"Sorry, this function is not avaliable now.";
            await new MessageDialog(msg).ShowAsync();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Globals.userEmail = "";
            //this.Frame.Navigate(typeof(login));
            App.Current.Exit();
        }
    }
}
