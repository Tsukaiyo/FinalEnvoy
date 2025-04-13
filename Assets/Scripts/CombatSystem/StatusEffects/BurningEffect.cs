using UnityEngine;

[CreateAssetMenu(fileName = "BurningEffect", menuName = "Effects/StatusEffects/BurningEffect")]
public class BurningEffect : BaseStatusEffectStatic
{ 
    HealthSystem healthSystem;
    public float duration = 8f;
    public float timeElapsed = 0f;
    public float timeInterval = 2f; // for DOT
    public bool isDamageOverTime;
    private float period = 0f;
    public override void ApplyEffect(IEffectable target)
    {
        target?.ApplyEffect(this);
    }

    public override void RemoveEffect(IEffectable target)
    {
        target?.RemoveEffect(this);
    }

    public override void UpdateEffect(IEffectable target)
    {
        healthSystem = target as HealthSystem;
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

                healthSystem.TakeDamage(100f);
                period = 0f;
            }
            period += Time.deltaTime;
            timeElapsed += Time.deltaTime;
        }
    }
}
