using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Flee", story: "[Self] performs [FleeCommand]", category: "Action", id: "ed6f9c62c0881d8e7ff6c568fe76f042")]
public partial class FleeAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<PrincessFleeCommand> FleeCommand;
    protected override Status OnStart()
    {
        Debug.Log("Start Flee");
        FleeCommand.Value.Initialize();
        FleeCommand.Value.Flee();
        return Status.Success;
    }

    
}

