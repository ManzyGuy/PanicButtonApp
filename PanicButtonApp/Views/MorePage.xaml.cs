using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PanicButtonApp.Models;

namespace PanicButtonApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MorePage : ContentPage
    {
        public ObservableCollection<CustomMenuItem> Items { get; set; }

        public MorePage()
        {
            InitializeComponent();

            Items = new ObservableCollection<CustomMenuItem>
            {
                new CustomMenuItem { Title = "Emergency Contacts", PageType = typeof(ContactsPage) },
                new CustomMenuItem { Title = "Set Profile", PageType = typeof(ProfilePage) },
                new CustomMenuItem { Title = "Set Location", PageType = typeof(LocationPage) }
            };

            MyListView.ItemsSource = Items;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is CustomMenuItem selectedItem)
            {
                var page = (Page)Activator.CreateInstance(selectedItem.PageType);
                await Navigation.PushAsync(page);
            }

            // Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
