using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    public static LoadSceneManager instance;

    [SerializeField]List<GameObject> gameObjects = new List<GameObject>();

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

    public async void LoadScene(string sceneName)
    {
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
        foreach (GameObject character in gameObjects)
        {
            ShowSavedData.Instance.LoadTemporaryStatsNextScene(character);
            Debug.Log("load data");
        }
    }

    public void OnSceneUnloaded(Scene scene)
    {
        foreach (GameObject character in gameObjects)
        {
            ShowSavedData.Instance.AddCharacterData(character);
            Debug.Log("Save data");
        }
    }

}
