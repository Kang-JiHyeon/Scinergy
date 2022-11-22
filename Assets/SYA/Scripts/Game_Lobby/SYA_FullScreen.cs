using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class SYA_FullScreen : MonoBehaviour
{
    public static SYA_FullScreen instance;

    public Action<bool> FullScreen;

    public Camera camera_;
    private void Awake()
    {
        instance = this;
        FullScreen += FullScreenCam;
    }
    void FullScreenCam(bool fullMode)
    {
        camera_.enabled=fullMode;
    }
}
