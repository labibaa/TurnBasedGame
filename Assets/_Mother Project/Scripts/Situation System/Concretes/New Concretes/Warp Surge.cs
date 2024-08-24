using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cysharp.Threading.Tasks;

public class WarpSurge : ICommand
{
    List<GameObject> Path;
    NavMeshAgent Agent;
    bool Automove;
    string ActionType;



    public WarpSurge(List<GameObject> path, NavMeshAgent agent, bool autoMove, string actionType)
    {
        Path = new List<GameObject>();
        Path.AddRange(path);
        Agent = agent;
        Automove = autoMove;
        ActionType = actionType;

        foreach (GameObject gm in Path)
        {
            Debug.Log($"{"Current path: " + gm.name}");
        }

    }


    public async UniTask Execute()
    {
        Agent.gameObject.GetComponent<TemporaryStats>().AutoMove = Automove;

        await CutsceneManager.instance.PlayAnimationForCharacter(Agent.gameObject,"WarpSurge");

        Agent.gameObject.transform.position = Path[Path.Count-1].transform.position;
        await CutsceneManager.instance.PlayAnimationForCharacter(Agent.gameObject, "WarpSurgeEnd");
        //await GridMovement.instance.MoveCharacterGrid(Path, Agent);
        Cursor.lockState = CursorLockMode.None;

        //Agent.Stop();

    }

    public string GetActionName()
    {
        return "WarpSurge";
    }

    public int GetPVValue()
    {
        return 0;
    }

    public CharacterBaseClasses GetTarget()
    {
        return null;
    }
    public int GetAPValue()
    {
        return 0;
    }

    public NavMeshAgent GetAgent()
    {
        return Agent;
    }
    public List<GameObject> GetPaths()
    {
        return Path;
    }
    public string GetActionType()
    {
        return ActionType;
    }
}
