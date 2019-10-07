using System;
using System.Collections.Generic;

namespace Entity
{
    public class LegalInformation
    {
        private readonly Dictionary<string, Dates> _information;
        public LegalInformation()
        {
            _information = new Dictionary<string, Dates>();
        }

        public Dates this[string type]
        {
            get
            {
                return _information[type];
            }
            set
            {
                _information[type] = value;
            }
        }
    }

    public class Dates
    {
        public Dates(DateTime dueDate, DateTime dateOfRenovation)
        {
            DueDate = dueDate;
            DateOfRenovation = dateOfRenovation;
        }
        public DateTime DueDate { get; set; }

        public DateTime DateOfRenovation { get; set; }
    }
}
