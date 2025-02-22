using ADC.API;
using ADC.Currencies;
using NUnit.Framework;
using UnityEngine;

namespace ADC._Tests.Editor.Components.EconomySystem
{
    public class BiofuelManagerTests
    {
        private const int FactionId = 1;
        private readonly Biofuel _initialAmount = new(100m);
        

        [Test]
        public void Init_WhenAlreadyInitialized_DoesNotReinitialize()
        {
            var manager = new BiofuelManager(FactionId);
            manager.Init(_initialAmount);
            manager.Init(_initialAmount*2);

            Assert.DoesNotThrow(() => manager.Init(new Biofuel(200m)));
            Assert.AreEqual(_initialAmount, manager.SaveAmount);
        }

        [Test]
        public void Init_TriggeredEvent_HasCorrectArguments()
        {
            var manager = new BiofuelManager(FactionId);

            Biofuel newValue;
            var valueChange = newValue = default;
            var changeType = CurrencyChangeType.WITHDRAW;
            
            manager.ValueChanged.AddListener( (e) =>
            {
                valueChange = e.Difference;
                newValue = e.NewValue;
                changeType = e.ChangeType;
            });

            manager.Init(_initialAmount);

            Assert.AreEqual(valueChange, 0);
            Assert.AreEqual(newValue, _initialAmount);
            Assert.AreEqual(changeType, CurrencyChangeType.INIT);
            Assert.AreEqual(manager.SaveAmount, _initialAmount);
        }

        [Test]
        public void Deposit_ValidAmount_SucceedsAndUpdateBalance()
        {
            var manager = new BiofuelManager(FactionId);
            manager.Init(_initialAmount);
            var depositAmount = new Biofuel(50m);
            
            Biofuel newValue;
            var valueChange = newValue = default;
            var changeType = CurrencyChangeType.WITHDRAW;
            
            manager.ValueChanged.AddListener( (e) =>
            {
                valueChange = e.Difference;
                newValue = e.NewValue;
                changeType = e.ChangeType;
            });
               
            var result = manager.Deposit(depositAmount);

            Assert.IsTrue(result);
            Assert.AreEqual(valueChange, depositAmount);
            Assert.AreEqual(newValue, _initialAmount+depositAmount);
            Assert.AreEqual(changeType, CurrencyChangeType.DEPOSIT);
            Assert.AreEqual(_initialAmount + depositAmount, manager.SaveAmount);
        }

        [Test]
        public void Deposit_EmptyAmount_FailsAndDoesNotUpdateBalance()
        {
            var manager = new BiofuelManager(FactionId);
            manager.Init(_initialAmount);
            var emptyAmount = new Biofuel(0m);
            var changed = false;
            manager.ValueChanged.AddListener((e) => changed = true);
            
            var result = manager.Deposit(emptyAmount);

            Assert.IsFalse(result);
            Assert.IsFalse(changed);
            Assert.AreEqual(_initialAmount, manager.SaveAmount);
        }

        [Test]
        public void Withdraw_ValidAmount_SucceedsAndUpdateBalance()
        {
            var manager = new BiofuelManager(FactionId);
            manager.Init(_initialAmount);
            var withdrawAmount = new Biofuel(2.0m);
            Biofuel newValue;
            var valueChange = newValue = default;
            var changeType = CurrencyChangeType.INIT;
            manager.ValueChanged.AddListener( (e) =>
            {
                valueChange = e.Difference;
                newValue = e.NewValue;
                changeType = e.ChangeType;
            });

            var result = manager.Withdraw(withdrawAmount);

            Assert.IsTrue(result);
            Assert.AreEqual(valueChange, withdrawAmount);
            Assert.AreEqual(newValue, _initialAmount-withdrawAmount);
            Assert.AreEqual(changeType, CurrencyChangeType.WITHDRAW);
            Assert.AreEqual(_initialAmount-withdrawAmount, manager.SaveAmount);
        }

        [Test]
        public void Withdraw_EmptyAmount_FailsAndDoesNotUpdateBalance()
        {
            var manager = new BiofuelManager(FactionId);
            manager.Init(_initialAmount);
            var emptyAmount = new Biofuel(0m);
            var changed = false;
            manager.ValueChanged.AddListener((e) => changed = true);

            var result = manager.Withdraw(emptyAmount);

            Assert.IsFalse(result);
            Assert.IsFalse(changed);
            Assert.AreEqual(_initialAmount, manager.SaveAmount);
        }

        [Test]
        public void Withdraw_ExceedsBalance_FailsAndDoesNotUpdateBalance()
        {
            var manager = new BiofuelManager(FactionId);
            manager.Init(_initialAmount);
            var excessiveAmount = new Biofuel(150m);
            var changed = false;
            manager.ValueChanged.AddListener((e) => changed = true);

            var result = manager.Withdraw(excessiveAmount);

            Assert.IsFalse(result);
            Assert.IsFalse(changed);
            Assert.AreEqual(_initialAmount, manager.SaveAmount);
        }
    }
}