using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.Video;

public class HologramRender : MonoBehaviour {

    //gl style point size only effect on opengl platform like mac iphone android etc.
    public bool UseGLStyle;

    public Material mHologramMat;

    public Material mHologramMatGL;

    public Texture2D mTextureSource;

    [Range(0.001f,0.03f)]
    public float PointSize = 0.003f;

    private Mesh mBaseMesh;

    private Bounds mDisplayBounds = new Bounds(Vector3.zero, Vector3.one * 10);

    private MaterialPropertyBlock mHologramMatProps;

    public VideoPlayer vp;

    public RawImage prevImage;

    // Use this for initialization
    void Start () {

        CreateBaseMesh();

        mTextureSource.filterMode = FilterMode.Point;
        
        mTextureSource.wrapMode = TextureWrapMode.Clamp;

    }
	
	// Update is called once per frame
	void Update () {

        if (mHologramMatProps == null)
        {
            

            if(vp != null)
            {
                if (vp.isPrepared)
                {
                    mHologramMatProps = new MaterialPropertyBlock();
                    //only once
                    mHologramMatProps.SetTexture("_MainTex", vp.texture);

                    prevImage.texture = vp.texture;
                }

            }
            else
            {
                mHologramMatProps = new MaterialPropertyBlock();
                //only once
                mHologramMatProps.SetTexture("_MainTex", mTextureSource);

                prevImage.texture = mTextureSource;
            }

        }
        else
        {
            mHologramMatProps.SetFloat("_PointSize", PointSize);
        }

       


        if (!UseGLStyle)
        {

            Graphics.DrawMesh(
                    mBaseMesh, transform.localToWorldMatrix,
                    mHologramMat, 0, null, 0, mHologramMatProps,
                    ShadowCastingMode.Off, false);
        }
        else
        {
            Graphics.DrawMesh(
                    mBaseMesh, transform.localToWorldMatrix,
                    mHologramMatGL, 0, null, 0, mHologramMatProps,
                    ShadowCastingMode.Off, false);

        }

    }

    void CreateBaseMesh()
    {
        mBaseMesh = new Mesh();
        mBaseMesh.MarkDynamic();
        mBaseMesh.hideFlags = HideFlags.DontSave;
        mBaseMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

        int wAmount = 640;
        int hAmount = 480;

        int totalAmount = wAmount * hAmount;

        Vector3[] vertex = new Vector3[totalAmount];
        Vector2[] uv = new Vector2[totalAmount];
        int[] indices = new int[totalAmount];

        int normConst = hAmount;
        int index = 0;
        for (int yCoord = 0; yCoord < hAmount; yCoord++)
        {
            for (int xCoord = 0; xCoord < wAmount; xCoord++)
            {
                vertex[index] = new Vector3(
                     ((float)xCoord - (0.5f * (float)wAmount)) / (float)normConst,
                     ((float)yCoord - (0.5f * (float)hAmount)) / (float)normConst,
                     0f
                    );

                uv[index] = new Vector2(
                    (float)xCoord / (float)wAmount, 
                    (float)yCoord / (float)hAmount);

                indices[index] = index;

                index++;
            }
        }


        mBaseMesh.vertices = vertex;
        mBaseMesh.uv = uv;
        mBaseMesh.SetIndices(indices, MeshTopology.Points, 0);
        mBaseMesh.bounds = mDisplayBounds;

    }
}
