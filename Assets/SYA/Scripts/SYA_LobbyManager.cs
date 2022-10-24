using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using SYA_UserInfoManagerSaveLoad;

public class SYA_LobbyManager : MonoBehaviourPunCallbacks
{
    //로비씬(로그인완료 씬)에 들어가면 
    //나의 캐릭터 가운데에 등장
    //상단에 내 닉네임과 환영하는 문구(2초 뒤 사라짐)
    //play버튼 누르면 1채널 1번 방 접속

    public GameObject nicnameBox;
    public Text nicknameW;
    float currentTime = 0;
    public float nameFadeTime = 2;

    List<string> roomList = new List<string>();
    string roomName;

    // Start is called before the first frame update
    void Start()
    {
        //나의 캐릭터 가운데에 등장

        //PhotonNetwork.Instantiate("DB에 저장된 아바타 유형", Vector3.zero, Quaternion.identity);
        nicknameW.text = "Signed in as " + PhotonNetwork.NickName;
        OnClickJoinRoom();
    }

    // Update is called once per frame
    void Update()
    {
        /*        if(GameObject.Find("Player"))
                {
                    PhotonNetwork.Instantiate("Player", new Vector3(-0.1f, 6.5f, 0), Quaternion.LookRotation(Camera.main.transform.position));
                }*/
        //상단에 내 닉네임과 환영하는 문구(2초 뒤 사라짐)
        currentTime += Time.deltaTime;
        if (currentTime >= nameFadeTime)
        {
            currentTime = 0;
            nicnameBox.SetActive(false);
        }
    }
    //play버튼 누르면 1채널 1번 방 접속
    public void OnClickJoinRoom()
    {
        CreateRoom();
    }
    //방 생성
    public void CreateRoom()
    {
        // 방 옵션을 설정
        RoomOptions roomOptions = new RoomOptions();
        // 최대 인원 (0이면 최대인원)
        roomOptions.MaxPlayers = 1;
        // 룸 리스트에 보이지 않게? 보이게?
        roomOptions.IsVisible = true;
        roomName = "room" + roomList.Count;
        roomList.Add(roomName);
        // 방 생성 요청 (해당 옵션을 이용해서)
        //PhotonNetwork.CreateRoom(roomName, roomOptions);
        PhotonNetwork.JoinRandomOrCreateRoom(null, 2, MatchmakingMode.FillRoom, null, null, roomName, roomOptions, null);
    }

    //방이 생성되면 호출 되는 함수
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print("OnCreatedRoom");
    }

    //방 생성이 실패 될때 호출 되는 함수
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        print("OnCreateRoomFailed , " + returnCode + ", " + message);
    }

    //방 참가 요청
    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    //방 참가가 완료 되었을 때 호출 되는 함수
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("OnJoinedRoom");
        PhotonNetwork.Instantiate(SYA_UserInfoManager.Instance.Avatar, new Vector3(-0.1f, 6.5f, 0), Quaternion.LookRotation(Camera.main.transform.position));
        SYA_UserInfoSave.Instance.Save();

        //PhotonNetwork.LoadLevel("SYA_MatchingScene");
    }

    //방 참가가 실패 되었을 때 호출 되는 함수
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        print("OnJoinRoomFailed, " + returnCode + ", " + message);
    }

    /*//플레이씬으로 이동하기
    public void PlaySceneChange()
    {
        SYA_SceneChange.Instance.PlayScene();
    }*/
}
