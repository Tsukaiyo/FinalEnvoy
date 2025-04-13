using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Check Distance of Target", story: "Check Proximity with [RangeDetector]", category: "Conditions", id: "057640869f6d0c31c0058e24b21cf2bd")]
public partial class CheckDistanceOfTargetCondition : Condition
{
    [SerializeReference] public BlackboardVariable<PrincessRange> RangeDetector;

    public override bool IsTrue()
    {
        RangeDetector.Value.Initialize(null, 0);
        return RangeDetector.Value.isEnemyDetected() ? true : false;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
