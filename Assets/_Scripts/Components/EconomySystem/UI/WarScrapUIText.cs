using ADC.API;
using RTSEngine;

namespace ADC.Currencies
{

    public class WarScrapUIText : CurrencyUIText<WarScrap>
    {
        
        protected override void OnCurrencyChanged(object sender, CurrencyChangeEventArgs args)
        {
            if (args.NewValue is not WarScrap warScrap) return;
            if (FactionId == -1)
            {
                if (!args.FactionId.IsLocalPlayerFaction()) return;
                textUI.text = warScrap.Value.ToString($"n{FloatingPoints}");
            }
            else
            {
                if (args.FactionId != FactionId) return;
                textUI.text = warScrap.Value.ToString($"n{FloatingPoints}");
            }
        }
    }
}