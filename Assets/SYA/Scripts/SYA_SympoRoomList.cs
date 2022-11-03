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
            SYA_SympoRoomItem item = go.GetComponent<SYA_SympoRoomItem>();
            item.SetInfo(info);

            //버튼 눌리면 호출되는 함수
            //람다식
            //item.onClickAction = (room) => { inputRoomName.text = room; };
            //item.onClickAction = SetRoomName(info.Name);

            string room_name = (string)info.CustomProperties["room_name"];
            //int map_id = (int)info.CustomProperties["map_id"];
            print(room_name);
        }
    }

    //이전 썸네일 id
    int prevmapId = -1;
    void SetRoomName(string room)
    {
        //룸이름설정
        //inputRoomName.text = room;
        /*//이전맵 썸네일이 활성화 되어있다면
        if (prevmapId > -1)
        {
            //이전맵 썸네일 비활성화
            mapThumbnails[prevmapId].SetActive(false);
        }
        //맵 섬네일 설정
        mapThumbnails[map_id].SetActive(true);
        //이전 맵 아이디 저장
        prevmapId = map_id;*/
    }
}
