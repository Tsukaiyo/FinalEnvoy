using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "AttackSpeedEffect", menuName = "Effects/StatusEffects/AttackSpeed")]
public class EffectAttackSpeed : BaseStatusEffectStatic
{
    public override void ApplyEffect(IEffectable target)
    {
        if (isActive) return;
        // Get target and change attack speed    
        isActive = true;
    }

    public override void RemoveEffect(IEffectable target)
    {
        // get target and restore attack speed to original
    }

    public override void UpdateEffect(IEffectable target) { return; }
}
