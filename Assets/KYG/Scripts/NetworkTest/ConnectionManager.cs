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
        //NameServer 접속(APPId, GameVersion, 지역)
        PhotonNetwork.ConnectUsingSettings();
    }

    //마스터 서버에 접속 성공, 로비 생성 혹은 진입할수 없는 상태
    public override void OnConnected()
    {
        base.OnConnected();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
    }
    //마스터 서버에 접속, 로비 생성 및 진입이 가능
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
        //닉네임 설정
        PhotonNetwork.NickName = "star_" + Random.Range(1,10000);
        //기본 로비 진입
        PhotonNetwork.JoinLobby();
        //특정 로비 진입
        //PhotonNetwork.JoinLobby(new TypedLobby("스타로비", LobbyType.Default));
    }

    //로비 접속 성공시 호출
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
