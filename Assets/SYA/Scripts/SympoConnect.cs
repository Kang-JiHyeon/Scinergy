using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SympoConnect : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
        //플레이어가 방을 나간다
    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        print("방나가짐");
    }
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
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
