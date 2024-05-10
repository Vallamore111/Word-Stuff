using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    private DataManager dataManager;

   
    private void Awake()
    {
        instance = this;
        dataManager = FindObjectOfType<DataManager>();
        
        if (File.Exists(Application.persistentDataPath + "/playerData.dat"))
        { LoadProgress(); }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        { DeleteProgress(); }

        if (Input.GetKeyDown(KeyCode.S))
        { SaveProgress(); }
    }


    public void SaveProgress()
    {
        Debug.Log("Saving...");
        dataManager.SaveLevelData(dataManager.levelData);

        FileStream file = new FileStream(Application.persistentDataPath + "/playerData.dat", FileMode.OpenOrCreate);

        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(file, dataManager.gameData);
        }
        catch (SerializationException error)
        {
            Debug.LogError("Unable to serialize data: " + error.Message);
        }
        finally { file.Close(); }
    }


    public void LoadProgress()
    {
        Debug.Log("Loading...");

        FileStream file = new FileStream(Application.persistentDataPath + "/playerData.dat", FileMode.Open);

        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            dataManager.gameData = formatter.Deserialize(file) as GameData;
        }
        catch (SerializationException error)
        {
            Debug.LogError("Unable to deserialize data: " + error.Message);
        }
        finally { file.Close(); }
    }



    public void DeleteProgress()
    {
        Debug.Log("Progress Deleted.");

        if (File.Exists(Application.persistentDataPath + "/playerData.dat"))
        { File.Delete(Application.persistentDataPath + "/playerData.dat"); }
    }
}
