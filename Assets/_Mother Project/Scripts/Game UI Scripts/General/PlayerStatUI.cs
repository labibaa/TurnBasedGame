using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Relay.Models;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatUI : MonoBehaviour
{
    #region summary HUD
    [SerializeField] private Image playerStatSummaryPanel;
    [SerializeField] private Image playerAvatarSummary;
    [SerializeField] private TMP_Text playerNameTextSummary;
    [SerializeField] private TMP_Text playerAPTextSummary;
    [SerializeField] private TMP_Text playerHPTextSummary;
    [SerializeField] private TMP_Text playerRPTextSummary;

    #endregion

    #region new Summary Stat

    public Image SummaryStatParent;
    public Image SummaryStatParentEnemy;
    public Image AvatarSummaryPrefab;
    public Image AvatarSummaryPrefabEnemy;
    public List<PlayableCharacterUI> CharacterUIList;

    #endregion


    [SerializeField] private Image playerStatDetailsPanel;
    [SerializeField] private Image playerAvatarDetails;
    //[SerializeField] private TMP_Text playerNameTextDetails;
    //[SerializeField] private TMP_Text playerClassTextDetails;
    [SerializeField] private TMP_Text playerAPTextDetails;
    [SerializeField] private TMP_Text playerHPTextDetails;
    //[SerializeField] private TMP_Text playerRPTextDetails;
    //[SerializeField] private TMP_Text playerArcanaTextDetails;
    //[SerializeField] private TMP_Text playerCharismaTextDetails;
    //[SerializeField] private TMP_Text playerMindTextDetails;
    //[SerializeField] private TMP_Text playerEnduranceTextDetails;
    //[SerializeField] private TMP_Text playerSkillTextDetails;
    //[SerializeField] private TMP_Text playerStrengthTextDetails;
    //[SerializeField] private TMP_Text playerDexterityTextDetails;

    public static PlayerStatUI instance;

    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
    }

    #region summary HUD

    public void GetPlayerStatSummary(CharacterBaseClasses currentPlayer)
    {
        playerAvatarSummary.sprite = currentPlayer.avatarHead;
        playerNameTextSummary.text = currentPlayer.characterName;
        playerAPTextSummary.text = "AP: " + currentPlayer.GetComponent<TemporaryStats>().CurrentAP.ToString();
        playerHPTextSummary.text = "HP: " + currentPlayer.GetComponent<TemporaryStats>().CurrentHealth.ToString();
        playerRPTextSummary.text = "RP: " + currentPlayer.GetComponent<TemporaryStats>().CurrentResolve.ToString();
    }

    #endregion

    public void CreateSummaryList()
    {

        foreach (Transform child in SummaryStatParent.transform)
        {
            Destroy(child.gameObject);
        }


        HashSet<CharacterBaseClasses> uiPlayerHashSet = new HashSet<CharacterBaseClasses>();
        
        foreach (PlayerTurn pt in TurnManager.instance.players)
        {
            
                uiPlayerHashSet.Add(pt.GetComponent<CharacterBaseClasses>());
            
            
        }

        foreach (CharacterBaseClasses player in uiPlayerHashSet)
        {
            
                Transform tempAvatarUI;
            if (player.GetComponent<TemporaryStats>().CharacterTeam == TeamName.TeamA)
            {
                tempAvatarUI = Instantiate(AvatarSummaryPrefab.transform, SummaryStatParent.transform);
                tempAvatarUI.GetComponent<PlayableCharacterUI>().myCharacter = player;
                CharacterUIList.Add(tempAvatarUI.GetComponent<PlayableCharacterUI>());
            }
            else
            {
                tempAvatarUI = Instantiate(AvatarSummaryPrefabEnemy.transform, SummaryStatParentEnemy.transform);
                tempAvatarUI.GetComponent<PlayableCharacterUI>().myCharacter = player;
                CharacterUIList.Add(tempAvatarUI.GetComponent<PlayableCharacterUI>());
            }
                
                
            
            
        }
        UpdateSummaryHUDUI();
    }

    public void UpdateSummaryHUDUI()
    {
        foreach (PlayableCharacterUI summaryHUD in CharacterUIList)
        {
            summaryHUD.UpdateHUD();
        }
    }

    public void GetPlayerStatDetails(CharacterBaseClasses currentPlayer)
    {
        

        playerAvatarDetails.sprite = currentPlayer.avatarHead;
        //playerNameTextDetails.text = currentPlayer.characterName;
        //playerClassTextDetails.text = currentPlayer.;

        playerAPTextDetails.text = "AP: " + currentPlayer.GetComponent<TemporaryStats>().CurrentAP.ToString();
        playerHPTextDetails.text = "HP: " + currentPlayer.GetComponent<TemporaryStats>().CurrentHealth.ToString();
        //playerRPTextDetails.text = "RP: " + currentPlayer.GetComponent<TemporaryStats>().CurrentResolve.ToString();

        //playerArcanaTextDetails.text = "Arcana: " + currentPlayer.Arcana.ToString();
        //playerCharismaTextDetails.text = "Charisma: " + currentPlayer.Charisma.ToString();
        //playerMindTextDetails.text = "Mind: " + currentPlayer.Mind.ToString();
        //playerEnduranceTextDetails.text = "Endurance: " + currentPlayer.Endurance.ToString();
        //playerSkillTextDetails.text = "Skill: " + currentPlayer.Skill.ToString();
        //playerStrengthTextDetails.text = "Strength: " + currentPlayer.Strength.ToString();
        //playerDexterityTextDetails.text = "Dexterity: " + currentPlayer.Dexterity.ToString();

    }
}
