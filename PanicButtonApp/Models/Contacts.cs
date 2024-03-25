using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using PanicButtonApp.Data;
using PanicButtonApp.Services;
using Xamarin.Essentials;
using PanicButtonApp.ViewModels;
using System.Threading.Tasks;

namespace PanicButtonApp.Models
{

    [Table("ContactsTable")]
    public class Contacts
    {
        [PrimaryKey,NotNull, Column("ContactId")]
        public string ContactId { get; set; }
        [Column("NamePrefix")]
        public string NamePrefix { get; set; }
        [Column("GivenName")]
        public string GivenName { get; set; }
        [Column("MiddleName")]
        public string MiddleName { get; set; }
        [Column("FamilyName")]
        public string FamilyName { get; set; }
        [Column("NameSuffix")]
        public string NameSuffix { get; set; }
        [Column("DisplayName")]
        public string DisplayName { get; set; }
        [Column("Phones")]
        public string Phones { get; set; } // Assuming one phone number per contact for simplicity
        [Column("Emails")]
        public string Emails { get; set; } // Assuming one email address per contact for simplicity

        
    }
}
