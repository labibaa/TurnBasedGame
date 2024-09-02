using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class RandomSceneLoad : MonoBehaviour
{
    public List<SceneField> sceneFields = new List<SceneField>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadRandomScene();
        }
    }

    public void LoadRandomScene()
    {
        int r = Random.Range(0, sceneFields.Count);
        LoadSceneManager.instance.LoadScene(sceneFields[r]);
    }
}
