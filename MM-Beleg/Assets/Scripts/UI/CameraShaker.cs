using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class CameraShaker : MonoBehaviour
{
    public AudioAnalyzer audioAnalyzer;

    public float ShakeDuration = 0.2f;    
    public float ShakeAmplitude = 3f;     
    public float ShakeFrequency = 100f;  

    private float ShakeElapsedTime = 0f;

    // Cinemachine Shake
    public CinemachineVirtualCamera VirtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    // Use this for initialization
    void Start()
    {
        ShakeElapsedTime = ShakeDuration;

        if (VirtualCamera != null)
        virtualCameraNoise = VirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera()
    {
        ShakeElapsedTime = ShakeDuration;
    }

    void UpdateShake()
    {
        float basses = audioAnalyzer.BandVol(20, 144);

        if (basses > 0.12f)
        {
            ShakeAmplitude = audioAnalyzer.BandVol(20, 144) * 3;
        }
        else
        {
            ShakeAmplitude = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateShake();

        // If the Cinemachine componet is not set, avoid update
        if (VirtualCamera != null && virtualCameraNoise != null)
        {
            // If Camera Shake effect is still playing
            if (ShakeElapsedTime > 0)
            {
                // Set Cinemachine Camera Noise parameters
                virtualCameraNoise.m_AmplitudeGain = ShakeAmplitude;
                virtualCameraNoise.m_FrequencyGain = ShakeFrequency;

                // Update Shake Timer
                //ShakeElapsedTime -= Time.deltaTime;
            }
            else
            {
                // If Camera Shake effect is over, reset variables
                virtualCameraNoise.m_AmplitudeGain = 0f;
                ShakeElapsedTime = 0f;
            }
        }
    }
}