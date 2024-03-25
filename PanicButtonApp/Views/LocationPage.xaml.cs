using PanicButtonApp.Models;
using PanicButtonApp.ViewModels;
using PanicButtonApp.Views;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PanicButtonApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocationPage : ContentPage
    {
        private readonly LocationViewModel locationViewModel;
        public LocationPage()
        {
            InitializeComponent();
            locationViewModel = new LocationViewModel();
            BindingContext = locationViewModel;
        }
        //brings location entry form to front.
        private async void OnAddLocationClicked(object sender, EventArgs e)
        {
            var popup = new LocationFormPopup(locationViewModel);
            await PopupNavigation.Instance.PushAsync(popup);
        }

        //code for the delete option. 

        //private async void OnDeleteTapped(object sender, EventArgs e)
        //{
        //    var tappedElement = (sender as TapGestureRecognizer).CommandParameter;

        //    if (tappedElement == null)
        //    {
        //        await DisplayAlert("Error", "Tapped item is null.", "OK");
        //        return;
        //    }

        //    if (tappedElement is Location location)
        //    {
        //        bool answer = await DisplayAlert("Delete", "Are you sure you want to delete this location?", "Yes", "No");
        //        if (answer)
        //        {
        //            await locationViewModel.DeleteLocation(location);
        //        }
        //    }


        //    //if ((sender as TapGestureRecognizer).CommandParameter is Location location)
        //    //{
        //    //    //displays a confirmation dialog before deleting
        //    //    bool answer = await DisplayAlert("Delete", "Are you sure you want to delete this location?", "Yes", "No");
        //    //    if (answer)
        //    //    {
        //    //        await locationViewModel.DeleteLocation(location);
        //    //    }
        //    //}
        //}


        //refreshes page
        private void OnRefreshing(object sender, EventArgs e)
        {
            var locationViewModel = BindingContext as LocationViewModel;
            locationViewModel.LoadLocations(); // Re-fetch the data
            MyListView.EndRefresh(); // End the refreshing state
        }
        //ListView Item 
        public async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            if (e.Item is Location tappedLocation)
            {
                string locationDetails = $"Area: {tappedLocation.Area}\n" +
                                        $"Street: {tappedLocation.Street}\n" +
                                        $"Plot Number: {tappedLocation.PlotNumber}\n" +
                                        $"Latitude: {tappedLocation.Latitude}\n" +
                                        $"Longitude: {tappedLocation.Longitude}";

                bool deleteConfirmed = await DisplayAlert("Location Details", locationDetails, "Delete", "OK");

                if (deleteConfirmed)
                {
                    await locationViewModel.DeleteLocation(tappedLocation);
                }
            }

    ((ListView)sender).SelectedItem = null;
        }



    }
}