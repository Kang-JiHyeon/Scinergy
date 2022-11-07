using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using SYA_UserInfoManagerSaveLoad;

public class SYA_SymposiumManager : MonoBehaviourPun
{
    public static SYA_SymposiumManager Instance;
    //���� ������ ���� �ϰ�, ������ ������

    //�÷��̾� ���� �� ����Ʈ�� �߰�
    //�÷��̾� ����Ʈ�� ���߾� ���� ����Ʈ ��ũ�� �信 ������ �����Ͽ� �����ֱ� 
    public Dictionary<string, PhotonView> player = new Dictionary<string, PhotonView>();
    public Dictionary<string, string> playerAuthority = new Dictionary<string, string>();
    public List<string> playerName = new List<string>();

    //���̸� ���� �����ڵ�
    public string roomName, roomOwner, roomCode;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate(SYA_UserInfoManager.Instance.Avatar, new Vector3(0, 5.5f, 1), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        roomName = PhotonNetwork.CurrentRoom.Name;
        roomOwner = PhotonNetwork.CurrentRoom.CustomProperties["owner"].ToString();
        roomCode = PhotonNetwork.CurrentRoom.CustomProperties["password"].ToString();
    }

    public void PlayerNameAuthority(string name, PhotonView photonView, bool master)
    {
        playerName.Add(name);
        player[name] = photonView;
        if (master)//���� ������ Ŭ���̾�Ʈ���
            playerAuthority[name] = " Presenter";
        else //�ƴ϶��
            playerAuthority[name] = "Audience";
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(player);
            stream.SendNext(playerAuthority);
        }

        else
        {
            player = (Dictionary<string, PhotonView>)stream.ReceiveNext();
            playerAuthority = (Dictionary<string, string>)stream.ReceiveNext();
        }
    }
}
