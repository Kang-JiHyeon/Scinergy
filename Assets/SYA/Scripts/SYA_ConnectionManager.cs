using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using SYA_UserInfoManagerSaveLoad;

public class SYA_ConnectionManager : MonoBehaviourPunCallbacks
{
    public InputField nameField;
    public Text nameCount;

    private void Update()
    {
        string count = nameField.text.Length.ToString();
        nameCount.text = $"{count} / 10";
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
        print(PhotonNetwork.NickName);
        base.OnConnectedToMaster();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
        //�� �г��� ����
        PhotonNetwork.NickName = nameField.text;
        SYA_UserInfoSave.Instance.NicNameSave(nameField.text);
        //�κ� ���� ��û
        TypedLobby typed = new TypedLobby("C1", LobbyType.Default);
        PhotonNetwork.JoinLobby(typed);
        SYA_SceneChange.Instance.SymposiumRoomList();
    }

    //�κ� ���� ������ ȣ��
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
        //LobbyScene���� �̵�
        PhotonNetwork.LoadLevel("AvartaScene");
    }

}
