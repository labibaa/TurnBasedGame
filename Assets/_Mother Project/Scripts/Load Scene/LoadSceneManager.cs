using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
public class LoadSceneManager : MonoBehaviour
{
    public static LoadSceneManager instance;

    private PlayerDataSave playerDataSave;
    public List<PlayerDataSave> SaveCharacterStats = new List<PlayerDataSave>();
    private List<IPersistableData> persistableDataList;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            //var d = new GameObject { name = "[LoadSceneManager]" };
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //var d = new GameObject { name = "[LoadSceneManager]" };
            Destroy(gameObject);
        }
       
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }
    private void Start()
    {
         persistableDataList = FindAllIPersitableDataObjects();
        //OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }
    public async void LoadScene(string sceneName)
    {
        SaveGame();
        var scene = SceneManager.LoadSceneAsync(sceneName);
    }

   /* public void SavePlayerState()
    {
        TemporaryStats playerState = new TemporaryStats(gameObject.GetComponent<TemporaryStats>().CurrentHealth, gameObject.GetComponent<TemporaryStats>().CurrentAP, gameObject.GetComponent<TemporaryStats>().CurrentDex);
        string json = JsonUtility.ToJson(playerState);
        File.WriteAllText(Application.persistentDataPath + "/playerState.json", json);
    }*/
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        persistableDataList = FindAllIPersitableDataObjects();

        LoadGame();
    }

    public void OnSceneUnloaded(Scene scene)
    {
       
       // SaveGame();
    }

    void LoadGame()
    {
        foreach (IPersistableData player_GO in persistableDataList)
        {
            GameObject Ch_obj = ((MonoBehaviour)player_GO).gameObject;
            ShowSavedData.Instance.LoadTemporaryStatsNextScene(Ch_obj);
            // player_GO.LoadData(playerDataSave);
        }
    }

    void SaveGame()
    {
        if (persistableDataList == null || persistableDataList.Count == 0)
        {
            Debug.LogError("Persistable data list is null or empty in SaveGame");
            return;
        }
        foreach (IPersistableData player_GO in persistableDataList)
        {
            GameObject Ch_obj = ((MonoBehaviour)player_GO).gameObject;
            ShowSavedData.Instance.AddCharacterData(Ch_obj);

            // player_GO.SaveData(playerDataSave);
            // SaveCharacterStats.Add(playerDataSave);
            //ShowSavedData.Instance.AddCharacterData(saveData);
        }

    }

    private List<IPersistableData> FindAllIPersitableDataObjects()
    {
        IEnumerable<IPersistableData> ipersistabledataObjects = FindObjectsOfType<MonoBehaviour>().OfType<IPersistableData>();
        
        return new List<IPersistableData>(ipersistabledataObjects);
    }
}
