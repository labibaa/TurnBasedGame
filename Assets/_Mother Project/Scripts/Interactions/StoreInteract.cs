using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreInteract : MonoBehaviour, IInteractable
{
    public void storeInterracted()
    {
        Inventory_UI.instance.RefreshStoreUI();
    }

    public void Interact()
    {
        storeInterracted();
    }
}
