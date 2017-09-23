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
    public int Channels { get; set; }

    private float _increment;
    private float _phase;
    private float _samplingFrequency = 48000;

    private void Awake()
    {
        _samplingFrequency = AudioSettings.outputSampleRate;
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
        // update increment in case frequency has changed
        _increment = frequency * 2 * Mathf.PI / _samplingFrequency;
        for (int i = 0; i < data.Length; i += channels)
        {
            _phase += _increment;

            // this is where we copy audio data to make them "available" to Unity
            switch (waveType)
            {
                case WaveType.SINE:
                    data[i] = gain * Mathf.Sin(_phase);
                    break;
                case WaveType.SQUARE:
                    data[i] = gain * Mathf.Sign(Mathf.Sin(_phase));
                    break;
                case WaveType.TRIANGLE:
                    var wave = Mathf.Abs(Mathf.Lerp(-1, 1, _phase / (2 * Mathf.PI))) * 2 - 1;
                    data[i] = gain * wave;
                    break;
                case WaveType.SAWTOOTH:
                    data[i] = gain * Mathf.Lerp(-1, 1, _phase / (2 * Mathf.PI));
                    break;
            }

            // if we have stereo, copy the mono data to each channel
            if (channels == 2)
            {
                data[i + 1] = data[i];
            }

            if (_phase > 2 * Mathf.PI)
            {
                _phase = 0;
            }
        }

        Data = data;
        Channels = channels;    
    }
}
