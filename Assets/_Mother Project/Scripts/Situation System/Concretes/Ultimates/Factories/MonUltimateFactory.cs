using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MonUltimate", menuName = "ScriptableObjects/UltimateActions/MonUltimate")]
public class MonUltimateFactory :UltimateActionsFactory
{
    
    public override IUltimate CreateUltimate()
    {
        
        return new MonUltimateCommand(this);
    }

    public override bool IsUltimateEnabled()
    {
        throw new System.NotImplementedException();
    }
}
