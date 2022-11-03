using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class SYA_SympoRoomItem : MonoBehaviour
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
    public InputField two;
    public InputField three;
    public InputField four;
    public string roomPassword;


    //����

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetInfo(RoomInfo info)
    {
        string roomName_ = info.CustomProperties["room_name"].ToString();
        name = roomName_;

        //���̸� (0/0)
        roomName.text = roomName_;
        playerCount.text = $"{info.PlayerCount}�� ���� ��";
        ownerName.text = info.CustomProperties["owner"].ToString();
        roomPassword= info.CustomProperties["password"].ToString(); ;
        unpubl.SetActive(info.CustomProperties["public"].ToString() == "False");
    }

    public void JoinRoom()
    {
        if(!unpubl.activeSelf)
        {
            PhotonNetwork.JoinRoom(name);
        }
        else
        {
            password.SetActive(true);
            password.transform.parent = GameObject.Find("Canvas").transform;
            password.transform.localPosition = Vector2.zero;
        }
    }

    public void passwordJoin()
    {
        string inputPassword =$"{one.text}{two.text}{three.text}{four.text}";
        if(inputPassword==roomPassword)
        {
            PhotonNetwork.JoinRoom(name);
        }
        else
        {
            print("�ٽ� �Է��ϼ���");
        }
    }
}
