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

    //마이크 온오프
    public GameObject micOn;
    public GameObject micOff;
    public void MicOnOff()
    {
        // 임시로 마스터라고 지정함 -> owner 로 변경해야함
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("RpcMicOnOff", RpcTarget.All, PhotonNetwork.NickName, micOn.activeSelf);
        }
        else
        {

        }

        micOn.SetActive(!micOn.activeSelf);
        micOff.SetActive(!micOff.activeSelf);
        // 마이크 키기
        // 본인의 닉네임의 오디오 소스를 끄고 켠다
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
