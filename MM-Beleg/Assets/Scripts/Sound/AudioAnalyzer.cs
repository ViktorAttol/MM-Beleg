using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAnalyzer : MonoBehaviour
{
    public AudioSource audioSource;
    public float updateStep = 0.1f;
    public int sampleDataLength = 1024;

    private float currentUpdateTime = 0f;

    private float clipLoudness;
    private float[] clipSampleData;

    private float fMax;

    void Awake()
    {

        if (!audioSource)
        {
            Debug.LogError(GetType() + ".Awake: there was no audioSource set.");
        }
        clipSampleData = new float[sampleDataLength];
        fMax = AudioSettings.outputSampleRate / 2;

    }

    void Update()
    {
        currentUpdateTime += Time.deltaTime;
        if (currentUpdateTime >= updateStep)
        {
            currentUpdateTime = 0f;
            audioSource.clip.GetData(clipSampleData, audioSource.timeSamples);
            clipLoudness = 0f;
            // Average
            foreach (var sample in clipSampleData)
            {
                clipLoudness += Mathf.Abs(sample);
            }
            clipLoudness /= sampleDataLength;

        }
    }

    public float GetLoudness()
    {
        return clipLoudness;
    }

    public float BandVol(float fLow, float fHigh)
    {

        fLow = Mathf.Clamp(fLow, 20, fMax);
        fHigh = Mathf.Clamp(fHigh, fLow, fMax); 


        int n1 = (int)Mathf.Floor(fLow * sampleDataLength / fMax);
        int n2 = (int)Mathf.Floor(fHigh * sampleDataLength / fMax);
        float sum = 0;

        // average
        for (int i = n1; i <= n2; i++)
        {
            sum += Mathf.Abs(clipSampleData[i]);
        }

        return sum / (n2 - n1 + 1);
    }
}