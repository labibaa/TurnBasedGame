using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory Tool", menuName = "ScriptableObjects/Tool")]
public class ToolObject : ItemClass
{
    public CurrentWeapon weapon;
    public override ConsumableObject GetConsumableObject()
    {
        return null;
    }

    public override ItemClass GetItem()
    {
        return this;
    }

    public override ToolObject GetToolObject()
    {
        return this;
    }
}
