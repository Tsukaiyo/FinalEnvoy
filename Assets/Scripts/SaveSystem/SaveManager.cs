using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[System.Serializable]
public class SaveManager : MonoBehaviour
{

    public static SaveManager Instance { get; private set; }
    private SaveData saveData;

    private SaveFileManager saveFileManager;

    private List<ISaveManager> ISaveManagerObjects;
    
    // Ensure only one instance of DataStorage
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    void Start()
    {
        // Find all objects implementing ISaveManager
        this.ISaveManagerObjects = FindSaveManagerObjects();
        this.saveFileManager = new SaveFileManager();

        LoadGame();
    }


    public void NewGame()
    {
        this.saveData = new SaveData();
    }


    // Call the LoadData() of each object implementing ISaveManager 
    public void LoadGame()
    {
        // Read data from file
        this.saveData = saveFileManager.LoadGame();

        // If there was an issue loading the file, start a new game
        if (this.saveData == null)
        {
            NewGame();
        }

        foreach (ISaveManager obj in ISaveManagerObjects)
        {
            obj.LoadData(saveData);
        }
    }

    // Call the SaveData() of each object implementing ISaveManager 
    public void SaveGame()
    {
        
        foreach (ISaveManager obj in ISaveManagerObjects)
        {
            obj.SaveData(ref saveData);
        }

        // Write data to file
        saveFileManager.SaveGame(saveData);
    }


    // Find every object implementing ISaveManager
    private List<ISaveManager> FindSaveManagerObjects(){
        IEnumerable<ISaveManager> saveManagerObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<ISaveManager>();
        return new List<ISaveManager>(saveManagerObjects);
    }


    // TEMPORARY FOR TESTING, IMPLEMENT SAVE/LOAD BUTTONS OR MENU
    void OnApplicationQuit()
    {
        SaveGame();   
    }

}
