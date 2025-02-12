using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class PlayableCharacterUI : MonoBehaviour
{
    public TMP_Text classText;
    public Image avatarImage;
    public GameObject CurrentPlayerPanel;

    public Image hpBar; // HP bar using fillAmount
    public GameObject[] APImages; // Array to hold AP images

    public CharacterBaseClasses myCharacter;

    private void Start()
    {
        UpdateHUD();
    }

    public void UpdateHUD()
    {
        // Update HP bar
        float currentHP = myCharacter.GetComponent<TemporaryStats>().CurrentHealth;
        float maxHP = myCharacter.GetComponent<TemporaryStats>().PlayerHealth;
        hpBar.fillAmount = currentHP / maxHP;

        // Update AP visibility
        UpdateAPImages();

        // Update avatar image
        avatarImage.sprite = myCharacter.avatarHead;
    }

    private void UpdateAPImages()
    {
        int currentAP = myCharacter.GetComponent<TemporaryStats>().CurrentAP;

        for (int i = 0; i < APImages.Length; i++)
        {
            APImages[i].SetActive(i < currentAP);
        }
    }


    public void CurrentPlayerHUD()
    {
        CurrentPlayerPanel.SetActive(myCharacter.GetComponent<PlayerTurn>().myTurn);
    }
}
