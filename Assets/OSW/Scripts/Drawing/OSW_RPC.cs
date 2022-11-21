using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Voice.Unity.Demos.DemoVoiceUI;
using SYA_UI;

// RPC�� ��ũ��Ʈ
public class OSW_RPC : MonoBehaviourPun
{
    OSW_LineDrawer lineDrawer;
    OSW_GameManager gameManager;

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
    void RPCCtrlZ()
    {
        lineDrawer.CtrlZ();
    }

    [PunRPC]
    void RPCCtrlY()
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
    void RPCPlayerAuthority(string name, bool master)
    {
        if (master)//���� ������ Ŭ���̾�Ʈ���
            SYA_SymposiumManager.Instance. playerAuthority[name] = "Owner";
        else //�ƴ϶��
            SYA_SymposiumManager.Instance.playerAuthority[name] = "Audience";
    }
}
