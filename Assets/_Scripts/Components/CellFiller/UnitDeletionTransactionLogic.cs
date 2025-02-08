using ADC.API;
using ADC.Currencies;
using UnityEngine;

namespace ADC.UnitCreation
{
    public class UnitDeletionTransactionLogic : TransactionLogic<UnitPlacementCosts>
    {
        public UnitDeletionTransactionLogic(IEconomySystem economySystem, int factionId) : base(economySystem, factionId)
        {
        }

        public override bool Process(UnitPlacementCosts args)
        {

            if (!Produce(args.WarScrap * args.RefundRatio))
            {
                //Debug.LogWarning($"Faction {factionId}: Failed to produce WarScrap.");
                return false;
            }

            //Debug.Log($"Faction {factionId}: Successfully deleted unit and gained {args.WarScrap} WarScrap.");
            return true;
        }
    }
}