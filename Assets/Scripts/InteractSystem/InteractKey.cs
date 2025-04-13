using UnityEngine;

public class InteractKey : MonoBehaviour, IInteractable
{
    public string Prompt => "Pick up Key";

    public void interact(GameObject interactor)
    {
        Debug.Log("I am a Key!");
    }
}
