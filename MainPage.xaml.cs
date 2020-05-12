﻿using System;
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
using Windows.Web.Http;
using Windows.Storage;
using Windows.ApplicationModel;
using System.Diagnostics;
using Windows.Data.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CanvasForDesktop
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            getClasses();
        }

        private async void getClasses()
        {

            StatusText.Text = "Connecting To Server...";

            HttpClient httpClient = new HttpClient();
            var headers = httpClient.DefaultRequestHeaders;
            StorageFolder storageFolder = Package.Current.InstalledLocation;
            StorageFile tokenFile = await storageFolder.GetFileAsync("token.txt");
            string token = await FileIO.ReadTextAsync(tokenFile);

            Uri requestUri = new Uri("https://canvas.instructure.com/api/v1/users/self/favorites/courses?access_token=" + token);
            HttpResponseMessage httpResponse = new HttpResponseMessage();
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
                StatusText.Text = "Failed";
                Debug.WriteLine(ex.Message);
                return;
            }

            StatusText.Text = "Fetched Classes Successfully";
            var courseArray = JsonArray.Parse(httpResponseBody);
            string[] courseIDs = new string[courseArray.Count];
            for(uint i=0; i<courseArray.Count; i++)
            {
                courseIDs[i] = courseArray.GetObjectAt(i)["id"].ToString();
            }

            StatusText.Text = "Parsed IDs Successfully";

        }
    }
}