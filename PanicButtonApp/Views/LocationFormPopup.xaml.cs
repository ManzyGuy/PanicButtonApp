using PanicButtonApp.ViewModels;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;//
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;


namespace PanicButtonApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocationFormPopup : PopupPage
    {
        public LocationViewModel _viewModel;

        private CancellationTokenSource _cancellationTokenSource;

        public LocationFormPopup(LocationViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            await _viewModel.SaveLocation();
            await PopupNavigation.Instance.PopAsync();
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await GetCurrentLocation();// gets location as soon the form is opened. 
        }
        async Task GetCurrentLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10)); // coordinates accuracy set to medium with, coordinates fetching time 10s
                _cancellationTokenSource = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, _cancellationTokenSource.Token);

                if (location != null)
                {
                    LatitudeEntry.Text = location.Latitude.ToString(); 
                    LongitudeEntry.Text = location.Longitude.ToString();
                }
            }
            catch (FeatureNotSupportedException)
            {
                // Handle not supported on device exception
                await DisplayAlert("Errror","Location is not supported on this device.","OK");
            }
            catch (FeatureNotEnabledException)
            {
                // Handle not enabled on device exception
                await DisplayAlert("Error", "Location services are not enabled on this device.", "OK");
            }
            catch (PermissionException)
            {
                // Handle permission exception
                await DisplayAlert("Error", "Location permissions are not granted. Please allow access to your location to use this feature.", "OK");
            }
            catch (Exception)
            {
                // Unable to get location
                await DisplayAlert("Error", "Unable to retrieve the location.", "OK");
            }


        }

        protected override void OnDisappearing()
        {
            if(_cancellationTokenSource!=null && _cancellationTokenSource.IsCancellationRequested)
                _cancellationTokenSource.Cancel();
            base.OnDisappearing();
        }


    }

}