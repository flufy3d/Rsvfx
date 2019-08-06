using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPointCloudBaker : MonoBehaviour
{

    [Space]
    [SerializeField] RenderTexture _colorMap = null;
    [SerializeField] RenderTexture _positionMap = null;

    [SerializeField] ComputeShader _compute = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
