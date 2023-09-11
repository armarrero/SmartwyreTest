using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.Calculators
{
    public class RebateCalculatorFactory
    {
        public static IRebateCalculator GetCalculator(IncentiveType incentiveType)
        {
            switch (incentiveType)
            {
                case IncentiveType.FixedCashAmount:
                    return new FixedCashAmountRebateCalculator();

                case IncentiveType.FixedRateRebate:
                    return new FixedRateRebateCalculator();

                case IncentiveType.AmountPerUom:
                    return new AmountPerUomRebateCalculator();

                default: return null;
            }
        }
    }
}
