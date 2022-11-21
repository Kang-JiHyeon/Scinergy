using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Voice.PUN;
using SYA_UserInfoManagerSaveLoad;

public class SYA_SymposiumManager : MonoBehaviourPun
{
    public static SYA_SymposiumManager Instance;
    //���� ������ ���� �ϰ�, ������ ������

    //�÷��̾� ���� �� ����Ʈ�� �߰�
    //�÷��̾� ����Ʈ�� ���߾� ���� ����Ʈ ��ũ�� �信 ������ �����Ͽ� �����ֱ� 
    //����
    public Dictionary<string, string> playerAuthority = new Dictionary<string, string>();
    //�����
    public Dictionary<string, PhotonView> player = new Dictionary<string, PhotonView>();
    //�����̸� ����Ʈ
    public List<string> playerName = new List<string>();
    //���溸�̽�
    public Dictionary<string, AudioSource> playerVoice = new Dictionary<string, AudioSource>();
    //������ �÷��̾� ��ü
    //public Dictionary<string, GameObject> playerObj = new Dictionary<string, GameObject>();
    //���̸� ���� �����ڵ�
    public string roomName, roomOwner, roomCode;

    private void Awake()
    {
        if (Instance == null)
        {
            //�ν��Ͻ��� ���� �ְ�
            Instance = this;
            //���� ���� ��ȯ�� �Ǿ �ı����� �ʰ� �ϰڴ�

            DontDestroyOnLoad(gameObject);
        }
        //�׷��� ������
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        roomName = PhotonNetwork.CurrentRoom.Name;
        roomOwner = PhotonNetwork.CurrentRoom.CustomProperties["owner"].ToString();//Presenter
        roomCode = PhotonNetwork.CurrentRoom.CustomProperties["password"].ToString();
    }

    public PhotonView GetMyPlayer()
    {
        foreach(PhotonView val in player.Values)
        {
            if(val.IsMine)
            {
                return val;
            }
        }
        return null;
    }

    public void PlayerNameAuthority(string name,PhotonView photonView, AudioSource audioSource)
    {
        //playerName.Add(name);
        player[name] = photonView;
        playerVoice[name] = audioSource;
        //playerObj[name] = gameObject;
    }

    public void PlayerAuthority(string name, bool master)
    {
        //if (master)//���� ������ Ŭ���̾�Ʈ���
        //    playerAuthority[name] = "Owner";
        //else //�ƴ϶��
        //    playerAuthority[name] = "Audience";
        player[PhotonNetwork.NickName].RPC("RPCPlayerAuthority", RpcTarget.All, name, master);

    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(player);
            stream.SendNext(playerAuthority);
            stream.SendNext(playerVoice);
        }

        else
        {
            player = (Dictionary<string, PhotonView>)stream.ReceiveNext();
            playerAuthority = (Dictionary<string, string>)stream.ReceiveNext();
            playerVoice = (Dictionary<string, AudioSource>)stream.ReceiveNext();
        }
    }
}
