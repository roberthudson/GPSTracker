using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace GPSTracker
{
    public class LocationService
    {
        private static readonly Lazy<LocationService> lazy = new Lazy<LocationService>(() => new LocationService());
        public static LocationService Instance { get { return lazy.Value; } }

        public async Task<Location> GetMyLocationOrDefaultAsync()
        {
            Location location = null;
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, new TimeSpan(0, 0, 3));
                location = await Geolocation.GetLocationAsync(request);

                if (location == null)
                {
                    location = await Geolocation.GetLastKnownLocationAsync();
                }
            }
            catch
            {
            }
            if (location == null)
            {
                location = GetDefaultLocation();
            }
            return location;
        }

        public Location GetDefaultLocation()
        {
            return new Location(0, 0);
        }

        public async Task<bool> CanGetLocationAsync()
        {
            try
            {
                var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                return status == PermissionStatus.Granted;
            }
            catch (Exception ex)
            {                
                return false;
            }
        }
    }
}