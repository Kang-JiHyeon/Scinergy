using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Voice.Unity.Demos.DemoVoiceUI;

// RPC용 스크립트
public class OSW_RPC : MonoBehaviourPun
{
    OSW_LineDrawer lineDrawer;
    OSW_GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        //lineDrawer = FindObjectOfType<OSW_LineDrawer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(lineDrawer == null)
        {
            lineDrawer = FindObjectOfType<OSW_LineDrawer>();
        }

        if(gameManager == null)
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
    public void RPCAllMute(int i, bool micOff)
    {
        SYA_SymposiumManager.Instance.playerVoice[SYA_SymposiumManager.Instance.playerName[i]].enabled = micOff;
    }

    public void RPCAllUnmute(int i, bool micon)
    {
        SYA_SymposiumManager.Instance.playerVoice[SYA_SymposiumManager.Instance.playerName[i]].enabled = micon;
    }
}
