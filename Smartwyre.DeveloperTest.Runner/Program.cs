using System;
using Microsoft.Extensions.DependencyInjection;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{
    static void Main(string[] args)
    {
        if(args.Length != 3)
        {
            throw new ArgumentException($"Exactly three values (string, string, decimal) must be provided");
        }

        var rebateId = args[0];
        var productId = args[1];
        decimal volume;

        if(!Decimal.TryParse(args[2], out volume))
        {
            throw new ArgumentException($"{args[2]} is not a valid decimal value for volume.");
        }

        var serviceProvider = new ServiceCollection()
           .AddSingleton<IRebateService, RebateService>()
           .BuildServiceProvider();

        var service = serviceProvider.GetService<IRebateService>();

        var result = service.Calculate(new CalculateRebateRequest() { RebateIdentifier = rebateId, ProductIdentifier = productId, Volume = volume});

        Console.WriteLine(result.Success ? $"Rebate calculation was successful in the amount of {result.Amount}" : "Rebate calculation was not successful.");
    }
}
