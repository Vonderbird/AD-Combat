using RTSEngine;

namespace ADC.Currencies
{

    public class WarScrapUIText : CurrencyUIText<WarScrap>
    {
        public override void Refresh(CurrencyChangeEventArgs<WarScrap> args)
        {
            if (!args.FactionId.IsLocalPlayerFaction()) return;
            textUI.text = args.NewValue.Value.ToString($"n{FloatingPoints}");
        }
    }
}