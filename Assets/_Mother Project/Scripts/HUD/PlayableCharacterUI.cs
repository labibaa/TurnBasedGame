using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayableCharacterUI : MonoBehaviour
{
    //public TMP_Text nameText;
    //public TMP_Text apText;
    //public TMP_Text hpText;
    public TMP_Text classText;
    public Image avatarImage;
    public GameObject CurrentPlayerPanel;

    public Image hpBar; // HP bar using fillAmount
    public Image apBar; // AP bar using fillAmount

    public CharacterBaseClasses myCharacter;

    private void Start()
    {
        UpdateHUD();
    }

    public void UpdateHUD()
    {
        // Update the UI elements with player's stats
        //nameText.text = myCharacter.name;

        // Update HP bar
        float currentHP = myCharacter.GetComponent<TemporaryStats>().CurrentHealth;
        float maxHP = myCharacter.GetComponent<TemporaryStats>().PlayerHealth; // Assuming you have max HP
        hpBar.fillAmount = currentHP / maxHP; // FillAmount expects a value between 0 and 1

        // Update AP bar
        float currentAP = myCharacter.GetComponent<TemporaryStats>().CurrentAP;
        float maxAP = 12; // Assuming you have max AP
        apBar.fillAmount = currentAP / maxAP;

        // Update avatar image
        avatarImage.sprite = myCharacter.avatarHead;

        // Show if it's the current player's turn
        //CurrentPlayerHUD();
    }

    public void CurrentPlayerHUD()
    {
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
