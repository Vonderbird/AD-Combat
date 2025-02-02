using ADC.API;
using ADC.Currencies;
// using NSubstitute;
using Moq;
using NUnit.Framework;
using UnityEngine;

namespace ADC.Editor.Tests
{
    [TestFixture]
    public class BiofuelIncomeSourceTests: IncomeSourceTests
    {
        private Biofuel _biofuel;

        [SetUp]
        public override void Setup()
        {
            base.Setup();
            _biofuel = new Biofuel(50m);
        }

        [Test]
        public void Constructor_InitializesWithCorrectValues()
        {
            // Act
            var source = new BiofuelIncomeSource(
                _waveTimer.Object,
                _economySystem.Object,
                _biofuel,
                FactionId
            );

            // Assert
            Assert.AreEqual(50m, source.PaymentAmount);
            // Assert.AreEqual(1, _waveTimer.Begin.GetPersistentEventCount()); ///????
        }

        [Test]
        public void Update_DepositsBiofuelToCorrectFaction()
        {
            // Arrange
            var factionEconomy = new Mock<IFactionEconomy>();
            factionEconomy.Setup(f=> f.Deposit(It.Is<Biofuel>(b=>b.Value==_biofuel.Value))).Returns(true);
            _economySystem.Setup(e => e[FactionId]).Returns(factionEconomy.Object);
            var source = new BiofuelIncomeSource(_waveTimer.Object, _economySystem.Object, _biofuel, FactionId);

            // Act
            _waveTimer.Object.Begin.Invoke();
            
            // Assert
            factionEconomy.Verify(f=>f.Deposit(It.Is<Biofuel>(b=>b.Value==_biofuel.Value)));
        }
    }
}