using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    /// <summary>
    /// The text that will be displayed by the Prompt UI when the player is nearby.
    /// </summary>
    public string Prompt { get; }

    void interact(GameObject interactor); 
}