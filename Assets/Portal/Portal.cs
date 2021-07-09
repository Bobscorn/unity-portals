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

    [SerializeField]
    private Material _transparentPortalMat;

    private Material _myPortalMat;

    private RenderTexture _renderTexture;

	private void OnEnable()
	{
        if (!_renderTexture)
            _renderTexture = new RenderTexture(_baseRT.RT);

        _portalCam.targetTexture = _renderTexture;

        _myPortalMat = new Material(_basePortalMat);
        _myPortalMat.mainTexture = _renderTexture;
        _render.material = _myPortalMat;
	}

	private void OnDisable()
	{
        _portalCam.targetTexture = null;

        _render.material = null;

        _renderTexture.Release();

        _renderTexture = null;

        _myPortalMat = null;
	}

	// Update is called once per frame
	void Update()
    {
        
    }

	private void LateUpdate()
	{
        LinkedTo.GoTransparent();

        var camTrans = Camera.main.transform;
        var linkedTrans = LinkedTo.transform;
        var myTrans = _portalCam.transform;

        var portalDistanceFromCam = Vector3.Dot(camTrans.forward, transform.position - camTrans.position);
        myTrans.position = linkedTrans.position - linkedTrans.forward * portalDistanceFromCam;

        myTrans.forward = linkedTrans.TransformDirection(camTrans.forward);
        myTrans.right = linkedTrans.TransformDirection(camTrans.right);
        myTrans.up = linkedTrans.TransformDirection(camTrans.up);

        _portalCam.Render();

        LinkedTo.GoNormal();
	}

    private void GoTransparent()
	{
        gameObject.layer = LayerMask.NameToLayer("PortalNoRender");
    }

    private void GoNormal()
	{
        gameObject.layer = LayerMask.NameToLayer("Default");
	}
}
