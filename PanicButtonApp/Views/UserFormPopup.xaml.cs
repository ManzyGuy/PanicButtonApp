using PanicButtonApp.Models;
using PanicButtonApp.ViewModels; // Imports the namespace where ViewModel resides
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PanicButtonApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserFormPopup : PopupPage
    {
        private readonly ProfileViewModel _viewModel; // Add ViewModel as a field

        public User User { get; set; }

        // Modify constructor to accept ProfileViewModel
        public UserFormPopup(ProfileViewModel viewModel, User user = null)
        {
            InitializeComponent();

            _viewModel = viewModel; // Assign the viewModel

            if (user == null)
                User = new User();
            else
                User = user;

            BindingContext = _viewModel;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            await _viewModel.SaveUser(); // Use ViewModel's method to save or update the user
            await PopupNavigation.Instance.PopAsync();
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}
