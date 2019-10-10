using System;

namespace Entity
{
    public class FinalcialInformation
    {
        private decimal _replacementFund, _socialContribution, _tireService,
                        _vehicleFixService, _nonContractualSecureService, _constactInsuranceService,
                        _socialProtection, _extraordinaryProtection, _administration, _others;
        public FinalcialInformation()
        {
        }

        public decimal ReplacementFund
        {
            get => _replacementFund;
            set => _replacementFund = (value >= 0) ? value : throw new ArgumentException("No se admiten valores negativos");
        }

        public decimal SocialContribution
        {
            get => _socialContribution;
            set => _socialContribution = (value >= 0) ? value : throw new ArgumentException("No se admiten valores negativos");
        }

        public decimal TireService
        {
            get => _tireService;
            set => _tireService = (value >= 0) ? value : throw new ArgumentException("No se admiten valores negativos");
        }

        public decimal VehicleFixService
        {
            get => _vehicleFixService;
            set => _vehicleFixService = (value >= 0) ? value : throw new ArgumentException("No se admiten valores negativos");
        }

        public decimal NonContractualSecureService
        {
            get => _nonContractualSecureService;
            set => _nonContractualSecureService = (value >= 0) ? value : throw new ArgumentException("No se admiten valores negativos");
        }

        public decimal ConstactInsuranceService
        {
            get => _constactInsuranceService;
            set => _constactInsuranceService = (value >= 0) ? value : throw new ArgumentException("No se admiten valores negativos");
        }

        public decimal SocialProtection
        {
            get => _socialProtection;
            set => _socialProtection = (value >= 0) ? value : throw new ArgumentException("No se admiten valores negativos");
        }

        public decimal ExtraordinaryProtection
        {
            get => _extraordinaryProtection;
            set => _extraordinaryProtection = (value >= 0) ? value : throw new ArgumentException("No se admiten valores negativos");
        }

        public decimal Administration
        {
            get => _administration;
            set => _administration = (value >= 0) ? value : throw new ArgumentException("No se admiten valores negativos");
        }

        public decimal Others
        {
            get => _others;
            set => _others = (value >= 0) ? value : throw new ArgumentException("No se admiten valores negativos");
        }

        public decimal CalculateTotal()
        {
            decimal accumulatedValue = 0;

            accumulatedValue += Administration;
            accumulatedValue += ConstactInsuranceService;
            accumulatedValue += ExtraordinaryProtection;
            accumulatedValue += NonContractualSecureService;
            accumulatedValue += Others;
            accumulatedValue += ReplacementFund;
            accumulatedValue += SocialContribution;
            accumulatedValue += SocialProtection;
            accumulatedValue += TireService;
            accumulatedValue += VehicleFixService;

            return accumulatedValue;
        }
    }
}
