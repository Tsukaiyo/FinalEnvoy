using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Set Variable Using Container Length", story: "Set [Variable] to length of [List]", category: "Action/Blackboard", id: "4b4bfc395442f6beb4a137494fa82017")]
public partial class SetVariableUsingContainerLengthAction : Action
{
    [SerializeReference] public BlackboardVariable<int> Variable;
    [SerializeReference] public BlackboardVariable<List<GameObject>> List;

    protected override Status OnStart()
    {

        Variable.Value = List.Value.Count;
        Debug.Log("Enemy Count: " + Variable.Value);
        return Status.Running;
    }


}

