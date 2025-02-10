using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    [SerializeField] SceneField SceneName;
    public String LastPosition;

    private void Update()
    {
       /* if (Input.GetKeyUp(KeyCode.P))
        {
            PlayerPrefs.SetString("LastPosition", LastPosition);
            LoadSceneManager.instance.LoadScene(SceneName);
        }*/
    }
    /* private void OnTriggerEnter(Collider other)
     {
         PlayerPrefs.SetString("LastPosition", LastPosition);
         LoadSceneManager.instance.LoadScene(SceneName);
     }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (LoadSceneManager.instance.ToAddUnlinkedCharacter)
            {
                LoadSceneManager.instance.LoadScene(LoadSceneManager.instance.leftOutcharacters[0].GetComponent<TemporaryStats>().currentScene);
            }
            else
            {
                LoadSceneManager.instance.LoadScene(SceneName);
            }
          
        }
        
    }

    public void LoadNextScene()
    {
        LoadSceneManager.instance.LoadScene(SceneName);
    }
}
