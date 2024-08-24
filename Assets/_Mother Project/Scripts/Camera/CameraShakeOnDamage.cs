using UnityEngine;
using Cinemachine;

public class CameraShakeOnDamage : MonoBehaviour
{
    public static CameraShakeOnDamage Instance;
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    public float shakeDuration = 0.3f;
    public float shakeAmplitude = 1.2f;
    public float shakeFrequency = 2.0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        if (virtualCamera==null)
        {
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
        }
    }

    public void ShakeCameraOnDamage()
    {
        if (virtualCamera != null)
        {
            CinemachineBasicMultiChannelPerlin noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            if (noise != null)
            {
                noise.m_AmplitudeGain = shakeAmplitude;
                noise.m_FrequencyGain = shakeFrequency;
                //Invoke("StopCameraShake", shakeDuration);
            }
        }
    }

    public void StopCameraShake()
    {
        if (virtualCamera != null)
        {
            CinemachineBasicMultiChannelPerlin noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            if (noise != null)
            {
                noise.m_AmplitudeGain = 0f;
                noise.m_FrequencyGain = 0f;
            }
        }
    }
}
