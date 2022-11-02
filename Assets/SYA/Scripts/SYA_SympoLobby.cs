using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class SYA_SympoLobby : MonoBehaviourPunCallbacks
{
    //방 만들기
    //방생성 페이지
    public GameObject roomCreate;
    //방확인 페이지
    public GameObject roomCompletion;
    //방이름 InputField
    public InputField inputRoomName;
    //방참가 Button
    public Button btnJoin;
    //방생성 Button
    public Button btnCreate;
    //공개여부
    bool public_;
    public GameObject publicText;
    public GameObject falseC;
    public GameObject trueC;

    //안내문구 
    public GameObject characterlimit;

    //방의 정보들   
    Dictionary<string, RoomInfo> roomCache = new Dictionary<string, RoomInfo>();
    //룸 리스트 Content
    public Transform trListContent;

    //Thumbnail
    public GameObject[] Thumbnails;

    //방 만들기 전 정보 확인
    public Text title;
    public Text owner;
    public Text password;

    void Start()
    {
        // 방이름(InputField)이 변경될때 호출되는 함수 등록
        inputRoomName.onValueChanged.AddListener(OnRoomNameValueChanged);
    }

    public void OnRoomNameValueChanged(string s)
    {
        //생성
        btnCreate.interactable = s.Length > 0;
    }

    //생성을 누를 경우
    public void OnClickCreate()
    {
        //→넘을 경우 안내 문구 등장
        characterlimit.SetActive(inputRoomName.text.Length > 20);
        //→안 념을 경우 확인 창On
        roomCreate.SetActive(inputRoomName.text.Length > 20);
        roomCompletion.SetActive(!(inputRoomName.text.Length > 20));
        CreateRoom();
    }

    //방 공개 여부 클릭
    public void OnClickPublic()
    {
        public_ = !public_;
        Vector2 pos = falseC.transform.position;
        falseC.transform.position = trueC.transform.position;
        trueC.transform.position = pos;
        publicText.SetActive(!public_);
        Color Fc = falseC.GetComponent<Image>().color;
        if (public_)
        {
            Fc.b = 255;
            Fc.r = 0;
            Fc.g = 0;
        }
        else
        {
            Fc.b = 255;
            Fc.r = 255;
            Fc.g = 255;
        }
        falseC.GetComponent<Image>().color = Fc;
    }

        // 방 옵션을 설정
        RoomOptions roomOptions = new RoomOptions();
    //방 생성
    public void CreateRoom()
    {
        // 최대 인원 (0이면 최대인원)
        roomOptions.MaxPlayers = 20;
        // 룸 리스트에 보이지 않게? 보이게?
        roomOptions.IsVisible = true;
        // custom 정보를 셋팅
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash["room_name"] = inputRoomName.text;
        hash["owner"] = PhotonNetwork.NickName;
        hash["public"] = public_;
        hash["password"] = $"{Random.Range(0,10)}{Random.Range(0, 10)}{Random.Range(0, 10)}{Random.Range(0, 10)}";
        roomOptions.CustomRoomProperties = hash;
        //커스텀 정보 공개 설정
        roomOptions.CustomRoomPropertiesForLobby = new string[] { "room_name", "owner", "public", "password" };

        //방 정보 창에 띄우기
        OnRoomInfo();
    }

        //방 정보 창에 띄우기
    public void OnRoomInfo()
    {
        title.text = roomOptions.CustomRoomProperties["room_name"].ToString();
        owner.text= roomOptions.CustomRoomProperties["owner"].ToString();
        password.text= roomOptions.CustomRoomProperties["password"].ToString();
    }

    public void OnJoint()
    {
        // 방 생성 요청 (해당 옵션을 이용해서)
        PhotonNetwork.CreateRoom(inputRoomName.text, roomOptions);
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

/*    //방 참가 요청
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(inputRoomName.text + inputPassword.text);
    }*/

    //방 참가가 완료 되었을 때 호출 되는 함수
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("OnJoinedRoom");
        PhotonNetwork.LoadLevel("SymposiumScene");
    }
/*
    //방 참가가 실패 되었을 때 호출 되는 함수
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        print("OnJoinRoomFailed, " + returnCode + ", " + message);
    }

    //방에 대한 정보가 변경되면 호출 되는 함수(추가/삭제/수정)
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);

        //룸리스트 UI 를 전체삭제
        DeleteRoomListUI();
        //룸리스트 정보를 업데이트
        UpdateRoomCache(roomList);
        //룸리스트 UI 전체 생성
        CreateRoomListUI();
    }

    void DeleteRoomListUI()
    {
        foreach (Transform tr in trListContent)
        {
            Destroy(tr.gameObject);
        }
    }

    void UpdateRoomCache(List<RoomInfo> roomList)
    {

        for (int i = 0; i < roomList.Count; i++)
        {
            // 수정, 삭제
            if (roomCache.ContainsKey(roomList[i].Name))
            {
                //만약에 해당 룸이 삭제된것이라면
                if (roomList[i].RemovedFromList)
                {
                    //roomCache 에서 해당 정보를 삭제
                    roomCache.Remove(roomList[i].Name);
                }
                //그렇지 않다면
                else
                {
                    //정보 수정
                    roomCache[roomList[i].Name] = roomList[i];
                }
            }
            //추가
            else
            {
                roomCache[roomList[i].Name] = roomList[i];
            }
        }

        //for (int i = 0; i < roomList.Count; i++)
        //{
        //    // 수정, 삭제
        //    if (roomCache.ContainsKey(roomList[i].Name))
        //    {
        //        //만약에 해당 룸이 삭제된것이라면
        //        if (roomList[i].RemovedFromList)
        //        {
        //            //roomCache 에서 해당 정보를 삭제
        //            roomCache.Remove(roomList[i].Name);
        //            continue;
        //        }                
        //    }
        //    roomCache[roomList[i].Name] = roomList[i];            
        //}
    }

    public GameObject roomItemFactory;
    void CreateRoomListUI()
    {
        foreach (RoomInfo info in roomCache.Values)
        {
            //룸아이템 만든다.
            GameObject go = Instantiate(roomItemFactory, trListContent);
            //룸아이템 정보를 셋팅(방제목(0/0))
            RoomItem item = go.GetComponent<RoomItem>();
            item.SetInfo(info);

            //버튼 눌리면 호출되는 함수
            //람다식
            //item.onClickAction = (room) => { inputRoomName.text = room; };
            item.onClickAction = SetRoomName;

            string desc = (string)info.CustomProperties["desc"];
            int map_id = (int)info.CustomProperties["map_id"];
            print(desc + map_id);
        }
    }

    //이전 썸네일 id
    int prevmapId = -1;
    void SetRoomName(string room, int map_id)
    {
        //룸이름설정
        inputRoomName.text = room;
        //이전맵 썸네일이 활성화 되어있다면
        if (prevmapId > -1)
        {
            //이전맵 썸네일 비활성화
            mapThumbnails[prevmapId].SetActive(false);
        }
        //맵 섬네일 설정
        mapThumbnails[map_id].SetActive(true);
        //이전 맵 아이디 저장
        prevmapId = map_id;
    }*/

}
