using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WaveType
{
    SINE,
    SQUARE,
    TRIANGLE,
    SAWTOOTH
}

public class SoundGenerator : MonoBehaviour {

    public float frequency = 440.0f;
    public float gain = 0.05f;
    public WaveType waveType = WaveType.SINE;

    public float[] Data { get; set; }

    private float increment;
    private float phase;
    private float samplingFrequency = 48000;

    private void Awake()
    {
        samplingFrequency = AudioSettings.outputSampleRate;
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
        // update increment in case frequency has changed
        increment = frequency * 2 * Mathf.PI / samplingFrequency;
        for (int i = 0; i < data.Length; i += channels)
        {
            phase += increment;

            // this is where we copy audio data to make them "available" to Unity
            switch (waveType)
            {
                case WaveType.SINE:
                    data[i] = gain * Mathf.Sin(phase);
                    break;
                case WaveType.SQUARE:
                    data[i] = gain * Mathf.Sign(Mathf.Sin(phase));
                    break;
                case WaveType.TRIANGLE:
                    var wave = Mathf.Abs(Mathf.Lerp(-1, 1, phase / (2 * Mathf.PI))) * 2 - 1;
                    data[i] = gain * wave;
                    break;
                case WaveType.SAWTOOTH:
                    data[i] = gain * Mathf.Lerp(-1, 1, phase / (2 * Mathf.PI));
                    break;
            }

            // if we have stereo, copy the mono data to each channel
            if (channels == 2)
            {
                data[i + 1] = data[i];
            }

            if (phase > 2 * Mathf.PI)
            {
                phase = 0;
            }
        }

        Data = data;
    }
}
