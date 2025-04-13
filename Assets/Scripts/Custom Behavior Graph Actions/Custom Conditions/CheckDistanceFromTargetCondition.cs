using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Check Distance From Target", story: "Distance of [Target] from [Agent] is: [Distance]", category: "Conditions", id: "f0c22d21212fa30ebd03fd4f0c69c17d")]
public partial class CheckDistanceFromTargetCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<float> Distance;

    public override bool IsTrue()
    {
        Vector3 agentPosition = Agent.Value.transform.position;
        Vector3 targetPosition = Target.Value.transform.position;
       // LayerMask targetLayerMask = LayerMask.GetMask(LayerMaskName.Value);

        float currentDistance = Vector3.Distance(agentPosition, targetPosition);

        return currentDistance < Distance.Value ? true : false;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
