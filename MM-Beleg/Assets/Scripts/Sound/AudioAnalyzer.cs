using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles audio analysis of music to use with camera shake effect. 
/// </summary>
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
        // max. frequency the audio can have
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

    /// <summary>
    /// Calculates volume of given frequency range for syncing to camera shake effect. 
    /// </summary>
    /// <param name="fLow"> Low frequency of band. </param>
    /// <param name="fHigh"> High frequency of band. </param>
    /// <returns> Average volume of given band. </returns>
    public float BandVol(float fLow, float fHigh)
    {
        // Limits lower band, because anything under 20Hz is not audible for most humans
        fLow = Mathf.Clamp(fLow, 20, fMax);
        // Use clamp again with 20Hz as Minimum and Samplerate / 2 as Maximum (processing more data is not needed)
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