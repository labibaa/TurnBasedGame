using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPersistableData 
{
    void SaveData(PlayerDataSave playerDataSave);

    void LoadData(PlayerDataSave playerDataSave);

}
