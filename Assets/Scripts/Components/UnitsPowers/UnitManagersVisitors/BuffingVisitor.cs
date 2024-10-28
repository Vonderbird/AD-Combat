using UnityEngine;

namespace ADC
{
    public class BuffingVisitor : MonoBehaviour, IUnitManagerVisitor
    {
        [SerializeField] private float buffMultiplier;
        [SerializeField] private float duration;

        public void Initialize(float buffMultiplier, float duration)
        {
            this.buffMultiplier = buffMultiplier;
            this.duration = duration;
        }

        public void Visit(SkyForger skyForger)
        {
            skyForger.Specs.ApplyBuff(
                new UnitDamage((int)(skyForger.Specs.BaseSpecs.UnitDamage * buffMultiplier)),
                duration);
            Debug.Log($"Buffed SkyForger's damage by {buffMultiplier * 100 - 100}% for {duration} seconds.");
        }

        public void Visit(TkArty tkArty)
        {
            tkArty.Specs.ApplyBuff(
                new Armor((int)(tkArty.Specs.BaseSpecs.Armor * buffMultiplier)),
                duration);
            Debug.Log($"Buffed TkArty's armor by {buffMultiplier * 100 - 100}% for {duration} seconds.");
        }

        public void Visit(Adamnt adamnt)
        {
            adamnt.Specs.ApplyBuff(
                new HealthPoint((int)( adamnt.Specs.BaseSpecs.HealthPoint * buffMultiplier)),
                duration);
            Debug.Log($"Buffed Adamnt's health by {buffMultiplier * 100 - 100}% for {duration} seconds.");
        }

        public void Visit(Naloxian naloxian)
        {
            naloxian.Specs.ApplyBuff(
                new ManaPoint((int)( naloxian.Specs.BaseSpecs.ManaPoint* buffMultiplier)),
                duration);
            Debug.Log($"Buffed Naloxian's mana by {buffMultiplier * 100 - 100}% for {duration} seconds.");
        }
    }
}