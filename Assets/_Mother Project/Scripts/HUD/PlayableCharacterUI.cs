using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayableCharacterUI : MonoBehaviour
{
    //public TMP_Text nameText;
    public TMP_Text apText;
    public TMP_Text hpText;
    public TMP_Text classText;
    public Image avatarImage;
    public GameObject CurrentPlayerPanel;

    public CharacterBaseClasses myCharacter;

    private void Start()
    {
          
    }


    public void UpdateHUD()
    {
        // Update the UI elements with player's stats
        //nameText.text = myCharacter.name;
        apText.text = "AP: " + myCharacter.GetComponent<TemporaryStats>().CurrentAP.ToString();
        hpText.text = "HP: " + myCharacter.GetComponent<TemporaryStats>().CurrentHealth.ToString();
        //classText.text = "Class: " + myCharacter.characterClass;
        avatarImage.sprite = myCharacter.avatarHead;
        CurrentPlayerHUD();

    }

    public void CurrentPlayerHUD()
    {
       // Debug.Log("CUrrent Turn"+ myCharacter.GetComponent<PlayerTurn>().myTurn);
        if (myCharacter.GetComponent<PlayerTurn>().myTurn)
        {
            CurrentPlayerPanel.SetActive(true);
        }
        else
        {
            CurrentPlayerPanel.SetActive(false);
        }
    }
}
