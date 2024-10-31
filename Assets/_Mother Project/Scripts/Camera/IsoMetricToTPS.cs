using UnityEngine;
using Cinemachine;

public class IsoMetricToTPS : MonoBehaviour
{
    public CinemachineVirtualCamera isoCamera;
    public CinemachineVirtualCamera tpsCamera;
    public KeyCode switchKey = KeyCode.I;

    private bool isIsometricActive = true;

    void Start()
    {
        if (isoCamera != null && tpsCamera != null)
        {
            isoCamera.Priority = 15;
            tpsCamera.Priority = 5;
        }
        else
        {
            Debug.LogError("Please assign both the isometric and TPS cameras.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(switchKey))
        {
            isIsometricActive = !isIsometricActive;

            if (isIsometricActive)
            {
                isoCamera.Priority = 15;
                tpsCamera.Priority = 5;
            }
            else
            {
                isoCamera.Priority = 5;
                tpsCamera.Priority = 15;
            }
        }
    }
}
