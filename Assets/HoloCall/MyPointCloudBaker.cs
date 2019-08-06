using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class MyPointCloudBaker : MonoBehaviour
{

    [Space]
    [SerializeField] RenderTexture _colorMap = null;
    [SerializeField] RenderTexture _positionMap = null;

    [SerializeField] ComputeShader _compute = null;

    RenderTexture _tempColorMap;
    RenderTexture _tempPositionMap;

    int2 _dimensions;

    // Start is called before the first frame update
    void Start()
    {
        _dimensions = math.int2(640, 480);

        if (_tempColorMap == null)
        {
            _tempColorMap = new RenderTexture(
                _dimensions.x, _dimensions.y, 0,
                RenderTextureFormat.ARGB32
            );
            _tempColorMap.enableRandomWrite = true;
            _tempColorMap.Create();
        }

        if (_tempPositionMap == null)
        {
            _tempPositionMap = new RenderTexture(
                _dimensions.x, _dimensions.y, 0,
                RenderTextureFormat.ARGBHalf
            );
            _tempPositionMap.enableRandomWrite = true;
            _tempPositionMap.Create();
        }


    }

    // Update is called once per frame
    void Update()
    {
        _compute.SetTexture(0, "ColorMap", _tempColorMap);
        _compute.SetTexture(0, "PositionMap", _tempPositionMap);
        _compute.Dispatch(0, 640 / 8, 480 / 8, 1);

        Graphics.CopyTexture(_tempColorMap, _colorMap);
        Graphics.CopyTexture(_tempPositionMap, _positionMap);
    }
}
