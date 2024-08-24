using UnityEngine;
using System;
using TMPro;
using System.Collections.Generic;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyTrait
    {
        Attack,
        Defense,
        Normal
    }
    
    public float health;
    public EnemyTrait trait;

    public GameObject enemyCanvas;
    public TextMeshProUGUI enemyText;

    public int minHealthToAttack;
    public int maxHealthToDefense;

    public GameObject target;
    private int minAPForAction = 2;

    private List<ImprovedActionStat> enemyAvailableActions = new List<ImprovedActionStat>();
    private List<ImprovedActionStat> enemyAffordableActions = new List<ImprovedActionStat>();
    private List<ImprovedActionStat> enemyAffordableAttackActions = new List<ImprovedActionStat>();
    private List<ImprovedActionStat> enemyAffordableDefenseActions = new List<ImprovedActionStat>();

    private void Start()
    {
        enemyCanvas.SetActive(false);
        // Example usage:
       // health = UnityEngine.Random.Range(0.1f, 1f); // Random health between 10% to 100%
        //trait = (EnemyTrait)UnityEngine.Random.Range(0, Enum.GetValues(typeof(EnemyTrait)).Length); // Random trait
        //SelectAction();
    }

    public void SelectAction()
    {
        health = GetComponent<TemporaryStats>().CurrentHealth;
        enemyCanvas.SetActive(true);
        enemyText.text = gameObject.name + " is selecting action";


        GetAvailableActions();
        /// get a list of affordable action against ap
        //PerformEnemyAction(enemyAvailableActions[UnityEngine.Random.Range(0, enemyAvailableActions.Count)]);


        // whatever the trait is if possible kill target
        if (enemyAffordableActions.Count > minAPForAction)
        {
            foreach (ImprovedActionStat action in enemyAffordableAttackActions)
            {
                if (target.GetComponent<TemporaryStats>().CurrentHealth <= action.RangeMappings[0].MappedValue)
                {
                    EnemyAIAttackMove();
                    PerformEnemyAction(action);
                    
                }
            }

            switch (trait)
            {
                case EnemyTrait.Attack:
                    TryAttackAction();
                    break;
                case EnemyTrait.Defense:
                    TryDefenseAction();
                    break;
                case EnemyTrait.Normal:
                    NormalAction();
                    break;
                default:
                    break;
            }
        }
    }

    private void TryAttackAction()
    {
        if (health > minHealthToAttack)
        {
            if(enemyAffordableActions.Count != 0)
            {
                if (enemyAffordableAttackActions.Count > 0)
                {
                    PerformAttackAction(enemyAffordableAttackActions[UnityEngine.Random.Range(0, enemyAffordableAttackActions.Count)]);
                    Debug.Log("Enemy is attacking");

                }
                else
                {
                    PerformDefenseAction(enemyAffordableDefenseActions[UnityEngine.Random.Range(0, enemyAffordableDefenseActions.Count)]);
                }
            }
           
            // Perform attack action
            //
        }
        else
        {
            Debug.Log("Enemy is moving");
            // Perform move action
            if (enemyAffordableActions.Count != 0)
            {
                if (enemyAffordableDefenseActions.Count > 0)
                {
                    PerformDefenseAction(enemyAffordableDefenseActions[UnityEngine.Random.Range(0, enemyAffordableDefenseActions.Count)]);
                    return;
                }
                else
                {
                    PerformAttackAction(enemyAffordableAttackActions[UnityEngine.Random.Range(0, enemyAffordableAttackActions.Count)]);
                }
            }
         
        }


    }

    private void PerformAttackAction(ImprovedActionStat actionToPerform)
    {
        EnemyAIAttackMove();
        PerformEnemyAction(actionToPerform);
       // EnemyAIDefensiveMove();

    }

    private void TryDefenseAction()
    {
        //if(target.GetComponent<TemporaryStats>().CurrentHealth)
        if (health < maxHealthToDefense)
        {
            // 60% chance to defend
            if (UnityEngine.Random.value < 0.6f)
            {
                
                if (enemyAffordableActions.Count != 0)
                {
                    Debug.Log("Enemy is defending");
                    if (enemyAffordableDefenseActions.Count > 0)
                    {
                        PerformDefenseAction(enemyAffordableDefenseActions[UnityEngine.Random.Range(0, enemyAffordableDefenseActions.Count)]);
                        return;
                    }
                    else
                    {
                        PerformAttackAction(enemyAffordableAttackActions[UnityEngine.Random.Range(0, enemyAffordableAttackActions.Count)]);
                    }
                }
               
                // Perform defense action
            }
            else
            {

                if (enemyAffordableActions.Count != 0)
                {
                    Debug.Log("Enemy is attacking");

                    if (enemyAffordableActions.Count != 0)
                    {
                        if (enemyAffordableAttackActions.Count > 0)
                        {
                            PerformAttackAction(enemyAffordableAttackActions[UnityEngine.Random.Range(0, enemyAffordableAttackActions.Count)]);
                            Debug.Log("Enemy is attacking");

                        }
                        else
                        {
                            PerformDefenseAction(enemyAffordableDefenseActions[UnityEngine.Random.Range(0, enemyAffordableDefenseActions.Count)]);
                        }
                    }

                }

                // Perform attack action
            }
        }
        else
        {
            if (enemyAffordableActions.Count != 0)
            {
                if (enemyAffordableAttackActions.Count > 0)
                {
                    PerformAttackAction(enemyAffordableAttackActions[UnityEngine.Random.Range(0, enemyAffordableAttackActions.Count)]);
                    Debug.Log("Enemy is attacking");

                }
                else
                {
                    PerformEnemyAction(enemyAffordableActions[UnityEngine.Random.Range(0, enemyAffordableActions.Count)]);
                }
            }

          
            // Perform move action
        }


    }

    private void PerformDefenseAction(ImprovedActionStat actionToPerform)
    {

        EnemyAIDefensiveMove();
        PerformEnemyAction(actionToPerform);
    }

    private void NormalAction()
    {
        if (health > 0.3f && health < 0.8f)
        {
            // 50% chance to attack, 50% chance to defend
            if (UnityEngine.Random.value < 0.5f)
            {
                Debug.Log("Enemy is attacking");
                // Perform attack action
            }
            else
            {
                Debug.Log("Enemy is defending");
                // Perform defense action
            }
        }
        else
        {
            Debug.Log("Enemy is moving");
            // Perform move action
        }


    }



    // need to refactor
    public void PerformEnemyAction(ImprovedActionStat actionToPerform)
    {
        if (actionToPerform.ActionName == "Sword Slash")
        {
            ActionArchive.instance.SwordSlash();
        }
        else if(actionToPerform.ActionName == "Push Back")
        {
            ActionArchive.instance.PushBack();
        }

        if (actionToPerform.ActionName == "Block")
        {
            ActionArchive.instance.Block();
        }
        else if (actionToPerform.ActionName == "Counter")
        {
            ActionArchive.instance.Counter();
        }

        GetAvailableActions();
        if (enemyAffordableActions.Count > minAPForAction)
        {
            SelectAction();
        }
    }

    public void EndEnemyAction()
    {

        enemyCanvas.SetActive(false);
    }


    public void EnemyAIAttackMove()
    {
        List<GameObject> targetGrid = new List<GameObject>();
        List<GameObject> tempTargetGrid = new List<GameObject>();
        Vector2 targetGridCoordinate;
        Vector2 myGridCoordinate;
        targetGridCoordinate = AutoGridMovement.instance.CheckClosestAdjacent(transform.position, target.transform.position);
        myGridCoordinate = GridSystem.instance.WorldToGrid(transform.position);
        targetGrid = AutoGridMovement.instance.FindPath(myGridCoordinate, targetGridCoordinate);
        tempTargetGrid.Add(targetGrid[0]);
        tempTargetGrid.Add(targetGrid[targetGrid.Count - 1]);
        ICommand AttackingMove = new Move(tempTargetGrid, GetComponent<NavMeshAgent>(), true, "MeleeMove");
        Turn turn = new Turn(GetComponent<CharacterBaseClasses>(), AttackingMove, 20);
        
        HandleTurnNew.instance.AddTurn(turn);
    }

    public void EnemyAIDefensiveMove()
    {
        List<GameObject> targetGrid = new List<GameObject>();
        Vector2 myGridCoordinate;
        myGridCoordinate = GridSystem.instance.WorldToGrid(transform.position);
        targetGrid.Add(GridSystem.instance._gridArray[(int)myGridCoordinate.x, (int)myGridCoordinate.y]);
        targetGrid.Add(AutoGridMovement.instance.GetFarthestGridFromTarget(target.GetComponent<CharacterBaseClasses>()));

        ICommand AttackingMove = new Move(targetGrid, GetComponent<NavMeshAgent>(), true, "MeleeMove");
        Turn turn = new Turn(GetComponent<CharacterBaseClasses>(), AttackingMove, 20);

        HandleTurnNew.instance.AddTurn(turn);
    }

    private void GetAvailableActions()
    {
        enemyAvailableActions = GetComponent<PerformerClass>().GetAvailableActions();
        enemyAffordableActions = ActionArchive.instance.GetActionsWithinAP(enemyAvailableActions, GetComponent<TemporaryStats>());
        enemyAffordableAttackActions = ActionArchive.instance.GetOffenseActions(enemyAffordableActions);
        enemyAffordableDefenseActions = ActionArchive.instance.GetDefenceActions(enemyAffordableActions);
    }
}

