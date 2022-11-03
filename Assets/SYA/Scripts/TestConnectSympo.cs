using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class TestConnectSympo : MonoBehaviourPunCallbacks
{
    public InputField nameField;
    public Text nameCount;

    private void Start()
    {
        OnClickConnect();
    }

    //��������
    public void OnClickConnect()//���� ���Ӽ��þ� �Ϸ��ư�� �����
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        //���� ���� ��û
        PhotonNetwork.ConnectUsingSettings();
    }


    //������ ���� ���Ӽ����� ȣ��(Lobby�� ������ �� ���� ����)
    public override void OnConnected()
    {
        base.OnConnected();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
    }

    //������ ���� ���Ӽ����� ȣ��(Lobby�� ������ �� �ִ� ����)
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
        //�� �г��� ����
        PhotonNetwork.NickName = "saasss";
        //�κ� ���� ��û
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
