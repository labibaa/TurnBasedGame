using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreserveRotation : MonoBehaviour
{

    [SerializeField]
    GameObject player;
    [SerializeField]
    int offsetX;
    [SerializeField]
    int offsetY;
    [SerializeField]
    int offsetZ;
    Vector3 offset;

    private void Start()
    {
         offset = new Vector3 (offsetX, offsetY, offsetZ);
    }

    private void LateUpdate()
    {
        if (!GridSystem.instance.IsGridOn)
        {
            transform.position = player.transform.position + offset;

        }
        
    }

}
