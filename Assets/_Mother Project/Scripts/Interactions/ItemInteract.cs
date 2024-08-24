using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemInteract : MonoBehaviour, IInteractable
{
    public void itemInterracted()
    {
        Debug.Log("Box says bye");
        SceneManager.LoadScene(1);
    }

    public void Interact()
    {
        itemInterracted();

    }
}
