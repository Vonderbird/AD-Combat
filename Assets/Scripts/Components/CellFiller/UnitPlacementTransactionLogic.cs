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

            if (args.Biofuel > 0m && !Produce(args.Biofuel))
            {
                Debug.LogWarning($"Faction {factionId}: Failed to produce Biofuel. Attempting to refund WarScrap.");

                // Attempt to refund WarScrap since Biofuel production failed
                if (Produce(args.WarScrap))
                {
                    Debug.LogError(
                        $"Faction {factionId}: Biofuel production failed, but WarScrap was refunded successfully.");
                    return false;
                }
                else
                {
                    Debug.LogError(
                        $"Faction {factionId}: Critical Error! Failed to refund WarScrap after Biofuel production failure.");
                    // Depending on game design, you might want to handle this scenario differently
                    return false;
                }
            }

            Debug.Log(
                $"Faction {factionId}: Successfully placed unit. Spent {args.WarScrap} WarScrap and gained {args.Biofuel} Biofuel.");
            return true;
        }
    }
}