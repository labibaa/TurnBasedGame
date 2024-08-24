using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
   public static LoadSceneManager instance;

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

    public async void LoadScene(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
    }

    public void SavePlayerState()
    {
        TemporaryStats playerState = new TemporaryStats(gameObject.GetComponent<TemporaryStats>().CurrentHealth, gameObject.GetComponent<TemporaryStats>().CurrentAP, gameObject.GetComponent<TemporaryStats>().CurrentDex);
        string json = JsonUtility.ToJson(playerState);
        File.WriteAllText(Application.persistentDataPath + "/playerState.json", json);
    }

}
