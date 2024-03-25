using PanicButtonApp.Models;
using System;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PanicButtonApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmergenciesPage : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }
        private readonly User _user;
        private readonly Models.Location _location;

        public EmergenciesPage()
        {
            InitializeComponent();

            Items = new ObservableCollection<string>();
            MyListView.ItemsSource = Items;
        }

        public EmergenciesPage(User user, Models.Location location) : this()
        {
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _location = location ?? throw new ArgumentNullException(nameof(location));

            UpdateItems();
        }

        private void UpdateItems()
        {
            Items.Clear();

            if (_user != null)
            {
                Items.Add($"Name: {_user.Fullname}");
                Items.Add($"Phone: {_user.Phone}");
            }

            if (_location != null)
            {
                Items.Add($"Area: {_location.Area}");
                Items.Add($"Street: {_location.Street}");
                Items.Add($"Latitude: {_location.Latitude}");
                Items.Add($"Longitude: {_location.Longitude}");
            }
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            // Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        async void OpenLocationButton_Clicked(object sender, EventArgs e)
        {
            if (_location != null)
            {
                await Map.OpenAsync(_location.Latitude, _location.Longitude, new MapLaunchOptions
                {
                    Name = $"{_location.Street}, {_location.Area}",
                    NavigationMode = NavigationMode.None
                });
            }
            else
            {
                await DisplayAlert("Error", "No location information available.", "OK");
            }
        }
    }
}
