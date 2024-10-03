using RTSEngine;

namespace ADC.Currencies
{

    public class WarScrapUIText : CurrencyUIText<WarScrap>
    {
        public override void Refresh(CurrencyChangeEventArgs<WarScrap> args)
        {
            if (!RTSHelper.IsLocalPlayerFaction(args.FactionId)) return;
            textUI.text = args.NewValue.Value.ToString($"n{FloatingPoints}");
        }
    }
}