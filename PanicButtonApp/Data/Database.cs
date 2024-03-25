using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PanicButtonApp.Models;
using SQLite;
using Xamarin.Essentials;



namespace PanicButtonApp.Data
{
    // data access layer to interact with the SQLite database.

    //its basically all table creation if the table doesn't exist and crud operations.
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public string DatabaseName = "DbSample.db3";

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);

            //creates Tips Table if non-existent
            _database.CreateTableAsync<Tips>().Wait();

            //creates Contacts Table if non-existent
            _database.CreateTableAsync<Models.Contacts>().Wait();

            //creates Users Table if non-existent
            _database.CreateTableAsync<User>().Wait();

            //creates Location Table if non-existent
            _database.CreateTableAsync<Models.Location>().Wait();  
        }

        // TIPS TABLE INTERACTIONS

        //fetches tips from database
        public async Task<List<Tips>> GetTipsAsync()
        {
            var count = await _database.Table<Tips>().CountAsync();
            Console.WriteLine(count);
            return await _database.Table<Tips>().ToListAsync();
        }

        //inserts information into database
        public Task<int> InsertTipAsync(Tips tip)
        {
            return _database.InsertAsync(tip);
        }

        //END OF TIPS TABLE INTERACTIONS

        //CONTACTS TABLE INTERACTIONS

        public async Task<List<Models.Contacts>> GetContactsAsync()
        {
            var count = await _database.Table<Models.Contacts>().CountAsync();
            Console.WriteLine(count);
            return await _database.Table<Models.Contacts>().ToListAsync();
        }

        public async Task<int> SaveContact(Models.Contacts contact)
        {
            return await _database.InsertAsync(contact);
        }

        public Task<int> DeleteContactAsync(Models.Contacts contact)
        {
            return _database.DeleteAsync(contact);
        }

        // END OF CONTACTS  INTERACTIONS

        // USER TABLE INTERACTIONS

        public Task<List<User>> GetUsersAsync()
        {
            return _database.Table<User>().ToListAsync();
        }

        public Task<int> SaveUserAsync(User user)
        {
            if (user.UserId != 0)
            {
                return _database.UpdateAsync(user);
            }
            else
            {
                return _database.InsertAsync(user);
            }
        }

        public Task<int> DeleteUserAsync(User user)
        {
            return _database.DeleteAsync(user);
        }

        // END OF USER TABLE INTERACTIONS

        //LOCATION TABLE INTERACTIONS
        public async Task<List<Models.Location>> GetLocationsAsync()
        {
            var count = await _database.Table<Models.Location>().CountAsync();
            Console.WriteLine(count);
            return await _database.Table<Models.Location>().ToListAsync();
        }
        public async Task<int> SaveLocationAsync(Models.Location location)
        {
            return await _database.InsertAsync(location);
        }
        public Task<int> DeleteLocationAsync(Models.Location location)
        {
            return _database.DeleteAsync(location);
        }

        //END OF LOCATION TABLE INTERACTIONS

    }
}