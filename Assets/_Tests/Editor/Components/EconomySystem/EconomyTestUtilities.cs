using System;
using ADC.API;
using Moq;

namespace ADC._Tests.Editor.Components.EconomySystem
{
    public static class EconomyTestUtilities
    {
        public static Mock<IIncomeSource> SetupIncomeSource(decimal amount, int factionId, Guid? guid, 
            Mock<ICurrency> mockCurrency, Mock<IIncomeSourceFactory> mockFactory)
        {
            var mockIncomeSource = new Mock<IIncomeSource>();
            var paymentCurrency = new Mock<ICurrency>();
            paymentCurrency.Setup(p => p.Value).Returns(amount);
            mockIncomeSource.Setup(s => s.IncomeId).Returns(guid ?? Guid.NewGuid());
            mockIncomeSource.Setup(s => s.PaymentAmount).Returns(paymentCurrency.Object);
            mockCurrency.Setup(c => c.Value).Returns(amount);
            mockFactory.Setup(f => f.Create(It.IsAny<ICurrency>(), factionId))
                .Returns(mockIncomeSource.Object);
            return mockIncomeSource;
        }
    }
}