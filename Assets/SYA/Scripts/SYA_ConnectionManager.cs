using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SYA_ConnectionManager : MonoBehaviourPunCallbacks
{
    //��������
    public void OnClickConnect()//���� ��ŸƮ���� ��ŸƮ��ư�� �����
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
        PhotonNetwork.NickName = "Player" + Random.Range(0, 10000);//�̸��� ȸ������ �ÿ� �����ص� �г��� ���� �ҷ��ͼ� ����
        //�κ� ���� ��û
        TypedLobby typed = new TypedLobby("C1", LobbyType.Default);
        PhotonNetwork.JoinLobby(typed);
    }

    //�κ� ���� ������ ȣ��
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);

        //LobbyScene���� �̵�
        PhotonNetwork.LoadLevel("LoginScene");
    }
}
