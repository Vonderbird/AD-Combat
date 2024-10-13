using RTSEngine;
using System.Linq;

namespace ADC.Currencies
{

    public class BiofuelUIText : CurrencyUIText<Biofuel>
    {
        public override void Refresh(CurrencyChangeEventArgs<Biofuel> args)
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