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
        print(PhotonNetwork.NickName);
        base.OnConnectedToMaster();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
        //내 닉네임 설정
        PhotonNetwork.NickName = nameField.text;
        SYA_UserInfoSave.Instance.NicNameSave(nameField.text);
        //로비 진입 요청
        TypedLobby typed = new TypedLobby("C1", LobbyType.Default);
        PhotonNetwork.JoinLobby(typed);
        SYA_SceneChange.Instance.SymposiumRoomList();
    }

    //로비 진입 성공시 호출
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
        //LobbyScene으로 이동
        PhotonNetwork.LoadLevel("AvartaScene");
    }

}
