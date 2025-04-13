using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


// Static Status Effects (Ex: Health up, attack up, etc):
// These can either be used on their own to apply semi-permanent
// buffs/debuffs or be used in a Timed Status Effect class
[CreateAssetMenu(fileName = "MovementSpeedEffect", menuName = "Effects/StatusEffects/MovementSpeed")]
public class EffectMovementSpeed : BaseStatusEffectStatic
{
    public override void ApplyEffect(IEffectable target)
    {
        if (isActive) return;
        /*
        if (target is Enemy)
        {
            Enemy temp = target as Enemy;
            Enemy enemy = GameObject.FindObjectsByType<Enemy>(FindObjectsSortMode.None).Single(e => e.name == temp.name);
            initialValue = enemy.speed;
            enemy.speed = enemy.speed + enemy.speed * (modifierPercent / 100);
        }
        */
        
        isActive = true;
    }

    public override void RemoveEffect(IEffectable target)
    {
        if (isActive)
        {
            /*
            if (target is Enemy)
            {
                Enemy temp = target as Enemy;
                Enemy enemy = GameObject.FindObjectsByType<Enemy>(FindObjectsSortMode.None).Single(e => e.name == temp.name);
                enemy.speed = initialValue;
            }
            target.RemoveEffect(this);
            */
            isActive = false;
        }
    }
    public override void UpdateEffect(IEffectable target) { return; }

}