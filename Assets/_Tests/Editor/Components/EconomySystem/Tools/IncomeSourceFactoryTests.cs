using System;
using ADC.API;
using ADC.Currencies;
using Moq;
using NUnit.Framework;

namespace ADC._Tests.Editor.Components.EconomySystem.Tools
{
    [TestFixture]
    public class IncomeSourceFactoryTests
    {
        private Mock<IWaveTimer> _mockWaveTimer;
        private IncomeSourceFactory _factory;
        private const int FactionId = 1;

        [SetUp]
        public void Setup()
        {
            _mockWaveTimer = new Mock<IWaveTimer>();
            _factory = new IncomeSourceFactory();
            _factory.Init(_mockWaveTimer.Object);
        }

        [Test]
        public void Create_WithBiofuel_ReturnsBiofuelIncomeSource()
        {
            // Arrange
            var biofuel = new Biofuel(10m);

            // Act
            var result = _factory.Create(biofuel, FactionId);
            ICurrency incomeAmount;
            result.IncomeReceived += (o, e) => incomeAmount = e.IncomeAmount;

            _mockWaveTimer.Raise(m=>m.Begin+=null, null, 2);
            // Assert
            Assert.IsInstanceOf<BiofuelIncomeSource>(result);
            var concreteResult = (BiofuelIncomeSource)result;
            
            // Assert.AreEqual(_mockWaveTimer.Object, concreteResult.WaveTimer);
            Assert.AreEqual(biofuel, concreteResult.PaymentAmount);
            // Assert.AreEqual(FactionId, concreteResult.FactionId); ///????????????????
        } 

        [Test]
        public void Create_WithWarScrap_ReturnsWarScrapIncomeSource()
        {
            // Arrange
            var warScrap = new WarScrap(5m);

            // Act
            var result = _factory.Create(warScrap, FactionId);

            // Assert
            Assert.IsInstanceOf<WarScrapIncomeSource>(result);
            var concreteResult = (WarScrapIncomeSource)result;
            // Assert.AreEqual(_mockWaveTimer.Object, concreteResult.WaveTimer);
            Assert.AreEqual(warScrap, concreteResult.PaymentAmount);
            // Assert.AreEqual(FactionId, concreteResult.FactionId); //??????????????????
        }

        [Test]
        public void Create_WithUnsupportedCurrency_ThrowsArgumentException()
        {
            // Arrange
            var unsupportedCurrency = new Mock<ICurrency>().Object;

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => 
                _factory.Create(unsupportedCurrency, FactionId));
            StringAssert.Contains("Unsupported currency type", ex.Message);
        }

        [Test]
        public void Create_BeforeInitialization_ThrowsNullReferenceException()
        {
            // Arrange
            var uninitializedFactory = new IncomeSourceFactory();
            var biofuel = new Biofuel(10m);

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => 
                uninitializedFactory.Create(biofuel, FactionId));
        }

        [Test]
        public void Init_WhenReinitialized_UpdatesWaveTimer()
        {
            var newWaveTimer = new Mock<IWaveTimer>();
            var biofuel = new Biofuel(10m);

            _factory.Init(newWaveTimer.Object);
            var result = (BiofuelIncomeSource)_factory.Create(biofuel, FactionId);
            var newWaveWorked = false;
            result.IncomeReceived += (o, i) => newWaveWorked = true;
            newWaveTimer.Raise(w => w.Begin += null, null, 2);
            
            Assert.IsTrue(newWaveWorked);
        }
    }

}