using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]
    public List<ICommand> commands = new List<ICommand>();
    [SerializeField]
    bool isSim = false;
    [SerializeField]
    bool isSequential = false;
    #region
    [SerializeField]
    CharacterBaseClasses player;
    [SerializeField]
    CharacterBaseClasses target;
    [SerializeField]
    TemporaryStats stats;

    public List<CharacterBaseClasses> players = new List<CharacterBaseClasses>();

    #endregion
    public async UniTask AddCommand(ICommand command)
    {
        commands.Add(command);
    }


    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
    }


  async UniTask StartSim()
    {
        foreach (ICommand command in commands)
        {
            if (!isSequential)
            {
                command.Execute();
            }
            else
            {
                await command.Execute();
            }
        }
        commands.Clear();
        GridMovement.instance.EndTurn();       // this resets the grids to its previous state after each turn
    }

    async void Update()
    {
        if (isSim)
        {
            GridMovement.instance.ExecuteModeOn();
            if (!isSequential)
            {
                await StartSim();
            }
            else
            {
                StartSim();
            }
            GridMovement.instance.ExecuteModeOff();
            isSim = false;
        }

    }


    public void CallAction()
    {
        ActionArchive.instance.Choke(player,target,stats);

    }

}
