using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using SYA_UI;
using Photon.Voice.Unity.Demos.DemoVoiceUI;

// 권한 Script
public class OSW_GameManager : MonoBehaviourPun
{
    public static OSW_GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // 방장일 경우에 모두 음소거 버튼이 활성화 되고, 아니면 활성화 안되게?
        if (SYA_SymposiumManager.Instance.playerAuthority[PhotonNetwork.NickName] == "Owner")
        {
            Debug.LogWarning("방장 들어왔다!!");
            mute.SetActive(true);
        }
    }

    public GameObject mute; // 모두 음소거 버튼

    // 모두 음소거
    public void AllMute()
    {
        PhotonView mine = SYA_SymposiumManager.Instance.GetMyPlayer();
        if (mine)
        {
            mine.RPC("RPCAllMute", RpcTarget.All);
        }
    }
}
