using ADC.Currencies;
using NUnit.Framework;


namespace ADC._Tests.Editor.Components.EconomySystem.Tools
{
    [TestFixture]
    public class WarScrapIncomeSourceTests: IncomeSourceTests
    {
        private WarScrap _warScrap;

        [SetUp]
        public override void Setup()
        {
            base.Setup();
            _warScrap = new WarScrap(50m);
        }

        [Test]
        public void Constructor_InitializesWithCorrectValues()
        {
            var source = new WarScrapIncomeSource(
                _waveTimer.Object,
                _warScrap,
                FactionId
            ); 
            var eventWorked = false; 
            source.IncomeReceived += (o, e)=> eventWorked = true;
            _waveTimer.Raise(w => w.Begin += null, null, 2);
            
            Assert.AreEqual(50m, source.PaymentAmount.Value);
            Assert.IsTrue(eventWorked);
        }

        [Test]
        public void Update_DepositsBiofuelToCorrectFaction()
        {
            // Arrange
            var source = new WarScrapIncomeSource(_waveTimer.Object, _warScrap, FactionId);
            WarScrap receivedIncome = new WarScrap();
            source.IncomeReceived += (o, e) => receivedIncome = (WarScrap)e.IncomeAmount;
            // Act
            _waveTimer.Raise(w => w.Begin += null, null, 2);
            
            // Assert
            Assert.AreEqual(receivedIncome, _warScrap);
        }
    }
}