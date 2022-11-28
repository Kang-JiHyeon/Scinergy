using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using SYA_UserInfoManagerSaveLoad;
using UnityEngine.SceneManagement;

public class SYA_ConnectionManager : MonoBehaviourPunCallbacks
{
    public InputField nameField;
    public Text nameCount;
    string count;

    private void Update()
    {
        string count = nameField.text.Length.ToString();
        nameCount.text = $"{int.Parse(count)} / 10";
    }

    //��������
    public void OnClickConnect()//���� ���Ӽ��þ� �Ϸ��ư�� �����
    {
        if (nameField.text == "") return;
        PhotonNetwork.AutomaticallySyncScene = true; 
        //���� ���� ��û
        PhotonNetwork.ConnectUsingSettings();
        /*SYA_Loading.LoadScene("SYA_SymposiumRoomList", SceneManager.GetActiveScene().name);
        SYA_Loading.Instance.OnCoroutin();*/
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
