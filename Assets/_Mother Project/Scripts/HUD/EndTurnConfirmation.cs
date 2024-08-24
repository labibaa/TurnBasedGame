using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTurnConfirmation : MonoBehaviour
{
    private void Start()
    {

        this.GetComponent<Button>().onClick.AddListener(() => TurnManager.instance.EndTurn());

        this.GetComponent<Button>().onClick.AddListener(() => ButtonStackManager.instance.ClearStack());

    }
    //private void OnMouseDown()
    //{
    //    Debug.Log("clickk");
    //    TurnManager.instance.EndTurn();

    //    ButtonStackManager.instance.ClearStack();
    //}
}
