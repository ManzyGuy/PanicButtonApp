using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using Xamarin.Essentials;
using PanicButtonApp.Data;
using PanicButtonApp.Models;
using PanicButtonApp.Services;
using PanicButtonApp.ViewModels;
using PanicButtonApp.Views;
using System.Linq;

namespace PanicButtonApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TriggerPage : ContentPage
    {
        public TriggerPage()
        {
            InitializeComponent();
           
        }

        public async void OnButtonClick(object sender, EventArgs e)
        {


            //instance of the IDatabasePathProvider 
            var pathProvider = DependencyService.Get<IDatabasePathProvider>();
            string dbPath = pathProvider.GetDatabasePath("DbSample.db3");

            var _database = new Database(dbPath);
            //fetchs user info, location info and contacts info

            var user = await _database.GetUsersAsync();
            var location = await _database.GetLocationsAsync();
            var contacts = await _database.GetContactsAsync();
           
            //pushes user and location info to emergencies page


            if(user.Any() && location.Any() )
            {
                await Navigation.PushAsync(new EmergenciesPage(user.First(),location.First()));
            }

            //generated a message for notification
            var contactsName = string.Join(",", contacts.Select(c => c.DisplayName));
            var notificationMessage = $"Alert has been sent to:{contactsName}";

            // Display the notification on the same page (as an alert for simplicity)
            await DisplayAlert("Notification", notificationMessage, "OK");

            // for sms feature


        }
    }
}