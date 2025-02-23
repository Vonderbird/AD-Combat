using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using ADC.API;
using ADC.Currencies;
using Moq;

namespace ADC._Tests.Editor.Components.EconomySystem
{
    // For our tests we create simple dummy visualizer classes.
    public class DummyBiofuelVisualizer : CurrencyInterface<Biofuel>
    {
        public bool Refreshed { get; private set; } = false;
        public override void Refresh(CurrencyChangeEventArgs<Biofuel> args)
        {
            Refreshed = true;
        }
    }
    public class DummyWarScrapVisualizer : CurrencyInterface<WarScrap>
    {
        public bool Refreshed { get; private set;} = false;
        public override void Refresh(CurrencyChangeEventArgs<WarScrap> args)
        {
            Refreshed = true;
        }
    }
    
    [TestFixture]
    public class FactionEconomyTests
    {
        private FactionEconomy _factionEconomy;
        private readonly int _factionId = 1;
        private GameObject _testGameObject;
        private DummyBiofuelVisualizer _biofuelVisualizer;
        private DummyWarScrapVisualizer _warScrapVisualizer;
        private Mock<IIncomeSourceFactory> _mockFactory;
        private Mock<ICurrency> _mockCurrency;

        private int _argsFactionId = default;
        private CurrencyChangeType _argsChangeType = CurrencyChangeType.Init;
        private Type _argsCurrencyType = null;
        
        [SetUp]
        public void Setup()
        {
            _mockFactory = new Mock<IIncomeSourceFactory>();
            _mockCurrency = new Mock<ICurrency>();
            var incomeSource = EconomyTestUtilities.SetupIncomeSource(10m, _factionId, 
                Guid.NewGuid(), _mockCurrency, _mockFactory);
            
            _testGameObject = new GameObject();
            _factionEconomy = new FactionEconomy();
            _factionEconomy.Init(_mockFactory.Object, _factionId);
            _factionEconomy.Start();
            _factionEconomy.Deposit(new Biofuel(100));
            _factionEconomy.Deposit(new WarScrap(100));
            
            _biofuelVisualizer = _testGameObject.AddComponent<DummyBiofuelVisualizer>();
            _warScrapVisualizer = _testGameObject.AddComponent<DummyWarScrapVisualizer>();
            _factionEconomy.AddVisualizer(_biofuelVisualizer);
            _factionEconomy.AddVisualizer(_warScrapVisualizer);
            
            
            
        }

        [Test]
        public void Init_SetsFactionId_And_CreatesIncomeManager()
        {
            // Verify that the public properties are set.
            Assert.AreEqual(_factionId, _factionEconomy.FactionId);
            Assert.IsNotNull(_factionEconomy.IncomeManager);
        }

        [Test]
        public void Deposit_With_Biofuel_ReturnsTrue()
        {
            // Create a Biofuel instance and call Deposit.
            var biofuel = new Biofuel(5m);
            PrepareCurrencyChangeParameters();
            
            var result = _factionEconomy.Deposit(biofuel);
            
            // We expect a true result (assuming the internal BiofuelManager returns true).
            Assert.IsTrue(result);
            Assert.AreEqual(_factionId, _argsFactionId);
            Assert.AreEqual(CurrencyChangeType.Deposit, _argsChangeType);
            Assert.AreEqual(typeof(Biofuel), _argsCurrencyType);
        }

        [Test]
        public void Withdraw_With_Biofuel_ReturnsTrue()
        {
            var biofuel = new Biofuel(3m);
            PrepareCurrencyChangeParameters();
            
            var result = _factionEconomy.Withdraw(biofuel);
            
            Assert.IsTrue(result);
            Assert.AreEqual(_factionId, _argsFactionId);
            Assert.AreEqual(CurrencyChangeType.Withdraw, _argsChangeType);
            Assert.AreEqual(typeof(Biofuel), _argsCurrencyType);
        }

        [Test]
        public void Deposit_With_WarScrap_ReturnsTrue()
        {
            var warScrap = new WarScrap(10m);
            PrepareCurrencyChangeParameters();
            
            var result = _factionEconomy.Deposit(warScrap);
            
            Assert.IsTrue(result);
            Assert.AreEqual(_factionId, _argsFactionId);
            Assert.AreEqual(CurrencyChangeType.Deposit, _argsChangeType);
            Assert.AreEqual(typeof(WarScrap), _argsCurrencyType);
        }

        [Test]
        public void Withdraw_With_WarScrap_ReturnsTrue()
        {
            var warScrap = new WarScrap(7m);
            PrepareCurrencyChangeParameters();
            
            var result = _factionEconomy.Withdraw(warScrap);
            
            Assert.IsTrue(result);
            Assert.AreEqual(_factionId, _argsFactionId);
            Assert.AreEqual(CurrencyChangeType.Withdraw, _argsChangeType);
            Assert.AreEqual(typeof(WarScrap), _argsCurrencyType);
        }

        [Test]
        public void Deposit_With_UnsupportedCurrency_ReturnsFalse()
        {
            var dummy = new Mock<ICurrency>();
            PrepareCurrencyChangeParameters();
            
            var result = _factionEconomy.Deposit(dummy.Object);
            
            Assert.IsFalse(result);
            Assert.AreEqual(0, _argsFactionId);
            Assert.AreEqual(CurrencyChangeType.Init, _argsChangeType);
            Assert.AreEqual(null, _argsCurrencyType);
        }

        [Test]
        public void Withdraw_With_UnsupportedCurrency_ReturnsFalse()
        {
            var dummy = new Mock<ICurrency>();
            PrepareCurrencyChangeParameters();
            
            var result = _factionEconomy.Withdraw(dummy.Object);
            
            Assert.IsFalse(result);
            Assert.AreEqual(0, _argsFactionId);
            Assert.AreEqual(CurrencyChangeType.Init, _argsChangeType);
            Assert.AreEqual(null, _argsCurrencyType);
        }

        [Test]
        public void AddVisualizer_AddsVisualizersCorrectly()
        {
            // Verify that each visualizer is added to the proper set.
            CollectionAssert.Contains(_factionEconomy.BiofuelVisualizers.ToList(), _biofuelVisualizer);
            CollectionAssert.Contains(_factionEconomy.WarScrapVisualizers.ToList(), _warScrapVisualizer);
        }

        [Test]
        public void AddVisualizers_AddsMultipleVisualizers()
        {
            var biofuelVisualizer1 = _testGameObject.AddComponent<DummyBiofuelVisualizer>();
            var biofuelVisualizer2 = _testGameObject.AddComponent<DummyBiofuelVisualizer>();
            var warScrapVisualizer1 = _testGameObject.AddComponent<DummyWarScrapVisualizer>();
            var warScrapVisualizer2 = _testGameObject.AddComponent<DummyWarScrapVisualizer>();

            var visualizerList = new List<CurrencyInterface>
            {
                biofuelVisualizer1,
                warScrapVisualizer1,
                biofuelVisualizer2,
                warScrapVisualizer2
            };

            _factionEconomy.AddVisualizers(visualizerList);
            
            CollectionAssert.Contains(_factionEconomy.BiofuelVisualizers.ToList(), biofuelVisualizer1);
            CollectionAssert.Contains(_factionEconomy.BiofuelVisualizers.ToList(), biofuelVisualizer2);
            CollectionAssert.Contains(_factionEconomy.WarScrapVisualizers.ToList(), warScrapVisualizer1);
            CollectionAssert.Contains(_factionEconomy.WarScrapVisualizers.ToList(), warScrapVisualizer2);
        }

        private void PrepareCurrencyChangeParameters()
        {
            _argsFactionId = default;
            _argsChangeType = CurrencyChangeType.Init;
            _argsCurrencyType = null;
            _factionEconomy.CurrencyChanged += (o, e) =>
            {
                _argsFactionId = e.FactionId;
                _argsChangeType = e.ChangeType;
                _argsCurrencyType = e.Difference.GetType();

            };
        }
    }
}
