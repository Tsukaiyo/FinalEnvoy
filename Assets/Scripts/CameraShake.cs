using Unity.Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }
    CinemachineCamera cinemachineCamera;
    float shakerTimer;
    float shakerTimerTotal;
    float startingIntensity;

    void Awake()
    {
        Instance = this;
        cinemachineCamera = GetComponent<CinemachineCamera>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            cinemachineCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();

        if (cinemachineBasicMultiChannelPerlin != null)
        {
            cinemachineBasicMultiChannelPerlin.AmplitudeGain = intensity;
        }

        startingIntensity = intensity;
        shakerTimerTotal = time;
        shakerTimer = time;
    }

    private void Update()
    {
        if (shakerTimer > 0)
        {
            shakerTimer -= Time.deltaTime;
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                cinemachineCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();

            if (cinemachineBasicMultiChannelPerlin != null)
            {
                cinemachineBasicMultiChannelPerlin.AmplitudeGain =
                    Mathf.Lerp(startingIntensity, 0f, 1f - shakerTimer / shakerTimerTotal);
            }
        }
    }
}