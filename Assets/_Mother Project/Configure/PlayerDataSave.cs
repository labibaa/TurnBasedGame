using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerDataSave
{
    public string Name;
    public int CurrentHealth;
    public int PlayerHealth;
    public int CurrentAP;
    public int PlayerAP;
    public int CurrentDex;
    public TeamName CharacterTeam;

    public PlayerDataSave(string name, int currentHealth,int playerHealth,int currentAP, int playerAP,int currentDex, TeamName teamName)
    {
        Name = name;
        CurrentHealth = currentHealth;
        PlayerHealth = playerHealth;
        CurrentAP = currentAP;
        PlayerAP = playerAP;
        CurrentDex = currentDex;
        CharacterTeam = teamName;

    }
}
