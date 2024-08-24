using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveTurnInformation 
{
   
    public string PlayerName;
    public int PlayerHP;
    public int PlayerAp;
    public Vector3 PlayerPosition;
    public Vector2 gridPlayerPosition;
    public string TargetName;
    public int TargetHP;
    public int TargetAp;
    public Vector3 TargetPosition;
    public Vector2 gridTargetPosition;
    public string ActionName;
    public int ActionAPCost;
    public string DateTime;


}
