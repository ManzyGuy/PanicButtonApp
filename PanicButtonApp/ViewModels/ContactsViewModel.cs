using System;
using PanicButtonApp.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;
using System.Threading.Tasks;
using PanicButtonApp.Services;
using System.Runtime.CompilerServices;
using System.Linq;

namespace PanicButtonApp.ViewModels
{
    public class ContactsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Models.Contacts> Items { get; set; }

        private readonly Database _database;

        public ContactsViewModel()
        {
            var dbPathProvider = DependencyService.Get<IDatabasePathProvider>();
            var dbFullPath = dbPathProvider.GetDatabasePath("DbSample.db3");

            _database = new Database(dbFullPath);
            Items = new ObservableCollection<Models.Contacts>();
            LoadContacts();
        }

        public async void LoadContacts()
        {
            var contactsList = await _database.GetContactsAsync();
            //Items = new ObservableCollection<Models.Contacts>(contactsList);
            //OnPropertyChanged(nameof(Items));

            Items.Clear();
            foreach (var contact in contactsList)
            {
                Items.Add(contact);
            }


        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task PickAndSaveContactAsync()
        {
            try
            {
                var contact = await Xamarin.Essentials.Contacts.PickContactAsync();

                if (contact == null)
                    return;

                Models.Contacts contacts = new Models.Contacts
                {
                    ContactId = contact.Id,
                    NamePrefix = contact.NamePrefix,
                    GivenName = contact.GivenName,
                    MiddleName = contact.MiddleName,
                    FamilyName = contact.FamilyName,
                    NameSuffix = contact.NameSuffix,
                    DisplayName = contact.DisplayName,
                    Phones = contact.Phones.FirstOrDefault()?.PhoneNumber,
                    Emails = contact.Emails.FirstOrDefault()?.EmailAddress
                };

                //inserting the contact information 
                await _database.SaveContact(contacts);
                Items.Add(contacts);

                //Print the DisplayName to debug console:
                System.Diagnostics.Debug.WriteLine($"Added Contact:{contacts.DisplayName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
        }

        public async Task DeleteContactAsync(Models.Contacts contact)
        {
            if (contact == null)
                return;

            await _database.DeleteContactAsync(contact);
            Items.Remove(contact);
        }


        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }

        // binding context related 
        //private string displayName;

        //public string DisplayName { get => displayName; set => SetProperty(ref displayName, value); }

        //private string givenName;

        //public string GivenName { get => givenName; set => SetProperty(ref givenName, value); }

        //private string familyName;

        //public string FamilyName { get => familyName; set => SetProperty(ref familyName, value); }

        //private string phones;

        //public string Phones { get => phones; set => SetProperty(ref phones, value); }
    }
}
