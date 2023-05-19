using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    private GameData gameData;
    private List<IDataPersistence> dataPersistences;

    public static DataPersistenceManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Found more than one Data Persistence");
        }

        Instance = this;
    }

    private void Start()
    {
        this.dataPersistences = FindAllDataPersistenceObject();
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void SaveGame()
    {
        
    }

    public void LoadGame()
    {
        if (this.gameData == null)
        {
            Debug.Log("No data was found.");
            NewGame();
        }
    }

    public void OnApplicationQuit()
    {

    }

    private List<IDataPersistence> FindAllDataPersistenceObject()
    {
        IEnumerable<IDataPersistence> dataPersistences = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistences);
    }
}
