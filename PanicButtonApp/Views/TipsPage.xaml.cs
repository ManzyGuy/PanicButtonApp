using PanicButtonApp.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PanicButtonApp.Models;

namespace PanicButtonApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TipsPage : ContentPage
    {
        public readonly TipsViewModel ViewModel;

        public TipsPage()
        {
            InitializeComponent();

            // Initialize the ViewModel
            ViewModel = new TipsViewModel();

            // Set the BindingContext to the ViewModel
            BindingContext = ViewModel;

            ViewModel.LoadTips();
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            var tappedTip = e.Item as Models.Tips; // Cast the tapped item to the Tips model
            await DisplayAlert("Tip Details", $"Title: {tappedTip.Title}\nDescription: {tappedTip.Description}", "OK");

            // Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        private async void OnAddTipClicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as TipsViewModel;

            var tipFormPopup = new TipFormPopup(viewModel);
            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(tipFormPopup);
        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    ToggleVisibilityButton.Clicked += OnToggleVisibilityClicked;
        //}

        //protected override void OnDisappearing() 
        //{  
        //    base.OnDisappearing();
        //    ToggleVisibilityButton.Clicked -= OnToggleVisibilityClicked;
        //}

        //hide entries section button
        //private void OnToggleVisibilityClicked(object sender, EventArgs e)
        //{
        //    // Toggle the visibility of the Entry fields and the Button

        //    TitleEntry.IsVisible = !TitleEntry.IsVisible;
        //    DescriptionEntry.IsVisible = !DescriptionEntry.IsVisible;
        //    InsertTipButton.IsVisible = !InsertTipButton.IsVisible;

        //    // Toggle the text of the special button based on visibility
        //    ToggleVisibilityButton.Text = TitleEntry.IsVisible ? "Hide" : "Add Tip";
        //}



        //async void OnInsertTipClicked(object sender, EventArgs e)
        //{
        //    var newtip = new Tips
        //    {
        //        Title = TitleEntry.Text,
        //        Description = DescriptionEntry.Text
        //    };

        //    await ViewModel.Inserttip(newtip);
        //    TitleEntry.Text = string.Empty; //clears entries
        //    DescriptionEntry.Text = string.Empty;
        //}

    }

}

