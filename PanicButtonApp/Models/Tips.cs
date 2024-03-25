using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PanicButtonApp.Models
{
    [Table("TipsTable")]
    public class Tips
    {
        [PrimaryKey, AutoIncrement, NotNull, Column("TipId")]
        public int TipId { get; set; }
        [NotNull, Column("Title")]
        public string Title { get; set; }
        [NotNull, Column("Description")]
        public string Description { get; set; }
        

    }
}
