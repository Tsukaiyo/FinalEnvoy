using System.Collections.Generic;
using UnityEngine;

// Weapon effects can be appplied to weapons and be inflicted on targets
// Each weapon effect can have multiple sub effects to create weapon
// properties like Bloodlust, Armour Piercing, and Nimble
[CreateAssetMenu(fileName = "WeaponEffect", menuName = "Effects/WeaponEffect", order = 0)]
public class WeaponEffect : BaseStatusEffect
{
    public List<BaseStatusEffect> weaponEffects;

    public override void ApplyEffect(IEffectable target)
    {
        foreach (BaseStatusEffect effect in weaponEffects)
        {
            target.ApplyEffect(effect);
        }
    }

    public override void RemoveEffect(IEffectable target)
    {
        foreach (BaseStatusEffect effect in weaponEffects)
        {
            target.RemoveEffect(effect);
        }
    }

    public override void UpdateEffect(IEffectable target)
    {
        foreach (BaseStatusEffect effect in weaponEffects)
        {
            target.UpdateEffect();
        }
    }
}