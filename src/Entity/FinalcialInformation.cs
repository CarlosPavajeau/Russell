
namespace Entity
{
    public class FinalcialInformation
    {
        public FinalcialInformation()
        {
        }

        public decimal ReplacementFund { get; set; }

        public decimal SocialContribution { get; set; }

        public decimal TireService { get; set; }

        public decimal VehicleFixService { get; set; }

        public decimal NonContractualSecureService { get; set; }

        public decimal ConstactInsuranceService { get; set; }

        public decimal SocialProtection { get; set; }

        public decimal ExtraordinaryProtection { get; set; }

        public decimal Administration { get; set; }

        public decimal Others { get; set; }

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
