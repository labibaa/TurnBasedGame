using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEntrancePosition : MonoBehaviour
{
    public string lastScenePosition;

    private void Start()
    {
        if(PlayerPrefs.GetString("LastPosition") == lastScenePosition)
        {
            LoadPlayer.Instance.transform.position = transform.position;
            LoadPlayer.Instance.transform.eulerAngles = transform.eulerAngles;
            Debug.Log("inn");
        }
    }
}
