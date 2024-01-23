using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[ImageEffectAllowedInSceneView]
public class CameraPostProcess : MonoBehaviour
{
    public Material myMaterial;
    private Camera myCamera;

    public void Start()
    {
        myCamera = GetComponent<Camera>();
        myCamera.depthTextureMode = DepthTextureMode.Depth;
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.SetRenderTarget(source.colorBuffer, source.depthBuffer);
        Graphics.Blit(source, destination, myMaterial);
    }
}
