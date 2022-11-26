using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using SYA_UI;
using Photon.Voice.Unity.Demos.DemoVoiceUI;

// ���� Script
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
        // ������ ��쿡 ��� ���Ұ� ��ư�� Ȱ��ȭ �ǰ�, �ƴϸ� Ȱ��ȭ �ȵǰ�?
        if (SYA_SymposiumManager.Instance.playerAuthority[PhotonNetwork.NickName] == "Owner")
        {
            Debug.LogWarning("���� ���Դ�!!");
            mute.SetActive(true);
        }
    }

    public GameObject mute; // ��� ���Ұ� ��ư

    // ��� ���Ұ�
    public void AllMute()
    {
        PhotonView mine = SYA_SymposiumManager.Instance.GetMyPlayer();
        if (mine)
        {
            mine.RPC("RPCAllMute", RpcTarget.All);
        }
    }
}
