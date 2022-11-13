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

    List<RoomInfo> roomListDe = new List<RoomInfo>();

    private void Awake()
    {

    }

    //�濡 ���� ������ ����Ǹ� ȣ�� �Ǵ� �Լ�(�߰�/����/����)
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        roomListDe = roomList;
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
        //��� ��
        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].Name.Length<4 || roomList[i].Name.Substring(0, 4) != "Star")
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
        }
        
        
    }

    public GameObject roomItemFactory;
    void CreateRoomListUI()
    {
        foreach (RoomInfo info in roomCache.Values)
        {
            //Ʈ���� ����� �� ����
            if (openRoomListOn == true ? info.CustomProperties["public"].ToString() == "True" : true)
            {
                //������� �����.
                GameObject go = Instantiate(roomItemFactory, trListContent);
                //������� ������ ����(������(0/0))
                SYA_SympoRoomItem item = go.GetComponent<SYA_SympoRoomItem>();
                item.SetInfo(info);

                string room_name = (string)info.CustomProperties["room_name"];
                //int map_id = (int)info.CustomProperties["map_id"];
                print(room_name);
            }
        }
    }

    public GameObject AllOn;
    public GameObject OpenOn;

    //����Ʈ
    //��ư�� ������ ��� ��(����, ����� �������)
    public void AllRoomList()
    {
        openRoomListOn = false;
        AllOn.SetActive(!openRoomListOn);
        OpenOn.SetActive(openRoomListOn);
        DeleteRoomListUI();
        UpdateRoomCache(roomListDe);
        CreateRoomListUI();
    }

    public bool openRoomListOn;
    //��ư�� ������ ������ ������ ����� ����
    public void OpenRoomList()
    {
        openRoomListOn = true;
        OpenOn.SetActive(openRoomListOn);
        AllOn.SetActive(!openRoomListOn);
        DeleteRoomListUI();
        UpdateRoomCache(roomListDe);
        CreateRoomListUI();
    }

    public GameObject creatRoom;
    public void OnCreateRoom()
    {
        creatRoom.SetActive(true);
    }
}
