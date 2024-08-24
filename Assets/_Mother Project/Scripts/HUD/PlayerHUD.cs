using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] GameObject player;
    public TMP_Text Health;
    public TMP_Text Strength;
    public TMP_Text Resolve;
    public TMP_Text Endurance;
    public TMP_Text Skill;
    public TMP_Text ActionMesssage;
    CharacterBaseClasses character;
    TemporaryStats tempStats;

    Turn turn;



    private void OnEnable()
    {
       // TimelineManager.OnActionExecution += GetActionMessage;
    }

    private void OnDisable()
    {
      //  TimelineManager.OnActionExecution -= GetActionMessage;
    }


    private void Start()
    {
        character = player.GetComponent<CharacterBaseClasses>();
        tempStats = player.GetComponent<TemporaryStats>();
    }

    private void Update() {

        UpdateHUDStats();
       
    }

    
    public void UpdateHUDStats()
    {
        Strength.text = "Strength is: " + character.Strength.ToString();
        Skill.text = "Skill is: " + character.Skill.ToString();
        Endurance.text = "Endurance is: " + character.Endurance.ToString();
        Health.text = "Health is: " + tempStats.CurrentHealth.ToString();
        Resolve.text = "Resolve is: " + tempStats.CurrentResolve.ToString();
       // ActionMesssage.text = TempManager.instance.attacker + turn.Command.GetActionName() + "ing" + TempManager.instance.defender;

       
    }


    //public void GetActionMessage(Turn turnFromTimeLine)
    //{
    //    if(character == turnFromTimeLine.Player || character == turnFromTimeLine.Command.GetTarget())
    //    {
    //        turn = turnFromTimeLine;
    //        ActionMesssage.text = character.characterName+" is " + turn.Command.GetActionName() + "ing " + turn.Command.GetTarget().characterName;
    //    }

    //}



    //

}
