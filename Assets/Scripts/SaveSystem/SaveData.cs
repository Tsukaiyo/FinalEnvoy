using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class SaveData : SerializedDictionary<string, int> // CHANGE THE VARIABLES HERE DEPENDING ON WHICH DICTIONARY IS NEEDED
{
    // Declare additional data to save here
    public Vector3 playerPos;
    public SerializedDictionary<string, int> quests; // EXAMPLE USAGE
    public SerializedDictionary<string, float> dialogueFloats;
    public SerializedDictionary<string, string> dialogueStrings;
    public SerializedDictionary<string, bool> dialogueBools;

    public SaveData()
    {
        // Initialize values of data
        this.playerPos = Vector3.zero;
        this.quests = new SerializedDictionary<string, int>{{"Quest 1", 0}, {"Quest 2", 0}}; // CAN USE TO TRACK STATE OF NPC DIALOGUE, ITEMS COLLECTED, BOSSES DEFEATED etc
        this.dialogueFloats = new SerializedDictionary<string, float>{{"$dutyAlignment", 0}, {"$relationship", 0}};
        this.dialogueStrings = new SerializedDictionary<string, string>{};
        this.dialogueBools = new SerializedDictionary<string, bool>{};
    }

}
