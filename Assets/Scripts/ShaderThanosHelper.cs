using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderThanosHelper : MonoBehaviour
{
    [SerializeField] float meshHeight;
    [SerializeField] Transform slicer;
    [SerializeField] MeshRenderer _renderer;
    Material _material;
    float value;
    // Start is called before the first frame update
    void Start()
    {
        _material = _renderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        value = 1 - (meshHeight - slicer.localPosition.y) / meshHeight;
        _material.SetFloat("_ClipThreshold", value);
    }
}
