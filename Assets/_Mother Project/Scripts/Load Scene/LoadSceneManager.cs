using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System;
using UnityEngine.TextCore.Text;
public class LoadSceneManager : MonoBehaviour
{
    public static LoadSceneManager instance;

    private PlayerDataSave playerDataSave;
    public List<PlayerDataSave> SaveCharacterStats = new List<PlayerDataSave>();
    private List<IPersistableData> persistableDataList;

    public String prevScene;
    bool isPrevScene;
    public List<GameObject> leftOutcharacters = new List<GameObject>();
    public bool ToAddUnlinkedCharacter;
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
        SwitchMC.OnCharacterRemove += LeftOutCharacter;
        SwitchMC.OnPrevScene += LoadPrevScene;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
        SwitchMC.OnPrevScene -= LoadPrevScene;
    }
    private void Start()
    {
         persistableDataList = FindAllIPersitableDataObjects();
        //OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    public async void LoadScene(string sceneName)
    {
        if (!isPrevScene)
        {
            SaveGame();
        }
     

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
       // if (!isPrevScene)
       // {
            LoadGame();
       // }
        isPrevScene = false;
    }

    public void OnSceneUnloaded(Scene scene)
    {
        // SaveGame();
    }
    public void StartNewGame()
    {
        foreach (IPersistableData player_GO in persistableDataList)
        {
            GameObject Ch_obj = ((MonoBehaviour)player_GO).gameObject;
            ShowSavedData.Instance.DefaultCharacterData(Ch_obj);
            Debug.Log("default");
        }
    }
    void LoadGame()
    {
        foreach (IPersistableData player_GO in persistableDataList)
        {
            GameObject Ch_obj = ((MonoBehaviour)player_GO).gameObject;
            ShowSavedData.Instance.LoadTemporaryStatsNextScene(Ch_obj);
            // player_GO.LoadData(playerDataSave);
        }
        SwitchMC.Instance.RemoveUnlinkedCharacter();
    }

    void SaveGame()
    {
        if (persistableDataList == null || persistableDataList.Count == 0)
        {
            Debug.LogError("Persistable data list is null or empty in SaveGame");
            return;
        }
        if (ToAddUnlinkedCharacter)
        {
            SwitchMC.Instance.AddUnlinkedCharacter();
            ToAddUnlinkedCharacter = false;
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

    public void LeftOutCharacter(GameObject leftOutCharacter)
    {
        leftOutcharacters.Clear();
        leftOutcharacters.Add(leftOutCharacter);
    }

    public void LoadPrevScene()
    {
        isPrevScene = true;
        ToAddUnlinkedCharacter = true;
        SwitchMC.Instance.BackToUnlinkedCharacter();
        prevScene = leftOutcharacters[0].GetComponent<TemporaryStats>().currentScene;
        LoadScene(prevScene);
       
    }
}
