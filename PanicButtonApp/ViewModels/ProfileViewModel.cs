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
    public class ProfileViewModel
    {
        private readonly Database _database;

        public List<User> _users;
        public ObservableCollection<User> Users { get; set; }

        public User User { get; set; } = new User();

        public ProfileViewModel()
        {

            // Fetch the platform-specific database path
            var dbPathProvider = DependencyService.Get<IDatabasePathProvider>();
            var dbFullPath = dbPathProvider.GetDatabasePath("DbSample.db3");

            _database = new Database(dbFullPath);

            LoadUsers();
        }

        public async void LoadUsers()
        {
            _users = await _database.GetUsersAsync();
            Users = new ObservableCollection<User>(_users);
            OnPropertyChanged(nameof(Users));
        }

        //Methods for Add, Update, and Delete operations

            public async Task SaveUser()
            {
                    await _database.SaveUserAsync(User);
                    LoadUsers(); // Update the list after saving
            }

            public async Task DeleteUser(User userToDelete)
            {
                    await _database.DeleteUserAsync(userToDelete);
                    LoadUsers(); // Update the list after deletion
            }

            // Raises PropertyChanged events for the Users collection

            public event PropertyChangedEventHandler PropertyChanged;

            // Implement INotifyPropertyChanged
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
    }

    
}
