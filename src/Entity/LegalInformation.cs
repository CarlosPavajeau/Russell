using System;
using System.Collections.Generic;

namespace Entity
{
    public enum LegalInformationType
    {
        License,
        Soat,
        OperationCard,
        TechnomechanicalReview,
        BiMonthlyReview
    }

    [Serializable]
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
            private set => _information[type] = value;
        }

        public void AddLegalInformation(LegalInformationType type, DateTime dueDate, DateTime dateOfRenovation)
        {
            _information[type] = new Dates(dueDate, dateOfRenovation);
        }

        public Dictionary<LegalInformationType, Dates> GetLegalInformation()
        {
            return _information;
        }
    }

    [Serializable]
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
