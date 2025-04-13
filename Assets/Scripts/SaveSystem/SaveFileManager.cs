using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System;

public class SaveFileManager
{
    private const string fileName = "gamesave.json";
    private string savePath = Path.Combine(Application.persistentDataPath, fileName);

    private bool encryptData = false;
    private string encryptionKey = "Spaghetti";


    public SaveData LoadGame()
    {
        SaveData loadedSave = null;

        if (File.Exists(savePath)){
            try
            {
                using (StreamReader reader = File.OpenText(savePath))
                {
                    string saveDataToLoad = reader.ReadToEnd();

                    if (encryptData)
                    {
                        saveDataToLoad = EncryptData(saveDataToLoad);
                    }

                    // Deserialize data
                    loadedSave = JsonUtility.FromJson<SaveData>(saveDataToLoad);
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
            
        }
        else{
            Debug.Log("File does not exist or is wrongly formatted?");
            return null;
        }
        return loadedSave;

    }
    
    public void SaveGame(SaveData data)
    {        
        string dataToFile = JsonUtility.ToJson(data, true);
        Directory.CreateDirectory(Path.GetDirectoryName(savePath));

        if (encryptData)
        {
            dataToFile = EncryptData(dataToFile);
        }
        try
        {
            using (StreamWriter writer = File.CreateText(savePath))
            {
                writer.Write(dataToFile);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

        
    }

    // Encrypts (and decrypts) data 
    private string EncryptData(string data)
    {
        string encryptedData = "";

        // Encrypted char is the result of XOR on a character of the original data and a character of the key 
        for (int i = 0; i < data.Length; i++)
        {
            encryptedData += (char) (data[i] ^ encryptionKey[i % encryptionKey.Length]);
        }

        return encryptedData;
    }
}