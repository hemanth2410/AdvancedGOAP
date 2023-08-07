using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToneGenerator : MonoBehaviour
{
    float duration = 1;
    int _frequency;
    [SerializeField] List<ToneData> toneData = new List<ToneData>();
    [SerializeField] List<AudioClip> clipList = new List<AudioClip>();
    public void CreateTone(string name, int frequency)
    {
        _frequency = frequency;
        AudioClip clip = AudioClip.Create(name, (int)(duration * AudioSettings.outputSampleRate), 1, AudioSettings.outputSampleRate, false, OnAudioRead);
        clipList.Add(clip);
    }
    private void OnAudioRead(float[] data)
    {
        // Generate a sine wave at the given frequency
        float increment = _frequency * 2 * Mathf.PI / AudioSettings.outputSampleRate;
        float phase = 0;
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = Mathf.Sin(phase);
            phase += increment;
        }
    }

    private void Start()
    {
        for (int i = 0; i < toneData.Count; i++)
        {
            CreateTone(toneData[i].ToneName, toneData[i].ToneFrequency);
        }
    }

}

[System.Serializable]
public class ToneData
{
    [SerializeField] string m_toneName;
    [SerializeField] int m_toneFrequency;
    public string ToneName { get { return m_toneName; } }
    public int ToneFrequency { get { return m_toneFrequency; } }
}
