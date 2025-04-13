using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Find Targets Within Radius", story: "Find [Targets] within [radius] m from [Agent] inside layer: [LayerName]", category: "Action", id: "18dfdc4c5d6c341499ab505c42f5d41b")]
public partial class FindTargetsWithinRadiusAction : Action
{
    [SerializeReference] public BlackboardVariable<List<GameObject>> Targets;
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
        List<GameObject> targetsDetected = new List<GameObject>();
        for (int i = 0; i < colliders.Length; i++)
        {
            targetsDetected.Add(colliders[i].gameObject);
            Debug.Log("Detected object in " + targetLayerMask + " layer: " + targetsDetected[i].name);
        }

        Targets.Value = targetsDetected;

        return Status.Success;

    }
}
