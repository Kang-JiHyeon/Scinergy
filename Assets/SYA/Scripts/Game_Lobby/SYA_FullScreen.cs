using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class SYA_FullScreen : MonoBehaviour
{
    public static SYA_FullScreen instance;

    public Action<Camera,bool> FullScreen;

    public Camera camera_;
    private void Awake()
    {
        instance = this;
        FullScreen += FullScreenCam;
        camera_ = GetComponent<Camera>();
        camera_.enabled = false;
    }
    void FullScreenCam(Camera camera, bool fullMode)
    {
        camera.enabled=fullMode;
    }
}
