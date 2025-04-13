using System;
using UnityEngine;

public static class CombatEventPublisher
{
    public static event Action<GameObject,float> OnCharacterDamaged;
    public static EventHandler testEvent;
    public static event Action<GameObject,GameObject> OnCharacterDeath;
    public static event Action<GameObject> OnApplyEffect;
   
    public static void TakeDamageEventCall(GameObject target, float damage)
    {
        OnCharacterDamaged?.Invoke(target, damage);
    }
    public static void DeathEventCall(GameObject target, GameObject g)
    {
        OnCharacterDeath?.Invoke(target, g);
    }
    public static void ApplyStatusEffects()
    {

    }
    public static void TestEventCall()
    {
        testEvent?.Invoke(null, EventArgs.Empty);
    }
}

public interface IDamageable
{
    public  void TakeDamage(GameObject target, float damage);
    public void Die(GameObject target, GameObject ragdoll);
}

public interface IDamageDealer
{
    public void DealDamage();
}