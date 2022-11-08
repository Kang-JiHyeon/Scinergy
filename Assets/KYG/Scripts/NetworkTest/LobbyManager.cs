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
    }
    //방입장
    
}
