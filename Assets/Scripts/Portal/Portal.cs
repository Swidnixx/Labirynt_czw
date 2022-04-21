using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Portal linkedPortal;
    public MeshRenderer renderPlane;

    public PortalTeleport teleport;

    Camera mainCamera;
    Camera portalCam;

    RenderTexture viewTexture;

    private void Awake()
    {
        mainCamera = Camera.main;
        portalCam = GetComponentInChildren<Camera>();
        //portalCam.enabled = false;

        linkedPortal.teleport.receiver = teleport.transform;
    }

    private void Start()
    {
        CreateViewTexture();
    }

    void CreateViewTexture()
    {
        if(viewTexture == null || viewTexture.width != Screen.width || viewTexture.height != Screen.height)
        {
            viewTexture = new RenderTexture(Screen.width, Screen.height, 0);

            portalCam.targetTexture = viewTexture;
            linkedPortal.renderPlane.material.SetTexture("_MainTex", viewTexture);
        }
    }

    private void Update()
    {
        Matrix4x4 m = transform.localToWorldMatrix * linkedPortal.transform.worldToLocalMatrix *
            mainCamera.transform.localToWorldMatrix;
        portalCam.transform.SetPositionAndRotation(m.GetColumn(3), m.rotation);
    }
}
