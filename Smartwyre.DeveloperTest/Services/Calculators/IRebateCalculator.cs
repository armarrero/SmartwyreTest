using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.Calculators
{
    public interface IRebateCalculator
    {
        CalculateRebateResult Calculate(Rebate rebate, Product product, CalculateRebateRequest request);
    }
}
