using System;
using UnityEngine;

namespace ADC
{
    public class HealingVisitor : IUnitManagerVisitor
    {
        private readonly int healingAmount;

        public HealingVisitor(int healingAmount)
        {
            this.healingAmount = healingAmount;
        }

        public void Visit(SkyForger skyForger)
        {
            skyForger.Specs.Heal(healingAmount * 2);
            Debug.Log($"Healed SkyForger by {healingAmount * 2} HP.");
        }

        public void Visit(TkArty tkArty)
        {
            tkArty.Specs.Heal(healingAmount);
            Debug.Log($"Healed TkArty by {healingAmount} HP.");
        }

        public void Visit(Adamnt adamnt)
        {
            adamnt.Specs.Heal(healingAmount / 2);
            Debug.Log($"Healed Adamnt by {healingAmount / 2} HP.");
        }

        public void Visit(Naloxian naloxian)
        {
            naloxian.Specs.Heal(healingAmount);
            Debug.Log($"Healed Naloxian by {healingAmount} HP.");
        }
    }
}