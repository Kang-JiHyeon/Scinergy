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
    //유저의 플레이어 몸체
    //public Dictionary<string, GameObject> playerObj = new Dictionary<string, GameObject>();
    //방이름 방장 입장코드
    public string roomName, roomOwner, roomCode;

    private void Awake()
    {
        if (Instance == null)
        {
            //인스턴스에 나를 넣고
            Instance = this;
            //나를 씬이 전환이 되어도 파괴되지 않게 하겠다

            DontDestroyOnLoad(gameObject);
        }
        //그렇지 않으면
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        roomName = PhotonNetwork.CurrentRoom.Name;
        roomOwner = PhotonNetwork.CurrentRoom.CustomProperties["owner"].ToString();//Presenter
        roomCode = PhotonNetwork.CurrentRoom.CustomProperties["password"].ToString();
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

    public void PlayerNameAuthority(string name,PhotonView photonView, AudioSource audioSource)
    {
        //playerName.Add(name);
        player[name] = photonView;
        playerVoice[name] = audioSource;
        //playerObj[name] = gameObject;
    }

    public void PlayerAuthority(string name, bool master)
    {
        //if (master)//만약 마스터 클라이언트라면
        //    playerAuthority[name] = "Owner";
        //else //아니라면
        //    playerAuthority[name] = "Audience";
        player[PhotonNetwork.NickName].RPC("RPCPlayerAuthority", RpcTarget.All, name, master);

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
