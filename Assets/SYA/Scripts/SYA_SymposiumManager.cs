using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Voice.PUN;
using SYA_UserInfoManagerSaveLoad;

public class SYA_SymposiumManager : MonoBehaviourPun
{
    public static SYA_SymposiumManager Instance;
    //방의 정보를 관할 하고, 참여자 관리자

    //플레이어 생성 시 리스트에 추가
    //플레이어 리스트에 맞추어 유저 리스트 스크롤 뷰에 아이템 생성하여 보여주기 
    //권한
    public Dictionary<string, string> playerAuthority = new Dictionary<string, string>();
    //포톤뷰
    public Dictionary<string, PhotonView> player = new Dictionary<string, PhotonView>();
    //유저이름 리스트
    public List<string> playerName = new List<string>();
    //포톤보이스
    public Dictionary<string, AudioSource> playerVoice = new Dictionary<string, AudioSource>();
    //방이름 방장 입장코드
    public string roomName, roomOwner, roomCode;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
      PhotonNetwork.Instantiate(SYA_UserInfoManager.Instance.Avatar, new Vector3(0, 5.5f, 1), Quaternion.identity);
    }

    public PhotonView GetMyPlayer()
    {
        foreach(PhotonView val in player.Values)
        {
            if(val.IsMine)
            {
                return val;
            }
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        roomName = PhotonNetwork.CurrentRoom.Name;
        roomOwner = PhotonNetwork.CurrentRoom.CustomProperties["owner"].ToString();
        roomCode = PhotonNetwork.CurrentRoom.CustomProperties["password"].ToString();
        
    }

    public void PlayerNameAuthority(string name, PhotonView photonView, bool master, AudioSource audioSource)
    {
        playerName.Add(name);
        player[name] = photonView;
        if (master)//만약 마스터 클라이언트라면
            playerAuthority[name] = "Presenter";
        else //아니라면
            playerAuthority[name] = "Audience";
        playerVoice[name] = audioSource;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(player);
            stream.SendNext(playerAuthority);
            stream.SendNext(playerVoice);
        }

        else
        {
            player = (Dictionary<string, PhotonView>)stream.ReceiveNext();
            playerAuthority = (Dictionary<string, string>)stream.ReceiveNext();
            playerVoice = (Dictionary<string, AudioSource>)stream.ReceiveNext();
        }
    }
}
