using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Check Target in Vicinity", story: "[Target] is within [Radius] m of [Agent] in Layer: [LayerName]", category: "Conditions", id: "d33e9ffef94d28b7676fdef3d256a378")]
public partial class CheckTargetInVicinityCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<float> Radius;
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<string> LayerName;

    public override bool IsTrue()
    {

        Vector3 agentPosition = Agent.Value.transform.position;
        LayerMask targetLayerMask = LayerMask.GetMask(LayerName.Value);

        Collider[] colliders = Physics.OverlapSphere(agentPosition, Radius.Value, targetLayerMask);
        //Target.Value = null; // Ensure Target.Value is null by default

        if (colliders.Length > 0) // No need for null check, OverlapSphere never returns null
        {
            Target.Value = colliders[0].gameObject;
            Debug.Log($"Detected object in layer '{LayerName.Value}': {Target.Value.name}");
        }
        else
        {
            Debug.Log($"Nothing detected in layer '{LayerName.Value}'");
        }

        return Target.Value != null ? true : false;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
