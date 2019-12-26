using System;
using System.Collections.Generic;
using System.Linq;

namespace Entity
{
    public enum FinalcialInformationType
    {
        ReplacementFund,
        SocialContribution,
        TireService,
        VehicleFixService,
        NonContractualSecureService,
        ConstactInsuranceService,
        SocialProtection,
        ExtraordinaryProtection,
        Administration,
        Others
    }

    [Serializable]
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

        public decimal Total
        {
            get => _financialInformation.Sum(fi => fi.Value);
        }
    }
}
