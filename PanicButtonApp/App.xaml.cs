using PanicButtonApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PanicButtonApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var mainPage = new NavigationPage(new Tabbed())
            {
                // Set the background color of the navigation bar
                BarBackgroundColor = Color.Red,

                // Set the text color of the navigation bar
                BarTextColor = Color.White,

                // Set the background color of the pages to white
                BackgroundColor = Color.White
            };


            MainPage = mainPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
