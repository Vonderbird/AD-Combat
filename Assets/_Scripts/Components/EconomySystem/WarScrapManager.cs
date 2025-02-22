using ADC.API;
using UnityEngine;

namespace ADC.Currencies
{

    public class WarScrapManager : CurrencyManager<WarScrap>
    {
        public WarScrapManager(int factionId)
        {
            this.factionId = factionId;
        }

        public override void Init(WarScrap initAmount)
        {
            if (isInitialized) return;
            saveAmount += initAmount;
            isInitialized = true;
            ValueChanged?.Invoke(new CurrencyChangeEventArgs<WarScrap>(factionId, new WarScrap(0m), initAmount,
                CurrencyChangeType.INIT));
        }

        public override bool Deposit(WarScrap amount)
        {
            if (amount.IsEmpty) return false;
            saveAmount += amount;
            //Debug.Log($"warscrap deposit amount: {amount}");
            ValueChanged?.Invoke(
                new CurrencyChangeEventArgs<WarScrap>(factionId, amount, saveAmount, CurrencyChangeType.DEPOSIT));
            return true;
        }

        public override bool Withdraw(WarScrap amount)
        {
            if (amount.IsEmpty) return false;
            if (amount > saveAmount) return false;
            saveAmount -= amount;
            //Debug.Log($"warscrap withdraw amount: {amount}");
            ValueChanged?.Invoke(new CurrencyChangeEventArgs<WarScrap>(factionId, amount, saveAmount,
                CurrencyChangeType.WITHDRAW));
            return true;
        }
    }
}