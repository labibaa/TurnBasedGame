using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPromptUI : MonoBehaviour
{
    private Camera MainCamera;
    [SerializeField] GameObject InteractionPopUpUI;

    public bool isDisplayed = false;

    private void Start()
    {
        MainCamera = Camera.main;
        InteractionPopUpUI.SetActive(false);

    }

    private void LateUpdate()
    {
        var rotation = MainCamera.transform.rotation;
        transform.LookAt(transform.position + rotation* Vector3.forward, rotation*Vector3.up);
    }

    public void SetInteractionPopUp()
    {
        InteractionPopUpUI.SetActive(true);
        isDisplayed = true;
    }

    public void closePopUp()
    {
        InteractionPopUpUI.SetActive(false);
        isDisplayed = false;
    }
}
