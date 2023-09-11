using System;
using Smartwyre.DeveloperTest.Services.Calculators;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests;

public class PaymentServiceTests
{
    [Fact]
    public void TestRebateCalculatorFactory()
    {
        var fixedCashAmountCalculator = RebateCalculatorFactory.GetCalculator(IncentiveType.FixedCashAmount);
        Assert.IsType<FixedCashAmountRebateCalculator>(fixedCashAmountCalculator);

        var fixedRateCalculator = RebateCalculatorFactory.GetCalculator(IncentiveType.FixedRateRebate);
        Assert.IsType<FixedRateRebateCalculator>(fixedRateCalculator);

        var amountPerUomCalculator = RebateCalculatorFactory.GetCalculator(IncentiveType.AmountPerUom);
        Assert.IsType<AmountPerUomRebateCalculator>(amountPerUomCalculator);
    }

    #region FixedCashAmountTests

    [Fact]
    public void FixedCashAmount_ReturnsTrue()
    {
        var rebate = new Rebate()
        {
            Amount = 10
        };

        var product = new Product()
        {
            SupportedIncentives = SupportedIncentiveType.FixedCashAmount
        };

        var actual = RebateCalculatorFactory.GetCalculator(IncentiveType.FixedCashAmount).Calculate(rebate, product, null);
        Assert.True(actual.Success);
        Assert.Equal(10, actual.Amount);
    }

    [Fact]
    public void FixedCashAmountNullRebate_ReturnsFalse()
    {
        var actual = RebateCalculatorFactory.GetCalculator(IncentiveType.FixedCashAmount).Calculate(null, new Product(), null);
        Assert.False(actual.Success);
    }

    [Fact]
    public void FixedCashAmountNullProduct_ReturnsFalse()
    {
        var actual = RebateCalculatorFactory.GetCalculator(IncentiveType.FixedCashAmount).Calculate(new Rebate(), null, null);
        Assert.False(actual.Success);
    }

    [Fact]
    public void FixedCashAmountZeroRebateAmount_ReturnsFalse()
    {
        var rebate = new Rebate()
        {
            Amount = 0
        };

        var product = new Product()
        {
            SupportedIncentives = SupportedIncentiveType.FixedCashAmount
        };

        var actual = RebateCalculatorFactory.GetCalculator(IncentiveType.FixedCashAmount).Calculate(rebate, product, null);
        Assert.False(actual.Success);
    }

    [Fact]
    public void FixedCashAmountNotSupportedByProduct_ReturnsFalse()
    {
        var rebate = new Rebate()
        {
            Amount = 10
        };

        var actual = RebateCalculatorFactory.GetCalculator(IncentiveType.FixedCashAmount).Calculate(rebate, new Product(), null);
        Assert.False(actual.Success);
    }

    #endregion

    #region FixedRateRebateTests
    [Fact]
    public void FixedRateRebate_ReturnsTrue()
    {
        var rebate = new Rebate()
        {
            Percentage = 0.5m
        };

        var product = new Product()
        {
            SupportedIncentives = SupportedIncentiveType.FixedRateRebate,
            Price = 100
        };

        var request = new CalculateRebateRequest()
        {
            Volume = 1
        };

        var actual = RebateCalculatorFactory.GetCalculator(IncentiveType.FixedRateRebate).Calculate(rebate, product, request);
        Assert.True(actual.Success);
        Assert.Equal(50, actual.Amount);
    }

    [Fact]
    public void FixedRateRebateZeroPercentage_ReturnsFalse()
    {
        var product = new Product()
        {
            SupportedIncentives = SupportedIncentiveType.FixedRateRebate,
            Price = 100
        };

        var request = new CalculateRebateRequest()
        {
            Volume = 1
        };

        var actual = RebateCalculatorFactory.GetCalculator(IncentiveType.FixedRateRebate).Calculate(new Rebate(), product, request);
        Assert.False(actual.Success);
    }

    [Fact]
    public void FixedRateRebateZeroPrice_ReturnsFalse()
    {
        var rebate = new Rebate()
        {
            Percentage = 0.5m
        };

        var product = new Product()
        {
            SupportedIncentives = SupportedIncentiveType.FixedRateRebate,
            Price = 0
        };

        var request = new CalculateRebateRequest()
        {
            Volume = 1
        };

        var actual = RebateCalculatorFactory.GetCalculator(IncentiveType.FixedRateRebate).Calculate(rebate, product, request);
        Assert.False(actual.Success);
    }

    [Fact]
    public void FixedRateRebateZeroVolume_ReturnsFalse()
    {
        var rebate = new Rebate()
        {
            Percentage = 0.5m
        };

        var product = new Product()
        {
            SupportedIncentives = SupportedIncentiveType.FixedRateRebate,
            Price = 100
        };

        var request = new CalculateRebateRequest()
        {
            Volume = 0
        };

        var actual = RebateCalculatorFactory.GetCalculator(IncentiveType.FixedRateRebate).Calculate(rebate, product, request);
        Assert.False(actual.Success);
    }
    #endregion

    [Fact]
    public void AmountPerUom_ReturnsTrue()
    {
        var rebate = new Rebate()
        {
            Amount = 10
        };

        var product = new Product()
        {
            SupportedIncentives = SupportedIncentiveType.AmountPerUom
        };

        var request = new CalculateRebateRequest()
        {
            Volume = 10
        };

        var actual = RebateCalculatorFactory.GetCalculator(IncentiveType.AmountPerUom).Calculate(rebate, product, request);
        Assert.True(actual.Success);
        Assert.Equal(100, actual.Amount);
    }

    [Fact]
    public void AmountPerUomAmountZero_ReturnsFalse()
    {
        var rebate = new Rebate()
        {
            Amount = 0
        };

        var product = new Product()
        {
            SupportedIncentives = SupportedIncentiveType.AmountPerUom
        };

        var request = new CalculateRebateRequest()
        {
            Volume = 10
        };

        var actual = RebateCalculatorFactory.GetCalculator(IncentiveType.AmountPerUom).Calculate(rebate, product, request);
        Assert.False(actual.Success);
    }

    [Fact]
    public void AmountPerUomVolumeZero_ReturnsFalse()
    {
        var rebate = new Rebate()
        {
            Amount = 10
        };

        var product = new Product()
        {
            SupportedIncentives = SupportedIncentiveType.AmountPerUom
        };

        var request = new CalculateRebateRequest()
        {
            Volume = 0
        };

        var actual = RebateCalculatorFactory.GetCalculator(IncentiveType.AmountPerUom).Calculate(rebate, product, request);
        Assert.False(actual.Success);
    }

}
