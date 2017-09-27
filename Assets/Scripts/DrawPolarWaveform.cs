using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPolarWaveform : MonoBehaviour {

    public Transform origin;
    public Material mat;
    public float waveLength = 5f;
    public float waveAmp = 1f;
    public float radius = 1f;
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

        float twoPi = 2.0f * Mathf.PI;
        var startVert = new Vector3(Mathf.Cos(0), Mathf.Sin(0), origin.position.z) * radius;
        var startDiff = new Vector3(Mathf.Cos(0) * data[0] * waveAmp, Mathf.Sin(0) * data[0] * waveAmp, origin.position.z);

        startVert += startDiff;
        startVert.z = origin.position.z;

        GL.Vertex(startVert);

        for (int x = 0; x < dataLength; x += channels)
        {
            var radians = ((float)x / dataLength) * twoPi;
            var pos = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), origin.position.z) * radius;
            var diff = new Vector3(Mathf.Cos(radians) * data[x] * waveAmp, Mathf.Sin(radians) * data[x] * waveAmp, origin.position.z);

            pos += diff;
            pos.z = origin.position.z;

            GL.Vertex(pos);
            GL.Vertex(pos);
        }

        GL.End();
        GL.PopMatrix();
    }

}
