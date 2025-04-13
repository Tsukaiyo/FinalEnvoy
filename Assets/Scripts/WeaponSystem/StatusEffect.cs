using UnityEngine;

public  class StatusEffect : ScriptableObject
{
    [SerializeField] private string effectName;
    [SerializeField] private float duration;
    [SerializeField] private float applyFrequency;
    [SerializeField] private float damagePerTick;
    public string Name => effectName;  // Read-only property
    public float Duration => duration;
    public float ApplyFrequency => applyFrequency;
    public float DamagePerTick => damagePerTick;
}



