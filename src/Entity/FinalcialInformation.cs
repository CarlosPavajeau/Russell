using System.Collections.Generic;
using System.Linq;

namespace Entity
{
    public enum FinalcialInformationType
    {
        REPLACEMENT_FUND,
        SOCIAL_CONTRIBUTION,
        TIRE_SERVICE,
        VEHICLE_FIX_SERVICE,
        NON_CONTRACTIAL_SERCURE_SERVICE,
        CONSTACT_INSURANCE_SERVICE,
        SOCIAL_PROTECTION,
        EXTRAORDINARY_PROTECTION,
        ADMINISTRATION,
        OTHERS
    }
    public class FinalcialInformation
    {
        private readonly Dictionary<FinalcialInformationType, decimal> _financialInformation;

        public FinalcialInformation()
        {
            _financialInformation = new Dictionary<FinalcialInformationType, decimal>();
        }

        public decimal this[FinalcialInformationType type]
        {
            get => _financialInformation[type];
            set => _financialInformation[type] = value;
        }

        public decimal CalculateTotal()
        {
            return _financialInformation.Sum(fi => fi.Value);
        }
    }
}
