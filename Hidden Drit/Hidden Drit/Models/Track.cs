using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hidden_Drit.Models
{
    public class Track
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreatedID { get; set; }
        public DateTime CreatedDate { get; set; }
        public int TrackTypesId { get; set; }
        public int TrackLevelId { get; set; }
        public string ImagePath { get; internal set; }
        public string ImageURL { get; internal set; }
    }
}

