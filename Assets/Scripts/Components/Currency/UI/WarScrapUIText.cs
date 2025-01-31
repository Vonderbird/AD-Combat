using ADC.API;
using RTSEngine;

namespace ADC.Currencies
{

    public class WarScrapUIText : CurrencyUIText<WarScrap>
    {
        public override void Refresh(CurrencyChangeEventArgs<WarScrap> args)
        {
            if (FactionId == -1)
            {
                if (!args.FactionId.IsLocalPlayerFaction()) return;
                textUI.text = args.NewValue.Value.ToString($"n{FloatingPoints}");
            }
            else
            {
                if (args.FactionId != FactionId) return;
                textUI.text = args.NewValue.Value.ToString($"n{FloatingPoints}");
            }
        }
    }
}