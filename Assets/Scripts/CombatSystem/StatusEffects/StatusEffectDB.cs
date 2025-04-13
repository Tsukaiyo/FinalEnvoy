using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "StatusEffectDB", menuName = "EffectDB/StatusEffectDB", order = 0)]
public class StatusEffectDB : ScriptableObject
{
    public List<BaseStatusEffect> statusEffects = new();
    public void Initialize()
    {
        statusEffects.Add(new TimedStatusEffect()
        {
            name = "Poison",
            description = "The effected target gets -40% movement speed and -20% attack speed for 15 seconds.",
            duration = 15,
            effectType = EffectType.Poison,
            effects = new List<BaseStatusEffectStatic>
            {
                new EffectMovementSpeed(){ modifierPercent = -40, name = "Movement Speed", description = "-40%", effectType = EffectType.MovementSpeed},
                new EffectAttackSpeed(){modifierPercent = -20, name = "Movement Speed", description = "-40%", effectType = EffectType.AttackSpeed}
            }
        });
    }
}