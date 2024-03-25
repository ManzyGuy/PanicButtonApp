//using PanicButtonApp.Data;
//using PanicButtonApp.Models;
//using Xamarin.Essentials;
//using Xamarin.Forms;
//using System.Collections.ObjectModel;
//using PanicButtonApp.Services;
//using System.Threading.Tasks;

//namespace PanicButtonApp.ViewModels
//{
//    public class LocationViewModel
//    {
//        private readonly Database _database;

//        public ObservableCollection<Models.Location> Locations { get; set; }

//        // Current location to be added or updated
//        public Models.Location CurrentLocation { get; set; } = new Models.Location();

//        public LocationViewModel()
//        {
//            // Fetch the platform-specific database path
//            var dbPathProvider = DependencyService.Get<IDatabasePathProvider>();
//            var dbFullPath = dbPathProvider.GetDatabasePath("DbSample.db3");
//            _database = new Database(dbFullPath);

//            LoadLocations();
//        }


//        public async Task SaveLocation()
//        {
//            await _database.SaveLocationAsync(CurrentLocation);
//        }

//        public async Task DeleteLocation(Models.Location location)
//        {
//            await _database.DeleteLocationAsync(location); 
//            Locations.Remove(location);
//        }

//        public async void LoadLocations() 
//        {
//            Locations = new ObservableCollection<Models.Location>( await _database.GetLocationsAsync());
//        }

//    }
//}
using PanicButtonApp.Data;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using PanicButtonApp.Services;
using System.Threading.Tasks;

namespace PanicButtonApp.ViewModels
{
    public class LocationViewModel
    {
        private readonly Database _database;

        public ObservableCollection<Models.Location> Locations { get; set; }

        // Current location to be added or updated
        public Models.Location CurrentLocation { get; set; } = new Models.Location();

        public LocationViewModel()
        {
            // Fetch the platform-specific database path
            var dbPathProvider = DependencyService.Get<IDatabasePathProvider>();
            var dbFullPath = dbPathProvider.GetDatabasePath("DbSample.db3");
            _database = new Database(dbFullPath);

            LoadLocations();
        }

        public async Task SaveLocation()
        {
            await _database.SaveLocationAsync(CurrentLocation);
        }

        public async Task DeleteLocation(Models.Location location)
        {
            await _database.DeleteLocationAsync(location);
            Locations.Remove(location);
        }

        public async void LoadLocations()
        {
            Locations = new ObservableCollection<Models.Location>(await _database.GetLocationsAsync());
        }

    }
}


