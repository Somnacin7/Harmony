using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SoundGenerator))]
public class DrawCartWaveform : MonoBehaviour {

    public Transform origin;
    public Material mat;
    public float waveLength = 5f;
    public float waveAmp = 1f;
    public SoundGenerator soundGenerator;

    private void Awake()
    {
        soundGenerator = GetComponent<SoundGenerator>(); 
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
        GL.MultMatrix(origin.transform.localToWorldMatrix);
        GL.Color(Color.red);
        GL.Begin(GL.LINES);

        var dataLength = soundGenerator.Data.Length;
        var data = new float[dataLength];
        var channels = soundGenerator.Channels;
        soundGenerator.Data.CopyTo(data, 0);

        var startVert = new Vector3(0, data[0] * waveAmp, 0);
        GL.Vertex(startVert);

        for (int x = 0; x < dataLength; x += channels)
        {
            var diff = new Vector3(((float)x / dataLength) * waveLength, data[x] * waveAmp, 0);
            var nextVert = diff;

            // closing vert
            GL.Vertex(nextVert);
            // starting vert
            GL.Vertex(nextVert);
        }

        GL.End();
        GL.PopMatrix();
    }


}
