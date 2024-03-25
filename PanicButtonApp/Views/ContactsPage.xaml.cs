using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PanicButtonApp.ViewModels;
using PanicButtonApp.Data;
using PanicButtonApp.Models;
using System.Threading.Tasks;

namespace PanicButtonApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactsPage : ContentPage
    {
        public ContactsPage()
        {
            InitializeComponent();

            BindingContext = new ContactsViewModel();
        }

        //public async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        //{
        //    if (e.Item == null)
        //        return;

        //    await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

        //    ((ListView)sender).SelectedItem = null;
        //}

        //tapped item event
        public async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            if (e.Item is Contacts tappedContact)
            {
                string contactDetails = $"Name Prefix: {tappedContact.NamePrefix}\n" +
                               $"Given Name: {tappedContact.GivenName}\n" +
                               $"Middle Name: {tappedContact.MiddleName}\n" +
                               $"Family Name: {tappedContact.FamilyName}\n" +
                               $"Name Suffix: {tappedContact.NameSuffix}\n" +
                               $"Display Name: {tappedContact.DisplayName}\n" +
                               $"Phone: {tappedContact.Phones}\n" +
                               $"Email: {tappedContact.Emails}";

                bool deleteConfirmed = await DisplayAlert("Contact Details", contactDetails, "Delete", "OK");



                if (deleteConfirmed)
                {
                    var CViewModel = BindingContext as ContactsViewModel;
                    await CViewModel.DeleteContactAsync(tappedContact);
                }


            }

            ((ListView)sender).SelectedItem = null;
        }



        //a contact button
        public async void OnAddContactsButtonClicked(object sender, EventArgs e)
        {
            var CViewModel = BindingContext as ContactsViewModel;
            await CViewModel.PickAndSaveContactAsync();
        }

        //refreshes list view
        private void OnRefreshing(object sender, EventArgs e)
        {
            var CViewModel = BindingContext as ContactsViewModel;
            CViewModel.LoadContacts(); // Re-fetch the data
            MyListView.EndRefresh(); // End the refreshing state
        }




    }
}

