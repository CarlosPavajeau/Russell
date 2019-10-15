using System;
using System.Collections.Generic;

namespace Entity
{
    public enum LegalInformationType
    {
        LICENSE,
        SOAT,
        OPERATIONCARD,
        TECHNOMECHANICALREVIEW,
        BIMONTHLYREVIEW
    }
    public class LegalInformation
    {
        private readonly Dictionary<LegalInformationType, Dates> _information;
        public LegalInformation()
        {
            _information = new Dictionary<LegalInformationType, Dates>();
        }

        public Dates this[LegalInformationType type]
        {
            get => _information[type];
            set => _information[type] = value;
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
