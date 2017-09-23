using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SoundGenerator))]
public class DrawCartWaveform : MonoBehaviour {

    public Transform origin;
    public Material mat;
    public float waveLength = 5f;
    public float waveAmp = 1f;

    private SoundGenerator _soundGenerator;

    private void Awake()
    {
        _soundGenerator = GetComponent<SoundGenerator>();
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

        var dataLength = _soundGenerator.Data.Length;
        var data = new float[dataLength];
        var channels = _soundGenerator.Channels;
        _soundGenerator.Data.CopyTo(data, 0);

        var startVert = new Vector3((origin.position.x + 0 / (float)dataLength) * waveLength, origin.position.y + data[0] * waveAmp, origin.position.z);
        GL.Vertex(startVert);

        for (int x = 0; x < dataLength; x += channels)
        {
            var nextVert = new Vector3((origin.position.x + x / (float)dataLength) * waveLength, origin.position.y + data[x] * waveAmp, origin.position.z);

            // closing vert
            GL.Vertex(nextVert);
            // starting vert
            GL.Vertex(nextVert);
        }

        GL.End();
        GL.PopMatrix();
    }


}
