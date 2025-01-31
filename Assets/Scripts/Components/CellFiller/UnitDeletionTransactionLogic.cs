using ADC.API;
using ADC.Currencies;
using UnityEngine;
using Zenject;

namespace ADC.UnitCreation
{
    public class UnitDeletionTransactionLogic : TransactionLogic<UnitPlacementCosts>
    {
        [Inject]
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