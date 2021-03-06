﻿using AccountBook.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBook.Data
{
    class service
    {
        private static List<Item> _myList;
        public static List<Item> GetData()
        {
            Debug.WriteLine("GET results.");

            // instantiate the list - _myList
            _myList = Globals.items;
            //Globals.items.Clear();

            Debug.WriteLine(_myList.ToString());
            return _myList;
        }

        public static void Write(Item item)
        {
            Debug.WriteLine("INSERT person with name " + item.detail);
        }

        public static async void Delete(Item item)
        {
            //Create an HTTP client object
            Windows.Web.Http.HttpClient httpClient = new Windows.Web.Http.HttpClient();

            //Get username and detail
            string username = item.username;
            string detail = item.detail;

            //Set uri
            Uri requestUri = new Uri(App.URL + "/deleteitem?username=" + username + "&detail=" + detail);

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
            if (httpResponseBody == "deleted")
            {
               
            }
            else
            {
               
            }
        }

    }
}
