using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SoundGenerator))]
public class DrawWaveform : MonoBehaviour {

    public GameObject dot;

    private SoundGenerator soundGenerator;

    private void Awake()
    {
        soundGenerator = GetComponent<SoundGenerator>();
    }

    void Update()
    {
        
    }

}
