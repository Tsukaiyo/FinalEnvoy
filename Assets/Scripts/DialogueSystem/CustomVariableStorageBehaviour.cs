using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using Yarn.Unity;

public class BasicCustomVariableStorageBehaviour : MonoBehaviour, ISaveManager
{
    [SerializeField]
    private InMemoryVariableStorage storedMemory;
    public DialogueRunner dialogueRunner;
    public SaveManager saveManager;

    public void SaveData(ref SaveData saveData)
    {
        var varDicts = storedMemory.GetAllVariables();
        foreach(var items in varDicts.Item1)
        {
            saveData.dialogueFloats[items.Key] = items.Value;
        }
    }

    public void LoadData(SaveData saveData)
    {
        Dictionary<string, float> floats = new Dictionary<string, float>();
        Dictionary<string, string> strings = new Dictionary<string, string>();
        Dictionary<string, bool> bools = new Dictionary<string, bool>();

        foreach(var items in saveData.dialogueFloats)
        {
            floats.Add(items.Key, items.Value);
        }
        foreach(var items in saveData.dialogueStrings)
        {
            strings.Add(items.Key, items.Value);
        }
        foreach(var items in saveData.dialogueBools)
        {
            bools.Add(items.Key, items.Value);
        }

        storedMemory.SetAllVariables(
            floats,
            strings, 
            bools);
    }
}