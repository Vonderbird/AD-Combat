using ADC.Currencies;
using UnityEngine;

namespace ADC.UnitCreation
{
    public class UnitPlacementTransactionLogic : TransactionLogic<UnitPlacementCosts>
    {

        public UnitPlacementTransactionLogic(int factionId) : base(factionId)
        {
        }

        public override bool Process(UnitPlacementCosts args)
        {
            if (!Drain(args.WarScrap))
            {
                Debug.LogWarning($"Faction {factionId}: Insufficient WarScrap to place unit.");
                return false;
            }


            if (!Produce(args.Biofuel))
            {
                Debug.LogWarning($"Faction {factionId}: Failed to produce BioFuel. Attempting to refund WarScrap.");

                // Attempt to refund WarScrap since BioFuel production failed
                if (Produce(args.WarScrap))
                {
                    Debug.LogError(
                        $"Faction {factionId}: BioFuel production failed, but WarScrap was refunded successfully.");
                    return false;
                }
                else
                {
                    Debug.LogError(
                        $"Faction {factionId}: Critical Error! Failed to refund WarScrap after BioFuel production failure.");
                    // Depending on game design, you might want to handle this scenario differently
                    return false;
                }
            }

            Debug.Log(
                $"Faction {factionId}: Successfully placed unit. Spent {args.WarScrap} WarScrap and gained {args.Biofuel} BioFuel.");
            return true;
        }
    }
}