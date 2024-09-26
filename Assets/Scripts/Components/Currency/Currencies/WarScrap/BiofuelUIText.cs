using RTSEngine;
using UnityEngine;

public class BiofuelUIText : CurrencyUIText<Biofuel>
{
    public override void Refresh(CurrencyChangeEventArgs<Biofuel> args)
    {
        if (!RTSHelper.IsLocalPlayerFaction(args.FactionId)) return;
        textUI.text = args.NewValue.Value.ToString($"n{FloatingPoints}");
    }
}