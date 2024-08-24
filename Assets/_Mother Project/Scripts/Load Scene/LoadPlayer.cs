using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Playables;

public class LoadPlayer : MonoBehaviour
{
    public static LoadPlayer Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
           
            DontDestroyOnLoad(gameObject);
        }
        else
        {
           
            Destroy(gameObject);
        }
    }

    public void LoadPlayerState()
    {
        string path = Application.persistentDataPath + "/playerState.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            TemporaryStats playerState = JsonUtility.FromJson<TemporaryStats>(json);
            gameObject.GetComponent<TemporaryStats>().CurrentHealth = playerState.CurrentHealth;
            gameObject.GetComponent<TemporaryStats>().CurrentAP = playerState.CurrentAP;
            gameObject.GetComponent<TemporaryStats>().CurrentDex = playerState.CurrentDex;
        }
    }
}
