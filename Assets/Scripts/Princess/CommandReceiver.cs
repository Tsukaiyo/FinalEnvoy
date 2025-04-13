
using UnityEngine;
using Unity.Behavior;
using System;
public class CommandReceiver : MonoBehaviour
{ 
    BehaviorGraphAgent behaviorAgent;
    bool isBeingCommanded;
    
    void Start()
    {
        behaviorAgent = GetComponent<BehaviorGraphAgent>();
        CommandManager.OnCommandToggle += IsBeingCommanded;
        CommandManager.OnCharacterFollow += StartFollowCommand;
        CommandManager.OnCharacterWait += StartWaitCommand;
        CommandManager.OnCharacterMoveToLocation += StartMoveCommand;
    }
    void IsBeingCommanded(GameObject commandSender, GameObject commandReceiver, bool isCommandToggleOn)
    {
        isBeingCommanded = isCommandToggleOn;
      
        behaviorAgent.SetVariableValue("isBeingCommanded", isBeingCommanded);

        if (isBeingCommanded)
        {
            Debug.Log($"{commandReceiver.name} is awaiting commands from {commandSender.name}");
        }
        else
        {
            Debug.Log($"{commandReceiver.name} is not receiving any commands");
        }
    }
    void MoveToLocation(GameObject sender, GameObject targetLocation)
    {

    }
    void StartFollowCommand(GameObject sender)
    {
        behaviorAgent.SetVariableValue("State", State.Follow);
    }
    void StartWaitCommand(GameObject sender)
    {
        behaviorAgent.SetVariableValue("State", State.Idle);
    }
    void StartMoveCommand(GameObject sender, Vector3 targetLocation)
    {
        behaviorAgent.SetVariableValue("State", State.MoveToLocation);
        behaviorAgent.SetVariableValue("TargetLocation", targetLocation);
    }
}
