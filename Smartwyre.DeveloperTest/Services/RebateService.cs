using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services.Calculators;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        var rebateDataStore = new RebateDataStore();
        var productDataStore = new ProductDataStore();

        Rebate rebate = rebateDataStore.GetRebate(request.RebateIdentifier);
        if (rebate == null)
        {
            return new CalculateRebateResult() { Success = false };
        }

        Product product = productDataStore.GetProduct(request.ProductIdentifier);
        if(product == null)
        {
            return new CalculateRebateResult() { Success = false };
        }

        var result = RebateCalculatorFactory.GetCalculator(rebate.Incentive).Calculate(rebate, product, request);

        if (result.Success)
        {
            var storeRebateDataStore = new RebateDataStore();
            storeRebateDataStore.StoreCalculationResult(rebate, result.Amount);
        }

        return result;
    }
}
