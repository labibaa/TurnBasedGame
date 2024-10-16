using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ground Blast", menuName = "ScriptableObjects/SmokeCloud")]
public class SmokeCloudObject : ItemClass
{
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
        return null;
    }

    public override void UseObject(TemporaryStats player)
    {
        ActionArchive.instance.SmokeCloud();
    }
}
