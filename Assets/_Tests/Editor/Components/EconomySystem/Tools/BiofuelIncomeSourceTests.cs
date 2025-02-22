using ADC.Currencies;
using NUnit.Framework;


namespace ADC._Tests.Editor.Components.EconomySystem.Tools
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
            var source = new BiofuelIncomeSource(
                _waveTimer.Object,
                _biofuel,
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
            var source = new BiofuelIncomeSource(_waveTimer.Object, _biofuel, FactionId);
            Biofuel receivedIncome = new Biofuel();
            source.IncomeReceived += (o, e) => receivedIncome = (Biofuel)e.IncomeAmount;
            // Act
            _waveTimer.Raise(w => w.Begin += null, null, 2);
            
            // Assert
            Assert.AreEqual(receivedIncome, _biofuel);
        }
    }
}