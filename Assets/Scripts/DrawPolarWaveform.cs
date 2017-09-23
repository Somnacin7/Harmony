using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPolarWaveform : MonoBehaviour {

    public Transform origin;
    public Material mat;
    public float waveLength = 5f;
    public float waveAmp = 1f;
    public float radius = 1f;

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

        float twoPi = 2.0f * Mathf.PI;
        var startVert = new Vector3(origin.position.x, origin.position.y + radius, origin.position.z);
        GL.Vertex(startVert);

        for (int x = 0; x < dataLength; x += channels)
        {
            
        }

        GL.End();
        GL.PopMatrix();
    }

}
