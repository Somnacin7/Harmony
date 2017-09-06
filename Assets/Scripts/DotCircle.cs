using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotCircle : MonoBehaviour {

    public GameObject dot = null;
    public float radius = 1.0f;
    public float radiusRange = 1.0f;
    public float frequency = 1.0f;
    public int numberOfDots = 10;

    private float _step;
    private List<GameObject> _dots = new List<GameObject>();

	void Start () {
        _step = (2 * Mathf.PI) / numberOfDots;
        float halfPi = Mathf.PI / 2;

		for (float i = 0; i < (2 * Mathf.PI); i += _step)
        {
            var offset = new Vector3(Mathf.Cos(i + halfPi), Mathf.Sin(i + halfPi));
            var tmp = radius + Mathf.Sin(i * frequency) * radiusRange;
            offset *= tmp;

            _dots.Add(Instantiate(dot, transform.position + offset, Quaternion.identity, null));
        }
	}
	
	void Update () {
		
	}
}
