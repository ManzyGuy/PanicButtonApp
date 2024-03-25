using PanicButtonApp.Data;
using PanicButtonApp.Models;
using PanicButtonApp.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PanicButtonApp.ViewModels
{
    public class TipsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Tips> _items;
        public ObservableCollection<Tips> Items
        {
            get { return _items; }
            set
            {
                if (_items != value)
                {
                    _items = value;
                    OnPropertyChanged();
                }
            }
        }

        public Database _database;

        public TipsViewModel()
        {
            // Initialize the Items collection
            Items = new ObservableCollection<Tips>();

            // Fetch the platform-specific database path
            var dbPathProvider = DependencyService.Get<IDatabasePathProvider>();
            var dbFullPath = dbPathProvider.GetDatabasePath("DbSample.db3");

            _database = new Database(dbFullPath);
            LoadInMemoryTip();
            LoadTips();
        }

        private void LoadInMemoryTip()
        {
            var inMemoryTips = new List<Tips>
        {
            new Tips {Title="Stay Calm", Description="Try to remain as calm as possible during a home invasion. Panic can cloud your judgment." },
            new Tips {Title="Hide Safely", Description="Find a safe place to hide, preferably in a room with a lock or barricade yourself if possible." },
            new Tips {Title="Call for Help", Description="Use your panic button or dial emergency services if you can do so discreetly." },
            new Tips {Title="Stay Quiet", Description="Stay quiet and avoid making noise that may alert the intruder to your presence." },
            new Tips {Title="Keep Communication", Description="If you can, quietly inform someone you trust about the situation, such as a friend or family member." },
            new Tips {Title="Observe Details", Description="Take mental notes of the intruder’s appearance and any distinguishing features to aid law enforcement." },
        };
            foreach (var tip in inMemoryTips)
            {
                Items.Add(tip);
            }
        }

        public async void LoadTips()
        {
            var tipsList = await _database.GetTipsAsync();
            foreach (var tip in tipsList)
            {
                Items.Add(tip);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task Inserttip(Tips newtip)
        {
            await _database.InsertTipAsync(newtip);
            LoadTips(); // reload the tips to update the listview
        }
    }

}