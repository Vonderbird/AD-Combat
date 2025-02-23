using System;
using ADC.API;

namespace ADC.Currencies
{
    public class BiofuelManager : ICurrencyManager<Biofuel>
    {
        private bool _isInitialized;
        public Biofuel SaveAmount { get; private set; }
        public int FactionId { get; }
        public event EventHandler<CurrencyChangeEventArgs<Biofuel>> ValueChanged;
        public BiofuelManager(int factionId)
        {
            FactionId = factionId;
        }

        public void Init(Biofuel initAmount)
        {
            if (_isInitialized) return;
            this.SaveAmount = initAmount;
            _isInitialized = true;
            ValueChanged?.Invoke(this, new CurrencyChangeEventArgs<Biofuel>(FactionId, new Biofuel(0m), initAmount, CurrencyChangeType.Init));
        }

        public bool Deposit(Biofuel amount)
        {
            if (amount.IsEmpty) return false;
            SaveAmount += amount;
            ValueChanged?.Invoke(this, new CurrencyChangeEventArgs<Biofuel>(FactionId, amount, SaveAmount, CurrencyChangeType.Deposit));
            return true;
        }

        public bool Withdraw(Biofuel amount)
        {
            if (amount.IsEmpty) return false;
            if (amount > SaveAmount) return false;
            SaveAmount -= amount;
            ValueChanged?.Invoke(this, new CurrencyChangeEventArgs<Biofuel>(FactionId, amount, SaveAmount, CurrencyChangeType.Withdraw));
            return true;
        }
    }
}