using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class SYA_SympoRoomItem : MonoBehaviourPunCallbacks
{
    //방 이름
    public Text roomName;
    //방 생성자
    public Text ownerName;
    //참여 인원
    public Text playerCount;
    //공개 및 비공개 여부
    public GameObject unpubl;
    //썸네일
    public Image thumbnail;

    //text
    public GameObject password;
    public InputField one;
    public string roomPassword;

    //사진
    RoomInfo roomInfo;
    public void SetInfo(RoomInfo info)
    {
        roomInfo = info;
        string roomName_ = info.CustomProperties["room_name"].ToString();
        name = roomName_;

        //방이름 (0/0)
        roomName.text = roomName_;
        playerCount.text = $"{info.PlayerCount}명 참여 중";
        ownerName.text = info.CustomProperties["owner"].ToString();
        roomPassword = info.CustomProperties["password"].ToString(); ;
        unpubl.SetActive(info.CustomProperties["public"].ToString() == "False");
        Texture2D texture2D = new Texture2D(0, 0);
        if (info.CustomProperties["Thumbnail"] != null)
        {
            if (((byte[])info.CustomProperties["Thumbnail"]).Length > 0)//정보가 있으면->받아온 값이 있으면
            {
                texture2D.LoadImage((byte[])info.CustomProperties["Thumbnail"]);
            }
        }
        thumbnail.sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0, 0));

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
            print("다시 입력하세요");
        }
    }
}
