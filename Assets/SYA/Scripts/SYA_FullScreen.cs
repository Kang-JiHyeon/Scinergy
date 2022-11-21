using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SYA_FullScreen : MonoBehaviour
{
    public Camera camera;
    private void Awake()
    {
        SYA_SymposiumManager.Instance.playerObj[PhotonNetwork.NickName].GetComponent<PlayerMove>().FullScreen += FullScreen;
    }
    void FullScreen(bool fullMode)
    {
        camera.enabled=fullMode;
    }
}
