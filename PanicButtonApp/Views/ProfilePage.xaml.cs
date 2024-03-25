using PanicButtonApp.Models;
using PanicButtonApp.ViewModels;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PanicButtonApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfileViewModel _userViewModel;
        public ProfilePage()
        {
            InitializeComponent();

            _userViewModel = new ProfileViewModel();
            BindingContext = _userViewModel;
        }
        private async void OnAddClicked(object sender, EventArgs e)
        {
            var userFormPopup = new ProfileViewModel(); 
            var popup = new UserFormPopup(userFormPopup);
            await PopupNavigation.Instance.PushAsync(popup);

        }
        //refreshes list view
        private void OnRefreshing(object sender, EventArgs e)
        {
            var _userViewModel = BindingContext as ProfileViewModel;
            _userViewModel.LoadUsers(); // Re-fetch the data
            MyListView.EndRefresh(); // End the refreshing state
        }
        //item tapped event
        public async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            if (e.Item is User tappedUser)
            {
                string userDetails = $"Full Name: {tappedUser.Fullname}\n" +
                                     $"Phone: {tappedUser.Phone}";

                bool deleteConfirmed = await DisplayAlert("User Details", userDetails, "Delete", "OK");

                if (deleteConfirmed)
                {
                    await _userViewModel.DeleteUser(tappedUser);
                    _userViewModel.LoadUsers(); // Refresh the user list after deletion
                }
            }

            ((ListView)sender).SelectedItem = null;
        }


    }
}