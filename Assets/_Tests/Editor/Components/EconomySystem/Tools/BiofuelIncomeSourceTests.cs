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
            // Debug.Log($">> _biofuel {_biofuel}");
            factionEconomy.Setup(f=> f.Deposit(It.IsAny<Biofuel>())).Returns(true);
            // factionEconomy.Deposit(Arg.Is<Biofuel>(b => b.Value == _biofuel.Value)).Returns(true);
            // _economySystem[FactionId].Returns(factionEconomy);
            // var source = new BiofuelIncomeSource(_waveTimer, _economySystem, _biofuel, FactionId);

            // Act
            // _waveTimer.Begin.Invoke();
            
            // Assert
            // Debug.Log($">> {_economySystem[FactionId].Deposit(_biofuel)}, _biofuel {_biofuel}");
            Debug.Log($">> {factionEconomy.Object.Deposit(_biofuel)}");
            // _economySystem[FactionId].Received().Deposit((_biofuel));
            // Debug.Log($">>1 {factionEconomy.Deposit(_biofuel)}");
            factionEconomy.Verify(f=>f.Deposit(It.IsAny<Biofuel>()));
        }
    }
}