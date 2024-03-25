using PanicButtonApp.Droid;
using System;
using System.IO;
using Xamarin.Forms;
using PanicButtonApp.Services;


[assembly: Dependency(typeof(DatabasePathProvider))]

namespace PanicButtonApp.Droid
{
    public class DatabasePathProvider : IDatabasePathProvider
    {
        public string GetDatabasePath(string filename)
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
           
            return Path.Combine(folderPath, filename);
        }
    }

}

