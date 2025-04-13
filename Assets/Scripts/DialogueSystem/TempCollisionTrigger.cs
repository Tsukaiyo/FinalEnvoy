using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class CollisionTrigger : MonoBehaviour
{
    public bool isForDialogue;
    public bool isForCutscene;
    public bool isForCutsceneLoad;
    public bool isForCheckpoint;

    public string yarnNode;
    public DialogueRunner dialogueRunner;
    public bool repeatableDialogue;
    public string cutsceneName;
    public SaveManager saveManager;

    private void OnTriggerEnter(Collider other)
    {
        if (isForDialogue && other.gameObject.name == "Player")
        {
            dialogueRunner.StartDialogue(yarnNode);
            if (!repeatableDialogue)
            {
                Destroy(this);
            }
        }
        if (isForCutscene && other.gameObject.name == "Player")
        {
            //TODO
            Destroy(this);
        }
        if (isForCheckpoint && other.gameObject.name == "Player")
            //TODO - if (!hasTriggered)
        {
            saveManager.SaveGame();
        }
    }

    private void OnTriggerExit (Collider other)
    {
        if (isForDialogue && other.gameObject.name == "Player")
        {
            //dialogueRunner.CancelDialogue();
        }
    }
}