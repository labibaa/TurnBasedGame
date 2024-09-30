using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterStats_UI : MonoBehaviour
{
    [SerializeField] GameObject StatsPanel;
    [SerializeField] TextMeshProUGUI hp_txt;
    [SerializeField] TextMeshProUGUI dex_txt;
    [SerializeField] TextMeshProUGUI end_txt;
    [SerializeField] TextMeshProUGUI str_txt;
    [SerializeField] TextMeshProUGUI int_txt;
    [SerializeField] TextMeshProUGUI arc_txt;
    void Update()
    {
        
    }

    public void CharacterStatsUI()
    {
        GameObject crntPlayer =  TempManager.instance.attacker.gameObject;
        StatsPanel.SetActive(true);
        hp_txt.text = crntPlayer.GetComponent<TemporaryStats>().CurrentHealth.ToString();
        dex_txt.text = crntPlayer.GetComponent<TemporaryStats>().CurrentDex.ToString();
        end_txt.text = crntPlayer.GetComponent<TemporaryStats>().CurrentEndurance.ToString();
        str_txt.text = crntPlayer.GetComponent<TemporaryStats>().CurrentStrength.ToString();
        int_txt.text = crntPlayer.GetComponent<TemporaryStats>().CurrentIntelligence.ToString();
        arc_txt.text = crntPlayer.GetComponent<TemporaryStats>().CurrentArcana.ToString();
    }
}
