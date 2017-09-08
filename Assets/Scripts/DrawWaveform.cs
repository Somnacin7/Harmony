using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SoundGenerator))]
public class DrawWaveform : MonoBehaviour {

    public GameObject dot;
    public Transform origin;
    public Material mat;
    public float waveLength = 5f;
    public float waveAmp = 1f;

    private SoundGenerator soundGenerator;
    private Vector3 startPosition;

    private void Awake()
    {
        soundGenerator = GetComponent<SoundGenerator>();
    }

    void Update()
    {
        startPosition = origin.position;
    }

    void OnRenderObject()
    {
        if (!mat)
        {
            Debug.LogError("Please Assign a material on the inspector");
            return;
        }
        mat.SetPass(0);
        GL.PushMatrix();
        GL.MultMatrix(transform.localToWorldMatrix);
        GL.Begin(GL.LINES);
        GL.Color(Color.red);
        GL.Vertex(startPosition);


        var dataLength = soundGenerator.Data.Length;
        for (int x = 0; x < dataLength; x += soundGenerator.Channels)
        {
            var nextVert = new Vector3((x / (float)dataLength) * waveLength, soundGenerator.Data[x] * waveAmp);
            GL.Vertex(nextVert);
            GL.Vertex(nextVert);
        }

        GL.End();
        GL.PopMatrix();
    }


}
