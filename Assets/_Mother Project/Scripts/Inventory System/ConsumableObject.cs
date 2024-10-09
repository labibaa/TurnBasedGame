using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConsumableObject Inventory", menuName = "ScriptableObjects/Counsumable")]
public class ConsumableObject : ItemClass
{
    public int ObjectBuff;
    public override ConsumableObject GetConsumableObject()
    {
        return this;
    }

    public override ItemClass GetItem()
    {
        return this;
    }

    public override ToolObject GetToolObject()
    {
        return null;
    }
    public override void UseObject(TemporaryStats player)
    {
        player.CurrentHealth = HealthManager.instance.HealthCap(player.PlayerHealth, HealthManager.instance.HealthCalculation(-ObjectBuff, player.CurrentHealth));
        Debug.Log(ObjectBuff + " is Healed");
    }
}
