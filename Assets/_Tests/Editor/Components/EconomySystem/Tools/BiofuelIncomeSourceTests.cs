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
                _biofuel,
                FactionId
            );

            // Assert
            Assert.AreEqual(50m, source.PaymentAmount.Value);
            // Assert.AreEqual(1, _waveTimer.Begin.GetPersistentEventCount()); ///????
        }

        [Test]
        public void Update_DepositsBiofuelToCorrectFaction()
        {
            // Arrange
            var source = new BiofuelIncomeSource(_waveTimer.Object, _biofuel, FactionId);
            Biofuel receivedIncome = new Biofuel();
            source.IncomeReceived += (o, e) => receivedIncome = (Biofuel)e.IncomeAmount;
            // Act
            _waveTimer.Object.Begin.Invoke();
            
            // Assert
            Assert.AreEqual(receivedIncome, _biofuel);
        }
    }
}