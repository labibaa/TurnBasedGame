using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;


public class HandleTurn : MonoBehaviour
{
    public static HandleTurn instance;

    public ClashCalculation clashCalculation;


    public List<Turn> allTurns = new List<Turn>();
    public List<Turn> performedTurns = new List<Turn>();
    public HashSet<Turn> turnsToBePerformedHS = new HashSet<Turn>();
    public List<Turn> turnsToBePerformed = new List<Turn>();
    public List<CharacterBaseClasses> looserOfClash = new List<CharacterBaseClasses>();
    public List<Turn> eligibleTurnsForClash = new List<Turn>();

    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        foreach (var turn in GetFirstTurns())
        {
            Debug.Log(GetNextTurnOfPlayer(turn.Player).PriorityValue);
        }
    }

    public List<Turn> SortedTurns()
    {
        allTurns.Sort((t1,t2)=>t1.PriorityValue.CompareTo(t2.PriorityValue));
        
        /*List<Turn>
        List<Turn> turnsWithSamePV;
        foreach (Turn turn in allTurns)
        {
            
        }*/
        
        return allTurns;
    }

    public List<Turn> GetFirstTurns()
    {
        performedTurns = SortedTurns().DistinctBy(x => x.Player).ToList();
        return performedTurns;
    }

    public List<Turn> GetTurnsToBePerformed()
    {
        turnsToBePerformed = SortedTurns().Except(performedTurns).ToList();
        return turnsToBePerformed;
    }


    public void PerformedTurn(Turn performedTurn)
    {
        performedTurns.Add(performedTurn);
        turnsToBePerformed.Remove(performedTurn);
    }
    public Turn GetLatestTurns()
    {
        return turnsToBePerformed[0];
    }
    
    public Turn GetNextTurnOfPlayer(CharacterBaseClasses player)
    {
        Turn nextTurnOfPlayer;
        if (GetTurnsToBePerformed().Any(x => x.Player == player))
        {
            nextTurnOfPlayer = GetTurnsToBePerformed().Where(x => x.Player == player).ToList()[0];
            //performedTurns.Add(nextTurnOfPlayer);
            return nextTurnOfPlayer;
        }
        return null;
    }



    public Turn GetLatestTurnWithCharacter(CharacterBaseClasses character)
    {

        return allTurns?.LastOrDefault(turn => turn.Player == character) ?? null;
    }
    public List<Turn> GetAllTurnOfThePlayer(CharacterBaseClasses player)
    {
        return GetTurnsToBePerformed().Where(x => x.Player == player).ToList();
    }

    public List<CharacterBaseClasses> TryClashCalc(List<Turn> clashAbleTurns)
    {
        
        CharacterBaseClasses tempPlayer;
        CharacterBaseClasses tempTarget;
        int pv1;
        int pv2;

        if(clashAbleTurns.Count > 0)
        {
            //eligibleTurnsForClash = clashAbleTurns.Where(x => x.Command.GetActionName() != "move").ToList();
            eligibleTurnsForClash = clashAbleTurns;

            foreach (Turn turn in eligibleTurnsForClash)
            {
                // if two players clashes each other
                if (!turn.IsPerformed)
                {
                    tempPlayer = turn.Player;
                    tempTarget = turn.Command.GetTarget();
                    pv1 = turn.Command.GetPVValue();
                    for (int i = 0; i < eligibleTurnsForClash.Count; i++)
                    {
                        // Debug.LogWarning("HT--player: " + tempPlayer + "-----HT--target: " + tempTarget);
                        if (tempPlayer == eligibleTurnsForClash[i].Command.GetTarget())
                        {
                            // Debug.Log("HT--player: " + tempPlayer);
                            if (tempTarget == eligibleTurnsForClash[i].Player)
                            {
                                // Debug.Log("HT--target: " + tempTarget);
                                pv2 = eligibleTurnsForClash[i].Command.GetPVValue();
                                looserOfClash.Add(clashCalculation.GetPVClashLooser(tempPlayer, pv1, tempTarget, pv2));
                            }
                            else
                            {
                                // Debug.Log("HT--player: " + tempPlayer + "-----HT--target: " + tempTarget);
                                continue;
                            }
                        }
                    }

                }
            }
        }


        //Debug.Log(looserOfClash);
        return looserOfClash;
    }

    public void ResetAllList()
    {
        allTurns.Clear();
        performedTurns.Clear();
        turnsToBePerformed.Clear();
        looserOfClash.Clear();
        eligibleTurnsForClash.Clear();
    }

    public void DeletePlayerTurns(CharacterBaseClasses player)
    {
        allTurns.RemoveAll(x => x.Player == player);
    }

    public void DeleteDeadTargetedTurns(CharacterBaseClasses target)
    {
        allTurns.RemoveAll(x => x.target == target);
    }



    /*public int GetPVofLastTurn(Turn currentTurn)
    {
        int lastTurnPV;
        Turn lastTurnOfCurrentPlayer;
        
        lastTurnOfCurrentPlayer = 
        currentPlayer = 
        lastTurnPV = 
        return lastTurnPV;
    }*/

}


