using RTSEngine;
using ADC.API;

namespace ADC.Currencies
{

    public class BiofuelUIText : CurrencyUIText<Biofuel>
    {
        protected override void OnCurrencyChanged(object sender, CurrencyChangeEventArgs args)
        {
            if (args.NewValue is not Biofuel biofuel) return;
            if (FactionId == -1)
            {
                if (!args.FactionId.IsLocalPlayerFaction()) return;
                textUI.text = biofuel.Value.ToString($"n{FloatingPoints}");
            }
            else
            {
                if (args.FactionId != FactionId) return;
                textUI.text = biofuel.Value.ToString($"n{FloatingPoints}");
            }
        }
    }
}