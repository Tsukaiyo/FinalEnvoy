using UnityEngine;
public abstract class BaseStatusEffect : ScriptableObject
{
    public string name = "Status Effect";
    public string description = "Status Effect Description";
    public bool isActive;
    public EffectType effectType;

    public abstract void ApplyEffect(IEffectable target);
    public abstract void RemoveEffect(IEffectable target);
    public abstract void UpdateEffect(IEffectable target);
}

// Status Effect that has no timer
public abstract class BaseStatusEffectStatic : BaseStatusEffect
{
    public int modifierPercent = 0;
    public float damageOverTime = 0f; // positive = damage, negative = heal
    public float initialValue = 0f;
}

// Used on any object that can receive/remove status effects from itself
public interface IEffectable
{
   public void ApplyEffect(BaseStatusEffect effect);
    public void RemoveEffect(BaseStatusEffect effect);
   public void UpdateEffect();
}
public enum EffectType
{
    Poison,
    Bleed,
    Burn,
    MovementSpeed,
    AttackSpeed,
    Health,
    Damage,
    ParryWindow
}