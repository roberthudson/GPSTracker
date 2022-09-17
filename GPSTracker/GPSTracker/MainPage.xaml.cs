using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GPSTracker
{
    public partial class MainPage : ContentPage
    {
        Timer timer;
        public MainPage()
        {
            InitializeComponent();

            timer = new Timer(TickHandler, null, 0, 600000);       

        }

        private async void TickHandler(object state)
        {
            try
            {
                //get location
                var location = await LocationService.Instance.GetMyLocationOrDefaultAsync();

                //write to log
                string logMessage = string.Format("{0}: Lat:{1}, Lon:{2}, Alt:{3}", DateTime.Now.ToString(), location.Latitude, location.Longitude, location.Altitude);
                string folderName = Xamarin.Essentials.FileSystem.AppDataDirectory;
                string fileName = string.Format("Log{0}.txt", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"), location.Latitude, location.Longitude);
                
                await FileStorageService.Instance.WriteTextAsync(folderName, fileName, logMessage);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
    }
}
