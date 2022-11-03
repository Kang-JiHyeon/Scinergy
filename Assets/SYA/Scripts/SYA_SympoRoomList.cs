using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class SYA_SympoRoomList : MonoBehaviourPunCallbacks
{
    //���� ������   
    Dictionary<string, RoomInfo> roomCache = new Dictionary<string, RoomInfo>();
    //�� ����Ʈ Content
    public Transform trListContent;

    //�濡 ���� ������ ����Ǹ� ȣ�� �Ǵ� �Լ�(�߰�/����/����)
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);

        //�븮��Ʈ UI �� ��ü����
        DeleteRoomListUI();
        //�븮��Ʈ ������ ������Ʈ
        UpdateRoomCache(roomList);
        //�븮��Ʈ UI ��ü ����
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
            // ����, ����
            if (roomCache.ContainsKey(roomList[i].Name))
            {
                //���࿡ �ش� ���� �����Ȱ��̶��
                if (roomList[i].RemovedFromList)
                {
                    //roomCache ���� �ش� ������ ����
                    roomCache.Remove(roomList[i].Name);
                }
                //�׷��� �ʴٸ�
                else
                {
                    //���� ����
                    roomCache[roomList[i].Name] = roomList[i];
                }
            }
            //�߰�
            else
            {
                roomCache[roomList[i].Name] = roomList[i];
            }
        }

        //for (int i = 0; i < roomList.Count; i++)
        //{
        //    // ����, ����
        //    if (roomCache.ContainsKey(roomList[i].Name))
        //    {
        //        //���࿡ �ش� ���� �����Ȱ��̶��
        //        if (roomList[i].RemovedFromList)
        //        {
        //            //roomCache ���� �ش� ������ ����
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
            //������� �����.
            GameObject go = Instantiate(roomItemFactory, trListContent);
            //������� ������ ����(������(0/0))
            SYA_SympoRoomItem item = go.GetComponent<SYA_SympoRoomItem>();
            item.SetInfo(info);

            //��ư ������ ȣ��Ǵ� �Լ�
            //���ٽ�
            //item.onClickAction = (room) => { inputRoomName.text = room; };
            //item.onClickAction = SetRoomName(info.Name);

            string room_name = (string)info.CustomProperties["room_name"];
            //int map_id = (int)info.CustomProperties["map_id"];
            print(room_name);
        }
    }

    //���� ����� id
    int prevmapId = -1;
    void SetRoomName(string room)
    {
        //���̸�����
        //inputRoomName.text = room;
        /*//������ ������� Ȱ��ȭ �Ǿ��ִٸ�
        if (prevmapId > -1)
        {
            //������ ����� ��Ȱ��ȭ
            mapThumbnails[prevmapId].SetActive(false);
        }
        //�� ������ ����
        mapThumbnails[map_id].SetActive(true);
        //���� �� ���̵� ����
        prevmapId = map_id;*/
    }
}
