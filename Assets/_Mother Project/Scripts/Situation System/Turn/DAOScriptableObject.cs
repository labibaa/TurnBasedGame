using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DAOScriptableObject : MonoBehaviour
{
    
    public static DAOScriptableObject instance;
    ActionStat currentAction;
    ImprovedActionStat currentImprovedAction;
    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
    
        if (instance != null && instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            instance = this; 
        } 
    }
    public ActionStat GetActionData(string directory,string actionName)
    {
        
        currentAction = Resources.Load<ActionStat>(directory+actionName);
        return currentAction;

    }
    public ImprovedActionStat GetImprovedActionData(string directory, string actionName)
    {

        currentImprovedAction = Resources.Load<ImprovedActionStat>(directory + actionName);
        return currentImprovedAction;

    }
}
