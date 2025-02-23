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
    [TestFixture]
    public class FactionEconomyTests
    {
        private FactionEconomy _factionEconomy;
        private const int FactionId = 1;

        private Mock<IIncomeSourceFactory> _mockFactory;
        private Mock<ICurrency> _mockCurrency;

        private int _argsFactionId;
        private CurrencyChangeType _argsChangeType = CurrencyChangeType.Init;
        private Type _argsCurrencyType;
        
        [SetUp]
        public void Setup()
        {
            _mockFactory = new Mock<IIncomeSourceFactory>();
            _mockCurrency = new Mock<ICurrency>();
            var incomeSource = EconomyTestUtilities.SetupIncomeSource(10m, FactionId, 
                Guid.NewGuid(), _mockCurrency, _mockFactory);
            
            _factionEconomy = new FactionEconomy();
            _factionEconomy.Init(_mockFactory.Object, FactionId);
            _factionEconomy.Start();
            _factionEconomy.Deposit(new Biofuel(100));
            _factionEconomy.Deposit(new WarScrap(100));
        }

        [Test]
        public void Init_SetsFactionId_And_CreatesIncomeManager()
        {
            // Verify that the public properties are set.
            Assert.AreEqual(FactionId, _factionEconomy.FactionId);
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
            Assert.AreEqual(FactionId, _argsFactionId);
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
            Assert.AreEqual(FactionId, _argsFactionId);
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
            Assert.AreEqual(FactionId, _argsFactionId);
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
            Assert.AreEqual(FactionId, _argsFactionId);
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
        private void PrepareCurrencyChangeParameters()
        {
            _argsFactionId = 0;
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
