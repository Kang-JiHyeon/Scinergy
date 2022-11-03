using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SympoConnect : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
        //�÷��̾ ���� ������
    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        print("�泪����");
    }
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
        TypedLobby typed = new TypedLobby("C2", LobbyType.Default);
        PhotonNetwork.JoinLobby(typed);
    }
    //�κ� ���� ������ ȣ��
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
        //LobbyScene���� �̵�
        PhotonNetwork.LoadLevel("SYA_SymposiumRoomList");
    }
}
