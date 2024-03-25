using System;
using SQLite;

namespace PanicButtonApp.Models
{
    public class Location
    {
        [AutoIncrement, PrimaryKey, NotNull, Column("Location_id")]
        public int Location_id { get; set; }
        [Column("Area")]
        public string Area { get; set; }
        [Column("PlotNumber")]
        public string PlotNumber { get; set; }
        [Column("Street")]
        public string Street { get; set; }
        [Column("Latitude")]
        public double Latitude { get; set; }
        [Column("Longitude")]
        public double Longitude { get; set; }

    }
}
