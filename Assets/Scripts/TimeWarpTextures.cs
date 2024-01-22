using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeWarpTextures : MonoBehaviour
{
    public GameObject emitter;

    private Material _material;

    private void Start()
    {
        _material = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        Shader.SetGlobalVector("_LightPos", emitter.transform.position);
    }
}
