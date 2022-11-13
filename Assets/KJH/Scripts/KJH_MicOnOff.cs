using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class KJH_MicOnOff : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //����ũ �¿���
    public GameObject micOn;
    public GameObject micOff;
    public void MicOnOff()
    {
        // �ӽ÷� �����Ͷ�� ������ -> owner �� �����ؾ���
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("RpcMicOnOff", RpcTarget.All, PhotonNetwork.NickName, micOn.activeSelf);
        }
        else
        {

        }

        micOn.SetActive(!micOn.activeSelf);
        micOff.SetActive(!micOff.activeSelf);
        // ����ũ Ű��
        // ������ �г����� ����� �ҽ��� ���� �Ҵ�
        //SYA_SymposiumManager.Instance.playerVoice[PhotonNetwork.NickName].enabled = micOn.activeSelf;
        //SYA_SymposiumManager.Instance.player[PhotonNetwork.NickName].RPC("RpcMicOnOff", RpcTarget.All, PhotonNetwork.NickName, micOn.activeSelf);
        photonView.RPC("RpcMicOnOff", RpcTarget.All, PhotonNetwork.NickName, micOn.activeSelf);
    }

    [PunRPC]
    public void RpcMicOnOff(string name, bool micOn)
    {
        SYA_SymposiumManager.Instance.playerVoice[name].enabled = micOn;
    }
}
