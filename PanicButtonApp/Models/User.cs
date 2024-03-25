using SQLite;

namespace PanicButtonApp.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int UserId { get; set; }
        
        public string Fullname { get; set; }
        
        public string Phone { get; set; }
    }
}
