using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class SYA_SympoRoomItem : MonoBehaviourPunCallbacks
{
    //�� �̸�
    public Text roomName;
    //�� ������
    public Text ownerName;
    //���� �ο�
    public Text playerCount;
    //���� �� ����� ����
    public GameObject unpubl;

    //text
    public GameObject password;
    public InputField one;
    public string roomPassword;


    //����
    RoomInfo roomInfo;
    public void SetInfo(RoomInfo info)
    {
        roomInfo = info;
        string roomName_ = info.CustomProperties["room_name"].ToString();
        name = roomName_;

        //���̸� (0/0)
        roomName.text = roomName_;
        playerCount.text = $"{info.PlayerCount}�� ���� ��";
        ownerName.text = info.CustomProperties["owner"].ToString();
        roomPassword = info.CustomProperties["password"].ToString(); ;
        unpubl.SetActive(info.CustomProperties["public"].ToString() == "False");
    }


    public void OffPassward()
    {
        password.SetActive(false);
        btn_Join.sprite = joinOff;
    }

    public Image btn_Join;
    public Sprite joinOn;
    public Sprite joinOff;
    public void JoinRoom()
    {
        if (!unpubl.activeSelf)
        {
            PhotonNetwork.JoinRoom(name);
        }
        else
        {
            password.SetActive(true);
            password.transform.parent = GameObject.Find("Canvas").transform;
            password.transform.localPosition = Vector2.zero;
        }
        btn_Join.sprite = joinOn;
    }

    public void passwordJoin()
    {
        string inputPassword = $"{one.text}";
        if (inputPassword == roomPassword)
        {
            PhotonNetwork.JoinRoom(name);
        }
        else
        {
            print("�ٽ� �Է��ϼ���");
        }
    }
}
