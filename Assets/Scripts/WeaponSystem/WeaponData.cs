using UnityEngine;
[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData", order = 1)]
public class WeaponData : ItemData
{
    public Animation lightAttackAnimation;
    public Animation heavyAttackAnimation;
    public float lightAttackDamage;
    public float heavyAttackDamage;
    public float weaponReach;
    public Vector2 sweetspotRange;
    //public WeaponEffect weaponEffect;
    public float attackSpeed;
    public bool isAttacking = false;
}
