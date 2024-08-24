using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownHandler : MonoBehaviour
{
    public List<Turn> turnPerformed;
    public float currentTime;
    public float waitTime = 5f;

    private void Start()
    {
        StartCoroutine(ExecuteActions());
    }

    private IEnumerator ExecuteActions()
    {
        foreach (Turn turn in turnPerformed)
        {
            // Wait until the current time matches the action's PV
            while (currentTime < turn.PriorityValue)
            {
                yield return null;
            }

            // Execute the action
            Debug.Log("Executing action: " + turn.Command.GetActionName() + " for player: " + turn.Player);

            // Check if there are subsequent actions with the same PV
            List<Turn> samePVActions = GetActionsWithSamePV(turn.PriorityValue);

            if (samePVActions.Count > 1)
            {
                yield return new WaitForSeconds(waitTime);
            }
        }

        Debug.Log("All actions executed.");
    }

    private List<Turn> GetActionsWithSamePV(int pv)
    {
        return turnPerformed.FindAll(turn => turn.PriorityValue == pv);
    }
}
