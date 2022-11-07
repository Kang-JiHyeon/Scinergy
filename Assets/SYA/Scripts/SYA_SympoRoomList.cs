using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class SYA_SympoRoomList : MonoBehaviourPunCallbacks
{
    //방의 정보들   
    Dictionary<string, RoomInfo> roomCache = new Dictionary<string, RoomInfo>();
    //룸 리스트 Content
    public Transform trListContent;

    List<RoomInfo> roomListDe = new List<RoomInfo>();

    private void Awake()
    {

    }

    //방에 대한 정보가 변경되면 호출 되는 함수(추가/삭제/수정)
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        roomListDe = roomList;
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
        //모든 방
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
        
        
    }

    public GameObject roomItemFactory;
    void CreateRoomListUI()
    {
        foreach (RoomInfo info in roomCache.Values)
        {
            //트루라면 비공개 방 제외
            if (openRoomListOn == true ? info.CustomProperties["public"].ToString() == "True" : true)
            {
                //룸아이템 만든다.
                GameObject go = Instantiate(roomItemFactory, trListContent);
                //룸아이템 정보를 셋팅(방제목(0/0))
                SYA_SympoRoomItem item = go.GetComponent<SYA_SympoRoomItem>();
                item.SetInfo(info);

                string room_name = (string)info.CustomProperties["room_name"];
                //int map_id = (int)info.CustomProperties["map_id"];
                print(room_name);
            }
        }
    }

    //디폴트
    //버튼을 누르면 모든 방(공개, 비공개 상관없이)
    public void AllRoomList()
    {
        openRoomListOn = false;
        DeleteRoomListUI();
        UpdateRoomCache(roomListDe);
        CreateRoomListUI();
    }

    public bool openRoomListOn;
    //버튼을 누르면 공개로 설정한 방들이 보임
    public void OpenRoomList()
    {
        openRoomListOn = true;
        DeleteRoomListUI();
        UpdateRoomCache(roomListDe);
        CreateRoomListUI();
    }
}
