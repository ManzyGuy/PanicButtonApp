using PanicButtonApp.Models;
using PanicButtonApp.ViewModels;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;

namespace PanicButtonApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TipFormPopup : PopupPage
    {
        private readonly TipsViewModel _viewModel; // Access the ViewModel
        public TipFormPopup(TipsViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel; // Pass the ViewModel from the parent page (TipsPage) to this popup
        }
        private async void OnSaveClicked(object sender, EventArgs e)
        {
            //save tip data
            var newTip = new Tips
            {
                Title = TitleEntry.Text,
                Description = DescriptionEntry.Text
            };

            await _viewModel.Inserttip(newTip); // Use the ViewModel to insert the new tip


            await PopupNavigation.Instance.PopAsync();
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

    }
}