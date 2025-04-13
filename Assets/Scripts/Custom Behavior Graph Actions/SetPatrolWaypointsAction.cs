using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Set Patrol Waypoints", story: "Set [Waypoints] from [PatrolPath]", category: "Action", id: "36b1f6bcf43e8698bd3fad27e2ae0146")]
public partial class SetPatrolWaypointsAction : Action
{
    [SerializeReference] public BlackboardVariable<List<GameObject>> Waypoints;
    [SerializeReference] public BlackboardVariable<GameObject> PatrolPath;
    protected override Status OnStart()
    {

        if (Waypoints == null)
        {
            LogFailure("No waypoints to assign.");
            return Status.Failure;
        }
        if (PatrolPath == null)
        {
            LogFailure("No path patrol given.");
            return Status.Failure;
        }
        int waypointCount = PatrolPath.Value.transform.childCount;

        for (int i = 0; i < waypointCount; i++)
        {
           Waypoints.Value.Add(PatrolPath.Value.transform.GetChild(i).gameObject);
        }
        
       
        return Status.Success;
    }

}

