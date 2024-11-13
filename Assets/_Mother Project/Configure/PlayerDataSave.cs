using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerDataSave
{
    public string Name;
    public int CurrentPlayerHealth;
    public int PlayerAP;
    public int CurrentDex;
    public int CurrentEndurance;
    public int CurrentStrength;
    public int CurrentArcana;
    public int CurrentIntelligence;
    public int CurrentExp;
    public TeamName CharacterTeam;

    public PlayerDataSave(string name,int playerHealth, int playerAP, int currentDex, int currentEndurance, int currentStrength, int currentArcana, int currentIntelligence, int exp ,TeamName teamName)
    {
        Name = name;
        CurrentPlayerHealth = playerHealth;
        PlayerAP = playerAP;
        CurrentDex = currentDex;
        CurrentEndurance = currentEndurance;
        CurrentStrength = currentStrength;
        CurrentArcana = currentArcana;
        CurrentIntelligence = currentIntelligence;
        CurrentExp = exp;
        CharacterTeam = teamName;
        
    }
}
