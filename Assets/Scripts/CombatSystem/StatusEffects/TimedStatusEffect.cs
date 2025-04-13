using System.Collections.Generic;
using UnityEngine;

// Timed Status Effect: can contain one or multiple effects to create
// timed compound effects (ex: Poison, Burn, Bleed etc)
[CreateAssetMenu(fileName = "TimedStatusEffect", menuName = "Effects/TimedStatusEffect", order = 0)]
public class TimedStatusEffect : BaseStatusEffect
{
    public float duration = 0f;
    public float timeElapsed = 0f;
    public float timeInterval = 0f; // for DOT
    public bool isDamageOverTime;
    private float period = 0f;

    public List<BaseStatusEffectStatic> effects = new();

    public override void ApplyEffect(IEffectable target)
    {
        if (!isActive)
        {
            foreach (BaseStatusEffectStatic e in effects)
            {
                e.ApplyEffect(target);
            }
            isActive = true;
        }
        else
        {
            UpdateEffect(target);
        }

    }

    public override void RemoveEffect(IEffectable target)
    {
        timeElapsed = duration;
        foreach (BaseStatusEffectStatic e in effects)
        {
            e.RemoveEffect(target);
        }

        target.RemoveEffect(this);
        isActive = false;
    }

    public override void UpdateEffect(IEffectable target)
    {
        if (!isActive)
        {
            ApplyEffect(target);
        }
        else
        {
            if (timeElapsed >= duration)
            {
                RemoveEffect(target);
                return;
            }
            if (isDamageOverTime && period >= timeInterval)
            {
                foreach (BaseStatusEffectStatic e in effects)
                {
                    e.UpdateEffect(target);
                }
                period = 0f;
            }
            period += Time.deltaTime;
            timeElapsed += Time.deltaTime;
        }
    }
}