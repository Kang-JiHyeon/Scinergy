using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
public class ConnectionManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        //NameServer ����(APPId, GameVersion, ����)
        PhotonNetwork.ConnectUsingSettings();
    }

    //������ ������ ���� ����, �κ� ���� Ȥ�� �����Ҽ� ���� ����
    public override void OnConnected()
    {
        base.OnConnected();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
    }
    //������ ������ ����, �κ� ���� �� ������ ����
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
        //�г��� ����
        PhotonNetwork.NickName = "star_" + Random.Range(1,10000);
        //�⺻ �κ� ����
        PhotonNetwork.JoinLobby();
        //Ư�� �κ� ����
        //PhotonNetwork.JoinLobby(new TypedLobby("��Ÿ�κ�", LobbyType.Default));
    }

    //�κ� ���� ������ ȣ��
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
        SceneManager.LoadScene("LobbyScene");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
