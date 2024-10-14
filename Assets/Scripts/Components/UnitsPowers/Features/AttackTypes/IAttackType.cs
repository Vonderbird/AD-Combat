using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADC
{
    public interface IAttackType { }

    public class Plasma: IAttackType{}
    public class Sharpened: IAttackType{}
    public class ExplosiveRound : IAttackType{}
    public class Biological: IAttackType{}
}
