using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TargetButton : MonoBehaviour
{
    public Image avatarHead;
    public TMP_Text avatarNameTxt;
    public CharacterBaseClasses target;


    // Start is called before the first frame update
    void Start()
    {

    }

    public void ButtonClick()
    {

        TempManager.instance.defender = target.gameObject;
        //ActionArchive.instance.GetPlayerStats();
        TempManager.instance.ChangeGameState(GameStates.MidTurn);
        DictionaryManager.instance.GiveAction(TempManager.instance.actionName);
        
        GridMovement.instance.ResetHighlightedPath();
        TurnManager.instance.ResetTargetHIghlightVisual();
        TurnManager.instance.targetsInRange.Clear();
        TurnManager.instance.nonCharacterTargetsInRange.Clear();
    }



}
