using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
public class EffectHandler : MonoBehaviour
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
  
        }
    }
    public void RemoveEffect(EffectInstance statusEffect)
    {
        if (statusEffects.Contains(statusEffect))
        {
            statusEffects.Remove(statusEffect);
            Debug.Log("Removed effect...");
        }
    }
}


