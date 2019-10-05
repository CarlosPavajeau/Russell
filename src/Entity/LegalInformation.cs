using System;
using System.Collections.Generic;

namespace Entity
{
    public class LegalInformation
    {
        public Dictionary<string, Dates> Information;
        public LegalInformation()
        {
            Information = new Dictionary<string, Dates>();
        }

        public void AddInformation(string type, Dates dates)
        {
            Information.Add(type, dates);
        }
    }

    public class Dates
    {
        public DateTime DueDate { get; set; }

        public DateTime DateOfRenovation { get; set; }
    }
}
