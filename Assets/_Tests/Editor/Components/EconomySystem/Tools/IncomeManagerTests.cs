using NUnit.Framework;
using Moq;
using System;
using ADC.API;
using System.Collections.Generic;
using ADC.Currencies;

namespace ADC._Tests.Editor.Components.EconomySystem.Tools
{
    [TestFixture]
    public class IncomeManagerTests
    {
        private Mock<IIncomeSourceFactory> _mockFactory;
        private Mock<ICurrency> _mockCurrency;
        private IncomeManager _incomeManager;
        private const int FactionID = 1;
        
        [SetUp]
        public void SetUp()
        {
            _mockFactory = new Mock<IIncomeSourceFactory>();
            _mockCurrency = new Mock<ICurrency>(); 
            _incomeManager = new IncomeManager(_mockFactory.Object, FactionID);
        }

        [Test]
        public void AddSource_CallsFactoryWithCorrectParameters()
        {
            const decimal expectedIncome = 10m;
            var mockIncomeSource = EconomyTestUtilities.SetupIncomeSource(expectedIncome, FactionID, Guid.NewGuid(),
                _mockCurrency, _mockFactory);
            
            _mockFactory.Setup(f => f.Create(It.IsAny<ICurrency>(), FactionID))
                 .Returns(mockIncomeSource.Object);
            _incomeManager.AddSource(_mockCurrency.Object);
            
            _mockFactory.Verify(f => f.Create(_mockCurrency.Object, FactionID), Times.Once);
        }

        [Test] 
        public void AddSource_UpdatesTotalIncomeAndTriggersEvent()
        {
            const decimal expectedIncome = 10m;
            var mockIncomeSource = EconomyTestUtilities.SetupIncomeSource(expectedIncome, FactionID, Guid.NewGuid(),
                _mockCurrency, _mockFactory);

            var eventInvoked = false;
            _incomeManager.IncomeChanged.AddListener(_ => eventInvoked = true);

            _incomeManager.AddSource(_mockCurrency.Object);

            Assert.IsTrue(eventInvoked); 
            // Verify total through subsequent removal
            Assert.IsTrue(_incomeManager.RemoveSource(mockIncomeSource.Object.IncomeId));
        }
        
        [Test]
        public void RemoveSource_ExistingId_RemovesSourceAndUpdatesTotal()
        {
            const decimal initialIncome = 15m; 
            var eventArgs = new List<decimal>();
            _incomeManager.IncomeChanged.AddListener(v => eventArgs.Add(v));
            var mockIncomeSource = EconomyTestUtilities.SetupIncomeSource(initialIncome, FactionID, Guid.NewGuid(),
                _mockCurrency, _mockFactory);
            
            _incomeManager.AddSource(_mockCurrency.Object);
            var result = _incomeManager.RemoveSource(mockIncomeSource.Object.IncomeId);
        
            Assert.IsTrue(result);
            mockIncomeSource.Verify(s => s.Dispose(), Times.Once);
            Assert.AreEqual(2, eventArgs.Count); // Add (15) and Remove (15)
            Assert.AreEqual(0m, eventArgs[1]);
        }
        
        [Test]
        public void RemoveSource_NonExistingId_ReturnsFalse()
        {
            var result = _incomeManager.RemoveSource(Guid.NewGuid());
            Assert.IsFalse(result);
        }
        
        [Test]
        public void IncomeReceived_Event_PropagatesFromSource()
        {
            var mockIncomeSource = EconomyTestUtilities.SetupIncomeSource(10m, FactionID, Guid.NewGuid(),
                _mockCurrency, _mockFactory);
            var eventRaised = false;
            _incomeManager.IncomeReceived += (s, e) => eventRaised = true;
            
            _incomeManager.AddSource(_mockCurrency.Object);
            mockIncomeSource.Raise(s => s.IncomeReceived += null, null, new IncomeEventArgs());
        
            Assert.IsTrue(eventRaised);
        }
        
        [Test]
        public void RemoveSource_UnsubscribesFromIncomeEvents()
        {
            var mockIncomeSource = EconomyTestUtilities.SetupIncomeSource(10m, FactionID, Guid.NewGuid(),
                _mockCurrency, _mockFactory);
            var eventRaised = false;
            _incomeManager.IncomeReceived += (s, e) => eventRaised = true;
        
            var sourceId = _incomeManager.AddSource(_mockCurrency.Object);
            mockIncomeSource.Raise(s => s.IncomeReceived += null, null, new IncomeEventArgs());
            
            Assert.IsTrue(eventRaised);
            
            eventRaised = false;
            _incomeManager.RemoveSource(sourceId);
            mockIncomeSource.Raise(s => s.IncomeReceived += null, null, new IncomeEventArgs());
                
            Assert.IsFalse(eventRaised);
        }
        
        [Test]
        public void MultipleOperations_UpdateTotalCorrectly()
        {
            var mockCurrency1 = new Mock<ICurrency>();
            var mockCurrency2 = new Mock<ICurrency>();
            var source1 = EconomyTestUtilities.SetupIncomeSource(10m, FactionID, Guid.NewGuid(),
                mockCurrency1, _mockFactory);
            var source2 = EconomyTestUtilities.SetupIncomeSource(5m, FactionID, Guid.NewGuid(),
                mockCurrency2, _mockFactory);
        
            _mockFactory.SetupSequence(f => f.Create(It.IsAny<ICurrency>(), FactionID))
                .Returns(source1.Object)
                .Returns(source2.Object);
        
            var eventValues = new List<decimal>();
            _incomeManager.IncomeChanged.AddListener(v => eventValues.Add(v));
        
            _incomeManager.AddSource(mockCurrency1.Object); // +10 → 10
            _incomeManager.AddSource(mockCurrency2.Object); // +5 → 15
            _incomeManager.RemoveSource(source1.Object.IncomeId); // -10 →5
            _incomeManager.RemoveSource(source2.Object.IncomeId); // -5 →0
            Assert.AreEqual(new[] { 10m, 15m, 5m, 0m }, eventValues);
        }

        
    }
}