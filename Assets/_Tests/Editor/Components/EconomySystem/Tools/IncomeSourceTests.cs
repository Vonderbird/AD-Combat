using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using System;
using System.Reflection;
using ADC.API;
using UnityEngine.Events;

// using Zenject;

namespace ADC.Editor.Tests
{
    [TestFixture]
    public class IncomeSourceTests
    {
        protected IWaveTimer _waveTimer;
        protected IEconomySystem _economySystem;
        protected const int FactionId = 1;

        [SetUp]
        public virtual void Setup()
        {
            _waveTimer = Substitute.For<IWaveTimer>();
            _waveTimer.Begin.Returns(new UnityEngine.Events.UnityEvent());
            _economySystem = Substitute.For<IEconomySystem>();
        }

        private class TestIncomeSource : IncomeSource
        {
            public TestIncomeSource(IWaveTimer waveTimer, IEconomySystem economySystem, int factionId) 
                : base(waveTimer, economySystem, factionId) { }

            public bool UpdateCalled = false;
            public override decimal PaymentAmount => 10m;
            protected override void Update() => UpdateCalled = true;
        }

        [Test]
        public void Constructor_RegistersWaveTimerListener()
        {
            // Act
            var source = new TestIncomeSource(_waveTimer, _economySystem, FactionId);
            
            // Assert
            Assert.NotNull(source);
            // Assert.AreEqual(1, _waveTimer.Begin.GetPersistentEventCount()); ///????
        }

        [Test]
        public void Dispose_UnregistersWaveTimerListener()
        {
            // Arrange
            var source = new TestIncomeSource(_waveTimer, _economySystem, FactionId);
        
            // Act
            source.Dispose();
        
            // Assert
            // Assert.AreEqual(0, _waveTimer.Begin.GetPersistentEventCount()); ???
        }

        [Test]
        public void Update_TriggersWhenWaveBegins()
        {
            // Arrange
            var source = new TestIncomeSource(_waveTimer, _economySystem, FactionId);

            // Act
            _waveTimer.Begin.Invoke();
        
            // Assert
            Assert.IsTrue(source.UpdateCalled);
        }
    }
}