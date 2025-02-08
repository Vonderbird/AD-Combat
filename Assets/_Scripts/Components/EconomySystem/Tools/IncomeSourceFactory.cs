using System;
using ADC.API;

namespace ADC.Currencies
{
    public class IncomeSourceFactory : IIncomeSourceFactory
    {
        private readonly IEconomySystem _economySystem;
        private readonly IWaveTimer _waveTimer;

        public IncomeSourceFactory(IWaveTimer waveTimer, IEconomySystem economySystem)
        {
            this._economySystem = economySystem;
            this._waveTimer = waveTimer;
        }

        public IncomeSource Create(ICurrency currency, int factionId)
        {
            return currency switch
            {
                Biofuel biofuel => new BiofuelIncomeSource(_waveTimer, _economySystem, biofuel, factionId),
                WarScrap warScrap => new WarScrapIncomeSource(_waveTimer, _economySystem, warScrap, factionId),
                _ => throw new ArgumentException($"Unsupported currency type: {currency.GetType()}")
            };
        }

    }
}