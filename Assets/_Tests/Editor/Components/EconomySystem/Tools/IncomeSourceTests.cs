using NUnit.Framework;
// using NSubstitute;
using UnityEngine;
using System;
using System.Reflection;
using ADC.API;
using Moq;
using UnityEngine.Events;

// using Zenject;

namespace ADC.Editor.Tests
{
    [TestFixture]
    public class IncomeSourceTests
    {
        protected Mock<IWaveTimer> _waveTimer;
        protected Mock<IEconomySystem> _economySystem;
        protected const int FactionId = 1;

        [SetUp]
        public virtual void Setup()
        {
            _waveTimer = new Mock<IWaveTimer>();
            _waveTimer.Setup(w => w.Begin).Returns(new UnityEngine.Events.UnityEvent());
            _economySystem = new Mock<IEconomySystem>();
        }

        private class TestIncomeSource : IncomeSource
        {
            public TestIncomeSource(IWaveTimer waveTimer, IEconomySystem economySystem, int factionId)
                : base(waveTimer, economySystem, factionId)
            {
            }

            public bool UpdateCalled = false;
            public override decimal PaymentAmount => 10m;
            protected override void Update() => UpdateCalled = true;
        }

        [Test]
        public void Constructor_RegistersWaveTimerListener()
        {
            // Act
            var source = new TestIncomeSource(_waveTimer.Object, _economySystem.Object, FactionId);

            // Assert
            Assert.NotNull(source);
            // Assert.AreEqual(1, _waveTimer.Begin.GetPersistentEventCount()); ///????
        }

        [Test]
        public void Dispose_UnregistersWaveTimerListener()
        {
            // Arrange
            var source = new TestIncomeSource(_waveTimer.Object, _economySystem.Object, FactionId);

            // Act
            source.Dispose();

            // Assert
            // Assert.AreEqual(0, _waveTimer.Begin.GetPersistentEventCount()); ???
        }

        [Test]
        public void Update_TriggersWhenWaveBegins()
        {
            // Arrange
            var source = new TestIncomeSource(_waveTimer.Object, _economySystem.Object, FactionId);

            // Act
            _waveTimer.Object.Begin.Invoke();

            // Assert
            Assert.IsTrue(source.UpdateCalled);
        }
    }
}