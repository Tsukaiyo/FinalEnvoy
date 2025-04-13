using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Collections.Generic;
using Unity.VisualScripting;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Find Target Within Radius", story: "Find [Target] within [Radius] m of [Agent] in layer: [LayerName]", category: "Action", id: "f25c67a8e86e2271e8d8809986dd4df5")]
public partial class FindTargetWithinRadiusAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<float> Radius;
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<string> LayerName;
    protected override Status OnStart()
    {
        if (Agent.Value == null)
        {
            LogFailure("No agent provided.");
            return Status.Failure;
        }

        Vector3 agentPosition = Agent.Value.transform.position;
        LayerMask targetLayerMask = LayerMask.GetMask(LayerName.Value);

        Collider[] colliders = Physics.OverlapSphere(agentPosition, Radius.Value, targetLayerMask);
        Target.Value = null; // Ensure Target.Value is null by default

        if (colliders.Length > 0) // No need for null check, OverlapSphere never returns null
        {
            Target.Value = colliders[0].gameObject;
            Debug.Log($"Detected object in layer '{LayerName.Value}': {Target.Value.name}");
        }
        else
        {
            Debug.Log($"Nothing detected in layer '{LayerName.Value}'");
        }

        return Target.Value != null ? Status.Success : Status.Failure;
    }

}

