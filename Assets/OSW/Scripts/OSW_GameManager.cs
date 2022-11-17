using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using SYA_UI;
using Photon.Voice.Unity.Demos.DemoVoiceUI;

// ���� Script
public class OSW_GameManager : MonoBehaviourPun
{
    public OSW_GameManager Instance;

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
        
    }

    void Update()
    {
        // ������ ��쿡 ��� ���Ұ� ��ư�� Ȱ��ȭ �ǰ�, �ƴϸ� Ȱ��ȭ �ȵǰ�?
        //if (PhotonNetwork.MasterClient.UserId == SYA_SymposiumManager.Instance.player[PhotonNetwork.NickName].Owner.UserId)
        if (SYA_SymposiumManager.Instance.playerAuthority[PhotonNetwork.NickName] == "Owner")
        {
            Debug.LogWarning("���� ���Դ�!!");
            mute.SetActive(true);
        }
    }

    public GameObject mute; // ���Ұ�
    public GameObject unmute; // ���Ұ� ����
    // ��� ���Ұ�
    public void AllMute()
    {
        // AllMute ��ư�� ������ mute ������Ʈ�� ���ְ�, unmute ������Ʈ�� ���ش�.
        mute.SetActive(false);
        unmute.SetActive(true);

        PhotonView mine = SYA_SymposiumManager.Instance.GetMyPlayer();
        if (mine)
        {
            for (int i = 0; i < SYA_SymposiumManager.Instance.playerName.Count; ++i)
            {
                mine.RPC("RPCAllMute", RpcTarget.All, i, SYA_UIManager.Instance.micOff.activeSelf);
            }
        }
    }

    
    // ��� ���Ұ� ����
    public void AllUnmute()
    {
        // AllUnmute ��ư�� ������ unmute ������Ʈ�� ���ְ�, mute ������Ʈ�� ���ش�.
        unmute.SetActive(false);
        mute.SetActive(true);

        PhotonView mine = SYA_SymposiumManager.Instance.GetMyPlayer();
        if (mine)
        {
            for (int i = 0; i < SYA_SymposiumManager.Instance.playerName.Count; ++i)
            {
                mine.RPC("RPCAllUnmute", RpcTarget.All, i, SYA_UIManager.Instance.micOn.activeSelf);
            }
        }
    }


    // �ϴ� �������´�� �����

    // ������ û���� ��ǥ�ڷ� ���� ��ư�� ������
    // ȭ�����, �Ǽ������ Ȱ��ȭ�ǰ� �ϰ� �ʹ�.
    // Ȱ��ȭ ������� �� ��ư �̸� : DrawOnOff, UwcOnOff, Button_Recod
    // �� ������ �ƴϸ� ��� ��ɵ��� setActive == false !!

    // ������ �ٲ���!
    public void GiveAuthority()
    {

    }
}
