using System;
using ADC.API;
using Sisus.Init;

namespace ADC.Currencies
{
    [Service(typeof(IIncomeSourceFactory))]
    public class IncomeSourceFactory : IIncomeSourceFactory, IInitializable<IWaveTimer>
    {
        private IWaveTimer _waveTimer;

        public void Init(IWaveTimer waveTimer)
        {
            this._waveTimer = waveTimer;
        }
        
        public IncomeSource Create(ICurrency currency, int factionId)
        {
            return currency switch
            {
                Biofuel biofuel => new BiofuelIncomeSource(_waveTimer, biofuel, factionId),
                WarScrap warScrap => new WarScrapIncomeSource(_waveTimer, warScrap, factionId),
                _ => throw new ArgumentException($"Unsupported currency type: {currency.GetType()}")
            };
        }

    }
}