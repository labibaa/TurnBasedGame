using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats_UI : MonoBehaviour
{
    [SerializeField] GameObject StatsPanel;
    [SerializeField] TextMeshProUGUI hp_txt;
    [SerializeField] TextMeshProUGUI dex_txt;
    [SerializeField] TextMeshProUGUI end_txt;
    [SerializeField] TextMeshProUGUI str_txt;
    [SerializeField] TextMeshProUGUI int_txt;
    [SerializeField] TextMeshProUGUI arc_txt;
    [SerializeField] TextMeshProUGUI characterName_txt;
    [SerializeField] TextMeshProUGUI currentXP_txt;
    [SerializeField] GameObject xpNotification;

    void Update()
    {
        
    }

    public void CharacterStatsUI()
    {
        GameObject crntPlayer =  TempManager.instance.attacker.gameObject;
        StatsPanel.SetActive(true);
        characterName_txt.text = crntPlayer.GetComponent<CharacterBaseClasses>().characterName.ToString();
        currentXP_txt.text = crntPlayer.GetComponent<TemporaryStats>().CurrentExp.ToString();
        hp_txt.text = crntPlayer.GetComponent<TemporaryStats>().PlayerHealth.ToString();
        dex_txt.text = crntPlayer.GetComponent<TemporaryStats>().CurrentDex.ToString();
        end_txt.text = crntPlayer.GetComponent<TemporaryStats>().CurrentEndurance.ToString();
        str_txt.text = crntPlayer.GetComponent<TemporaryStats>().CurrentStrength.ToString();
        int_txt.text = crntPlayer.GetComponent<TemporaryStats>().CurrentIntelligence.ToString();
        arc_txt.text = crntPlayer.GetComponent<TemporaryStats>().CurrentArcana.ToString();
    }

    public void LevelUpCharacter()
    {
        GameObject crntPlayer = TempManager.instance.attacker.gameObject;
        if (crntPlayer.GetComponent<TemporaryStats>().CurrentExp >= crntPlayer.GetComponent<CharacterBaseClasses>().MaxExperiencePoint)
        {
            crntPlayer.GetComponent<TemporaryStats>().CurrentExp -= crntPlayer.GetComponent<CharacterBaseClasses>().MaxExperiencePoint;
            crntPlayer.GetComponent<CharacterBaseClasses>().LevelUp();
            crntPlayer.GetComponent<TemporaryStats>().SetCharacterStat();
            CharacterStatsUI();
        }
        else
        {
            xpNotification.SetActive(true);
        }
    }
}
