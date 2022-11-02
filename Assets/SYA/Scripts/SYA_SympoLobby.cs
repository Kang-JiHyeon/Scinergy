using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class SYA_SympoLobby : MonoBehaviourPunCallbacks
{
    //�� �����
    //����� ������
    public GameObject roomCreate;
    //��Ȯ�� ������
    public GameObject roomCompletion;
    //���̸� InputField
    public InputField inputRoomName;
    //������ Button
    public Button btnJoin;
    //����� Button
    public Button btnCreate;
    //��������
    bool public_;
    public GameObject publicText;
    public GameObject falseC;
    public GameObject trueC;

    //�ȳ����� 
    public GameObject characterlimit;

    //���� ������   
    Dictionary<string, RoomInfo> roomCache = new Dictionary<string, RoomInfo>();
    //�� ����Ʈ Content
    public Transform trListContent;

    //Thumbnail
    public GameObject[] Thumbnails;

    //�� ����� �� ���� Ȯ��
    public Text title;
    public Text owner;
    public Text password;

    void Start()
    {
        // ���̸�(InputField)�� ����ɶ� ȣ��Ǵ� �Լ� ���
        inputRoomName.onValueChanged.AddListener(OnRoomNameValueChanged);
    }

    public void OnRoomNameValueChanged(string s)
    {
        //����
        btnCreate.interactable = s.Length > 0;
    }

    //������ ���� ���
    public void OnClickCreate()
    {
        //����� ��� �ȳ� ���� ����
        characterlimit.SetActive(inputRoomName.text.Length > 20);
        //��� ���� ��� Ȯ�� âOn
        roomCreate.SetActive(inputRoomName.text.Length > 20);
        roomCompletion.SetActive(!(inputRoomName.text.Length > 20));
        CreateRoom();
    }

    //�� ���� ���� Ŭ��
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

        // �� �ɼ��� ����
        RoomOptions roomOptions = new RoomOptions();
    //�� ����
    public void CreateRoom()
    {
        // �ִ� �ο� (0�̸� �ִ��ο�)
        roomOptions.MaxPlayers = 20;
        // �� ����Ʈ�� ������ �ʰ�? ���̰�?
        roomOptions.IsVisible = true;
        // custom ������ ����
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash["room_name"] = inputRoomName.text;
        hash["owner"] = PhotonNetwork.NickName;
        hash["public"] = public_;
        hash["password"] = $"{Random.Range(0,10)}{Random.Range(0, 10)}{Random.Range(0, 10)}{Random.Range(0, 10)}";
        roomOptions.CustomRoomProperties = hash;
        //Ŀ���� ���� ���� ����
        roomOptions.CustomRoomPropertiesForLobby = new string[] { "room_name", "owner", "public", "password" };

        //�� ���� â�� ����
        OnRoomInfo();
    }

        //�� ���� â�� ����
    public void OnRoomInfo()
    {
        title.text = roomOptions.CustomRoomProperties["room_name"].ToString();
        owner.text= roomOptions.CustomRoomProperties["owner"].ToString();
        password.text= roomOptions.CustomRoomProperties["password"].ToString();
    }

    public void OnJoint()
    {
        // �� ���� ��û (�ش� �ɼ��� �̿��ؼ�)
        PhotonNetwork.CreateRoom(inputRoomName.text, roomOptions);
    }

    //���� �����Ǹ� ȣ�� �Ǵ� �Լ�
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print("OnCreatedRoom");
    }

    //�� ������ ���� �ɶ� ȣ�� �Ǵ� �Լ�
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        print("OnCreateRoomFailed , " + returnCode + ", " + message);
    }

/*    //�� ���� ��û
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(inputRoomName.text + inputPassword.text);
    }*/

    //�� ������ �Ϸ� �Ǿ��� �� ȣ�� �Ǵ� �Լ�
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("OnJoinedRoom");
        PhotonNetwork.LoadLevel("SymposiumScene");
    }
/*
    //�� ������ ���� �Ǿ��� �� ȣ�� �Ǵ� �Լ�
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        print("OnJoinRoomFailed, " + returnCode + ", " + message);
    }

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
            RoomItem item = go.GetComponent<RoomItem>();
            item.SetInfo(info);

            //��ư ������ ȣ��Ǵ� �Լ�
            //���ٽ�
            //item.onClickAction = (room) => { inputRoomName.text = room; };
            item.onClickAction = SetRoomName;

            string desc = (string)info.CustomProperties["desc"];
            int map_id = (int)info.CustomProperties["map_id"];
            print(desc + map_id);
        }
    }

    //���� ����� id
    int prevmapId = -1;
    void SetRoomName(string room, int map_id)
    {
        //���̸�����
        inputRoomName.text = room;
        //������ ������� Ȱ��ȭ �Ǿ��ִٸ�
        if (prevmapId > -1)
        {
            //������ ����� ��Ȱ��ȭ
            mapThumbnails[prevmapId].SetActive(false);
        }
        //�� ������ ����
        mapThumbnails[map_id].SetActive(true);
        //���� �� ���̵� ����
        prevmapId = map_id;
    }*/

}
