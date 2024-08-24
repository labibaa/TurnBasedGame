using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerHandler : MonoBehaviour
{
    public static TimerHandler instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one TimeHandler in the scene");
        }
        instance = this;
    }



    public IEnumerator WaitForTime(int time)
    {
        
        yield return new WaitForSeconds(time); 
    }

}
