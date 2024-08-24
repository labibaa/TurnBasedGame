using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUltimate 
{
    void setValues(CharacterBaseClasses playerCh, TemporaryStats playerTemp);
    void Execute();
    int GetultimateThreshold();

}
