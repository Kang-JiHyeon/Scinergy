using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SYA_ConnectionManager : MonoBehaviourPunCallbacks
{
    //서버접속
    public void OnClickConnect()//현재 스타트씬의 스타트버튼과 연결됨
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
        PhotonNetwork.NickName = "Player" + Random.Range(0, 10000);//이메일 회원가입 시에 설정해둔 닉네임 정보 불러와서 적용
        //로비 진입 요청
        TypedLobby typed = new TypedLobby("C1", LobbyType.Default);
        PhotonNetwork.JoinLobby(typed);
    }

    //로비 진입 성공시 호출
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);

        //LobbyScene으로 이동
        PhotonNetwork.LoadLevel("LoginScene");
    }
}
