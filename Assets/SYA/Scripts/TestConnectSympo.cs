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

    //서버접속
    public void OnClickConnect()//현재 네임세팅씬 완료버튼과 연결됨
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        //서버 접속 요청
        PhotonNetwork.ConnectUsingSettings();
    }


    //마스터 서버 접속성공시 호출(Lobby에 진입할 수 없는 상태)
    public override void OnConnected()
    {
        base.OnConnected();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
    }

    //마스터 서버 접속성공시 호출(Lobby에 진입할 수 있는 상태)
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
        //내 닉네임 설정
        PhotonNetwork.NickName = "saasss";
        //로비 진입 요청
        TypedLobby typed = new TypedLobby("C2", LobbyType.Default);
        PhotonNetwork.JoinLobby(typed);
    }

    //로비 진입 성공시 호출
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
        //LobbyScene으로 이동
        PhotonNetwork.LoadLevel("SYA_SymposiumRoomList");
    }
}
