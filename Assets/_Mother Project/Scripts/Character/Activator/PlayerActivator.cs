using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActivator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivatePlayer()
    {
         GetComponent<ThirdPersonController>().DisableAnim();
         GetComponent<ThirdPersonController>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
    }
    public void DeActivatePlayer()
    {
       
        gameObject.GetComponent<ThirdPersonController>().enabled = true;
    }
}
