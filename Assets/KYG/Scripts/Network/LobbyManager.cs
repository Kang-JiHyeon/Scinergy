using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class LobbyManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        CreateRoom();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //방생성
    public void CreateRoom()
    {
        //방 옵션 세팅
        RoomOptions roomOptions = new RoomOptions();

        //최대 인원(0명이면 최대인원)
        roomOptions.MaxPlayers = 10;
        //룸 목록에 보이냐? 보이지 않느냐?
        roomOptions.IsVisible = true;
        //방을 만든다.
        PhotonNetwork.CreateRoom("star", roomOptions, TypedLobby.Default);
        
    }
    //방 생성 완료
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
    }
    //방 생성 실패
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        print("OnCreatedRoomFailed, " + returnCode + ", " + message);
        //방입장
        JoinRoom();
    }
    //방입장 요청
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom("star");
        //PhotonNetwork.JoinRoom : 선택한 방에 들어갈 때
        //PhotonNetwork.JoinOrCreateRoom : 방이름 설정해서 들어가려고 할 때 해당이름으로 된 방이 없다면 그 이름으로 방 생성 후 입장
        //PhotonNetwork.JoinRandomOrCreateRoom: 랜덤방을 들어가려고 할 때, 조건에 맞는 방이 없다면 내가 방을 생성후 입장
        //PhotonNetwork.JoinRandomRoom : 랜덤한방 들어갈때
    }

    //방 입장이 성공했을 때
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("OnJoinedRoom");
        PhotonNetwork.LoadLevel("KYG_Scene");
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        print("OnJoinRoomFailed, " + returnCode + ", " + message);
        base.OnJoinRoomFailed(returnCode, message);
    }

}
