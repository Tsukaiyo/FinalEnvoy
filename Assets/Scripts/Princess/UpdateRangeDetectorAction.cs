using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Update Range Detector", story: "Update [RangeDetector] to find [Target] within [FollowRange] and whether [isNearEnemy]", category: "Action", id: "3d2357ca87c23e3aef8c0df19393b955")]
public partial class UpdateRangeDetectorAction : Action
{
    [SerializeReference] public BlackboardVariable<PrincessRange> RangeDetector;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<float> FollowRange;
    [SerializeReference] public BlackboardVariable<bool> IsNearEnemy;
    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        RangeDetector.Value.Initialize(Target.Value, FollowRange.Value);
        RangeDetector.Value.isPlayerDetected();
        RangeDetector.Value.isEnemyDetected();
        //Debug.Log("IsEnemyNear: " + IsNearEnemy);
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

