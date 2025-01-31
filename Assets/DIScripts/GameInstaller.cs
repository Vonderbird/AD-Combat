using ADC.API;
using ADC.Currencies;
using UnityEngine;
using Zenject;

namespace ADC.DI
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private EconomySystem economySystem;
        [SerializeField] private WaveTimer waveTimer;

        public override void InstallBindings()
        {
            Container.Bind<IEconomySystem>().FromInstance(economySystem);
            Container.Bind<IWaveTimer>().FromInstance(waveTimer);
            Container.Bind<IIncomeSourceFactory>().To<IncomeSourceFactory>().AsSingle();
        }
    }
}
