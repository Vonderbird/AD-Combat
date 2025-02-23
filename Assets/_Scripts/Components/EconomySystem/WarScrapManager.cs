using System;
using ADC.API;

namespace ADC.Currencies
{

    public class WarScrapManager : ICurrencyManager<WarScrap>
    {
        private bool _isInitialized;
        public WarScrap SaveAmount { get; private set; }

        public int FactionId { get; }

        public event EventHandler<CurrencyChangeEventArgs<WarScrap>> ValueChanged;
        
        public WarScrapManager(int factionId)
        {
            FactionId = factionId;
        }

        public void Init(WarScrap initAmount)
        {
            if (_isInitialized) return;
            SaveAmount += initAmount;
            _isInitialized = true;
            ValueChanged?.Invoke(this, new CurrencyChangeEventArgs<WarScrap>(
                FactionId, new WarScrap(0m), initAmount, CurrencyChangeType.Init));
        }

        public bool Deposit(WarScrap amount)
        {
            if (amount.IsEmpty) return false;
            SaveAmount += amount;
            //Debug.Log($"warscrap deposit amount: {amount}");
            ValueChanged?.Invoke(this, new CurrencyChangeEventArgs<WarScrap>(
                FactionId, amount, SaveAmount, CurrencyChangeType.Deposit));
            return true;
        }

        public bool Withdraw(WarScrap amount)
        {
            if (amount.IsEmpty) return false;
            if (amount > SaveAmount) return false;
            SaveAmount -= amount;
            //Debug.Log($"warscrap withdraw amount: {amount}");
            ValueChanged?.Invoke(this, new CurrencyChangeEventArgs<WarScrap>(
                FactionId, amount, SaveAmount, CurrencyChangeType.Withdraw));
            return true;
        }
    }
}