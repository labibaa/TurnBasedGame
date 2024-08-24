using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveCue : MonoBehaviour
{
    public static RemoveCue instance;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void RemoveAllCues()
    {
        List<GameObject> listOfCue = new List<GameObject>();


        listOfCue.AddRange(GameObject.FindGameObjectsWithTag("Cue"));

        foreach (GameObject gm in listOfCue)
        {
            Destroy(gm);
            //if (gm.name == gameObject.name) in case of player clash
            //{
            //    //destroying the visual cue that remains
            //}
        }
    }
}
