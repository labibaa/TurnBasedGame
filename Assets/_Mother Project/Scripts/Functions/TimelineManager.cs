using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System;
using Cinemachine;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine.UI;

public class TimelineManager : MonoBehaviour
{

    public static event Action<Turn> OnActionExecution;

    public GameObject Bar;
    private Tweener barTween;

    public PlayerHUD playerHud;

   
    public float TotalTime;
    public float StartTime;
    public Vector3 InitialScale;
    public Vector3 TargetScale;
    [SerializeField]
    int timeMultiplier = 1;
    


  //  public float elapsedTime;
    Vector3 currentScale;

    public int i = 0;
    //float[] arr = {5,10, 30, 50, 60};

    public List<Turn> turnsForTimeline;
    public List<Turn> turnsPerforming;

    public bool IsPaused = false;
    public int timer;

   // public HandleTurn turnsToHandle;

    bool isPlaying = false;
    
    [SerializeField]TurnManager turnManager;

    private void OnEnable()
    {
        timer = 0;
        turnsForTimeline = HandleTurn.instance.SortedTurns();
        TotalTime = turnsForTimeline[turnsForTimeline.Count - 1].PriorityValue / timeMultiplier;

        turnsPerforming = HandleTurn.instance.GetFirstTurns();


        TimelineBarAnimation(TotalTime);

        for (int j = 0; j < turnsPerforming.Count; j++)
        {
            // Debug.Log(turnsPerforming[j].Player + "Started Action At 0 With priority Value of" + turnsPerforming[j].PriorityValue );

            //PopTimelineTxt.gameObject.SetActive(true);
            //PopTimelineTxt.text = turnsPerforming[j].Player + "Started Action At 0 With priority Value of" + turnsPerforming[j].PriorityValue;
            if (turnsPerforming[j].Command.GetActionName() == "Move")
            {
                GetTurn(turnsPerforming[j], j);
            }

        }

        Invoke("CountDown", 1f);
    }

    void Start()
    {
       


    }

    //private  void Update()
    //{

    //    elapsedTime = Time.time - StartTime;

    //    if (elapsedTime<TotalTime )
    //    {


    //        //if (i < turnsStartingAtZero.Count)
    //        //{
    //        //    CheckListValue(turnsStartingAtZero[i],i);
    //        //    // pvOfTurns[i] for starting time
    //        //}

    //        ClashPerform();



    //        for (int k = 0; k < turnsStartingAtZero.Count; k++)
    //        {

    //             CheckListValue(turnsStartingAtZero[k], k);
    //           // await Task.Delay(3000);


    //        }

    //        // Interpolate the scale based on the elapsed time
    //        float t = Mathf.Clamp01(elapsedTime / TotalTime);
    //        currentScale = Vector3.Lerp(InitialScale, TargetScale, t);


    //    }


    //    // Use the currentScale as desired
    //  //  Debug.Log("Current Scale: " + currentScale);
    //}


    private void CountDown()
    {
        timer++;


        if (timer <= TotalTime)
        {
            for (int k = 0; k < turnsPerforming.Count; k++)
            {
                StartCoroutine(PerformAction());

            }
            // Interpolate the scale based on the elapsed time
            float t = Mathf.Clamp01(timer / TotalTime);
            currentScale = Vector3.Lerp(InitialScale, TargetScale, t);

            Invoke("CountDown", 1f);

        }
        else
        {
            RoundEnd();
            Debug.Log("Round End");
        }


    }

    IEnumerator PerformAction()
    {

        int PvOfCurrentAction ;
      
    

        for (int k = 0; k < turnsPerforming.Count; k++)
        {
            //Debug.Log("Performing Action");
            PvOfCurrentAction = turnsPerforming[k].PriorityValue / timeMultiplier;

            if (Mathf.Abs(PvOfCurrentAction - timer) < 1)
            {
               // Debug.LogWarning(turnsPerforming[k].Player + "is " + turnsPerforming[k].Command.GetActionName() + turnsPerforming[k].target + "At " + timer);

                if (turnsPerforming[k].Command.GetTarget())
                {
                    ClashPerform();
                }
                if (turnsPerforming[k].Command.GetActionName()!="Move")
                {
                    GetTurn(turnsPerforming[k], k);
                }
                

                yield return null;



            }
        }

    }
    public void TimelineBarAnimation(float time)
    {
        float currentTime = Time.time;

        barTween = Bar.transform.DOScaleX(1f, time).SetEase(Ease.Linear);
    }
     

    public void GetTurn(Turn currentTurn,int k)
    {
       

        //Debug.Log(HandleTurn.instance.GetAllTurnOfThePlayer(currentTurn.Player).Count)
        if (currentTurn!=null)
        {

            ActionExecution(currentTurn);
           
        }
       

        Turn nextTurn = HandleTurn.instance.GetNextTurnOfPlayer(currentTurn.Player);
        //Debug.LogWarning(nextTurn.PriorityValue);

        if (nextTurn != null)
        {
            if (nextTurn.Command.GetActionName() == "Move")
            {
                ActionExecution(nextTurn);

                //Turn nextTurnofMove = HandleTurn.instance.GetNextTurnOfPlayer(currentTurn.Player);
                if (nextTurn != null)
                {
                   

                    turnsPerforming[k] = nextTurn;
                }


            }
            else
            {
                int tempTurn = nextTurn.PriorityValue;

                turnsPerforming[k] = nextTurn;

                //PopTimelineTxt.gameObject.SetActive(true);
                //PopTimelineTxt.text = currentTurn.Player + " starting action at " + currentTurn.PriorityValue + " finished at " + nextTurn.PriorityValue;

            }


            //  Debug.Log(currentTurn.Player + " starting action at " + currentTurn.PriorityValue + " finished at " + nextTurn.PriorityValue);


        }

        else
        {
          
           
        }


    }

    public void ActionExecution(Turn turn)
    {
        if (!turn.IsPerformed)
        {
           // CutsceneManager.instance.PlayCinemachine(turn.Player.gameObject);  // move camera before playing animation
           // Debug.Log(turn.Player + "is " + turn.Command.GetActionName() + "ing " + turn.Command.GetTarget());

            turn.Command.Execute();
            
            turn.IsPerformed = true;
            OnActionExecution?.Invoke(turn);
            if (turn.Command.GetActionName()!= "Move")
            {

                PauseTimeline(turn.Player.gameObject, turn.Command.GetActionName());                          // TO PAUSE TIMELINE for playing animation
                StartCoroutine(ResumeAfterDelay(5f));

            }
            
            HandleTurn.instance.PerformedTurn(turn);
           
        }
    }


    public void ClashPerform()
    {
        if (turnsPerforming.Count != turnsPerforming.DistinctBy(x => x.PriorityValue).ToList().Count)
        {
            // send a list of similar turns to handle turns TryClashCalc function and get the loser then delete all the loser ,Remove from current list



            List<Turn> clashedTurns = new List<Turn>();
            List<CharacterBaseClasses> losersOfClash = new List<CharacterBaseClasses>();

            clashedTurns = turnsPerforming.GroupBy(turn => turn.PriorityValue)
                                       .Where(group => group.Count() > 1)
                                       .SelectMany(group => group)
                                       .ToList();

            //foreach (Turn turn in clashedTurns)
            //{
            //    Debug.Log(turn.PriorityValue);
            //}



            losersOfClash = HandleTurn.instance.TryClashCalc(clashedTurns);
            List<Turn> toBeDeleted = new List<Turn>();
            if (losersOfClash.Count > 0)
            {
                foreach (Turn turn in turnsPerforming)
                {
                    foreach (CharacterBaseClasses looser in losersOfClash)
                    {
                        if (turn.Player == looser)
                        {
                            toBeDeleted.Add(turn);
                      //      turnsPerforming.Remove(turn); //removed all moves from the loser


                            //clashedTurns.Remove(turn);
                        }

                    }
                }

                turnsPerforming=turnsPerforming.Except(toBeDeleted).ToList();

            }
            else
            {
              //  Debug.Log("Have to make it sequential");
                foreach (Turn turn in clashedTurns)
                {
                    //Debug.LogWarning(turn.Player+"is "+turn.Command.GetActionName()+"ing "+ turn.Command.GetTarget());
                }
            }
            foreach(Turn seqTurns in clashedTurns)
            {
                //seqTurns.PriorityValue++;
            }

        }


    }



    public void PauseTimeline(GameObject character,String animName)
    {
        //IsPaused = true;

        Time.timeScale = 0f;
        CutsceneManager.instance.PlayAnimationForCharacter(character, animName);


    }

    public void ResumeTimeline()
    {
        // IsPaused = false;
        Time.timeScale = 1f;
       // CutsceneManager.instance.PlayCameraPriorityReset();
    }

    IEnumerator ResumeAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay); 
        ResumeTimeline();

    }

 
    public void RoundEnd()
    {
       
        ResetTimeline();
        turnManager.ResetTurn();
        TempManager.instance.ChangeGameState(GameStates.StartTurn);
       

    }

    public void ResetTimeline()
    {
        //timer = 0;
        //timeline bar reset

        //bar.transform.DOScale(0f, 0f);
        barTween.Kill();
        Bar.transform.DOScaleX(0f, 0f);
        //Bar.transform.localScale = Vector3.one;
       HandleTurn.instance.ResetAllList();


    }


}

