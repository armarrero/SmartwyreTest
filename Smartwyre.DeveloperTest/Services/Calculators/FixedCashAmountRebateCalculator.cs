using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.Calculators
{
    public class FixedCashAmountRebateCalculator: IRebateCalculator
    {
        public CalculateRebateResult Calculate(Rebate rebate, Product product, CalculateRebateRequest request = null)
        {
            var result = new CalculateRebateResult();

            if (rebate == null)
            {
                result.Success = false;
            }
            else if(product == null)
            {
                result.Success = false;
            }
            else if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount))
            {
                result.Success = false;
            }
            else if (rebate.Amount == 0)
            {
                result.Success = false;
            }
            else
            {
                result.Amount = rebate.Amount;
                result.Success = true;
            }
            return result;
        }
    }
}
