using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SecondUltimate", menuName = "ScriptableObjects/UltimateActions/SecondUltimate")]
public class Ultimate2Factory : UltimateActionsFactory
{
    public override IUltimate CreateUltimate()
    {
        return new Ultimate2Command();
    }

    public override bool IsUltimateEnabled()
    {
        throw new System.NotImplementedException();
    }

    
}
