using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Portal LinkedTo;

    [SerializeField]
    private MeshRenderer _render;

    [SerializeField]
    private Camera _portalCam;

    [SerializeField]
    private RenderTextureHolder _baseRT;

    [SerializeField]
    private Material _basePortalMat;

    private RenderTexture _renderTexture;

	private void OnEnable()
	{
        if (!_renderTexture)
            _renderTexture = new RenderTexture(_baseRT.RT);

        _portalCam.targetTexture = _renderTexture;

        _render.material = new Material(_basePortalMat);
        _render.material.mainTexture = _renderTexture;
	}

	private void OnDisable()
	{
        _portalCam.targetTexture = null;

        _render.material = null;

        _renderTexture.Release();

        _renderTexture = null;
	}

	// Update is called once per frame
	void Update()
    {
        
    }

	private void LateUpdate()
	{
        _portalCam.Render();
	}
}
