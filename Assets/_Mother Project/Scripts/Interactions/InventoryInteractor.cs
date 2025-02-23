using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryInteractor : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject InventoryHolder;
    public void Interact()
    {
        ActivateInventory();
    }

    public void ActivateInventory()
    {
        foreach (var player in SwitchMC.Instance.characters)
        {
            player.GetComponent<ThirdPersonController>().enabled = false;
        }
        Cursor.lockState = CursorLockMode.None;
        InventoryHolder.SetActive(true);
    }

}
