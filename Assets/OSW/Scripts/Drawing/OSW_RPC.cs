using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Voice.Unity.Demos.DemoVoiceUI;
using SYA_UI;

// RPC용 스크립트
public class OSW_RPC : MonoBehaviourPun
{
    OSW_LineDrawer lineDrawer;
    OSW_GameManager gameManager;
    OSW_BtnClickEvent btnClickEvent;
    SYA_SympoUI sympoUI;

    void Update()
    {
        if(lineDrawer == null)
        {
            lineDrawer = FindObjectOfType<OSW_LineDrawer>();
        }

        if (gameManager == null)
        {
            gameManager = FindObjectOfType<OSW_GameManager>();
        }

        if(btnClickEvent == null)
        {
            btnClickEvent = FindObjectOfType<OSW_BtnClickEvent>();
        }

        if(sympoUI == null)
        {
            sympoUI = FindObjectOfType<SYA_SympoUI>();
        }
    }

    [PunRPC]
    public void RpcDrawing(float _linewidth, float r, float g, float b, int _sortingOrder)
    {
        lineDrawer.NetDraw(_linewidth, r, g, b, _sortingOrder);
        
    }

    [PunRPC]
    public void SendLineVec(Vector3 line)
    {
        Debug.LogWarning("OSW_RPC Called!");
        lineDrawer.AddLine(line);
    }

    [PunRPC]
    public void RPCCtrlZ()
    {
        lineDrawer.CtrlZ();
    }

    [PunRPC]
    public void RPCCtrlY()
    {
        lineDrawer.CtrlY();
    }

    [PunRPC]
    public void RPCAllDelete()
    {
        lineDrawer.AllDelete();
    }

    [PunRPC]
    public void RPCAllMute()
    {
        if(SYA_SymposiumManager.Instance.playerVoice[PhotonNetwork.NickName].enabled == true)
        {
            SYA_UIManager.Instance.MicOnOff();
        }
    }


    

    [PunRPC]
    public void RPCGiveAuthority(string name, string authority, int num)
    {
        btnClickEvent.GiveAuthority(name, authority);
    }

    [PunRPC]
    public void RPCClearUserList(Transform transform)
    {
        sympoUI.ClearUserList(transform);
    }
}
