using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectManager : MonoBehaviour
{
    [SerializeField] List<EffectInstance> statusEffects = new List<EffectInstance>();
    public void AddEffect(EffectInstance statusEffect)
    {
        if (!statusEffects.Contains(statusEffect))
        {
            statusEffects.Add(statusEffect);
            StartCoroutine(statusEffect.ApplyEffectCoroutine());  //Begin effect logic as soon as it is added
        }
        else if (statusEffects.Contains(statusEffect))
        {
            Debug.Log(transform.name + "is already " + statusEffect.name + " ...");
        }
    }
    public void RemoveEffect(EffectInstance statusEffect)
    {
        if (statusEffects.Contains(statusEffect))
        {
            statusEffects.Remove(statusEffect);
        }
    }
    public List<EffectInstance> GetEffectInstances() { return statusEffects; }
}
[System.Serializable]
public class EffectInstance
{
    StatusEffect effectSO;  // Reference to the shared ScriptableObject
    GameObject target;
    [SerializeField] public string name;
    [SerializeField] float timeRemaining;
    StatusEffectManager statusEffectManager;

    public EffectInstance(StatusEffect scriptableEffect, GameObject target)
    { 
        statusEffectManager = target.GetComponent<StatusEffectManager>();
        effectSO = scriptableEffect;
        this.name = effectSO.Name;
        this.timeRemaining = effectSO.Duration;
        this.target = target;
        
    }
    public void AddEffectToTarget()
    {
       statusEffectManager.AddEffect(this);
    }

    public IEnumerator ApplyEffectCoroutine()
    {
        while (timeRemaining > 0)
        {
            ApplyHealthEffects();
            ApplyWeaponEffects();
            ApplyMovementEffects();

            yield return new WaitForSeconds(effectSO.ApplyFrequency);
            
            timeRemaining -= effectSO.ApplyFrequency;
        }
        statusEffectManager.RemoveEffect(this);
    }
    public void ApplyHealthEffects()
    {
        HealthSystem healthSystem = target.GetComponent<HealthSystem>();
        healthSystem.TakeEffectDamage(effectSO.DamagePerTick);
    }
    public void ApplyWeaponEffects()
    {

    }
    public void ApplyMovementEffects()
    {

    }
}
