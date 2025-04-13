using UnityEngine;

using System;


public class CommandManager: MonoBehaviour
{
    public static CommandManager instance;

    public static event Action<GameObject, GameObject, bool> OnCommandToggle;
    public static event Action<GameObject, GameObject> OnCommandSelect;
    public static event Action<GameObject> OnCharacterWait;
    public static EventHandler testEvent;
    public static event Action<GameObject> OnCharacterFollow;
    public static event Action<GameObject, Vector3> OnCharacterMoveToLocation;
    
    private void Start()
    {
        instance = this;
    }
    public void OnCommandToggleCall(GameObject sender, GameObject receiver, bool isCommandToggleOn)
    {
        OnCommandToggle?.Invoke(sender, receiver, isCommandToggleOn);
    }
    public void OnCommandSelectCall(GameObject sender, GameObject receiver)
    {
        OnCommandSelect?.Invoke(sender, receiver);
    }
    public void CharacterWaitEventCall(GameObject sender)
    {
        OnCharacterWait.Invoke(sender);
    }
    public void CharacterFollowCall(GameObject sender)
    {
        OnCharacterFollow?.Invoke(sender);
    }
    public void CharacterMoveToLocation(GameObject sender, Vector3 targetLocation)
    {
        OnCharacterMoveToLocation?.Invoke(sender, targetLocation);
    }
    
    public void TestEventCall()
    {
        testEvent?.Invoke(null, EventArgs.Empty);
    }
}
