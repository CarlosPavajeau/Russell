using System;

namespace Entity
{
    public class LegalInformation
    {
        public LegalInformation(DateTime dueDate, DateTime dateOfRenovation)
        {
            DueDate = dueDate;
            DateOfRenovation = dateOfRenovation;
        }

        public DateTime DueDate
        {
            get;
            set;
        }

        public DateTime DateOfRenovation
        {
            get;
            set;
        }
    }
}
