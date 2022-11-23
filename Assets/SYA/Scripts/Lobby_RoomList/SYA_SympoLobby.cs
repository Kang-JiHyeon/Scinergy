using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class SYA_SympoLobby : MonoBehaviourPunCallbacks
{
    public static SYA_SympoLobby Instance;

    //방 만들기
    //방생성 페이지
    public GameObject roomCreate;
    //방확인 페이지
    public GameObject roomCompletion;
    //방이름 InputField
    public InputField inputRoomName;
    //방참가 Button
    public Button btnJoin;
    //방생성 Button
    public Button btnCreate;
    //공개여부
    bool public_;
    public GameObject publicText;
    public GameObject falseC;
    public GameObject trueC;

    //안내문구 
    public GameObject characterlimit;

    //Thumbnail
    public Image Thumbnails;
    //썸네일 소스 정보
    public RawImage Thumbnails_;
    public Texture2D Thumbnails_sp;

    //방 만들기 전 정보 확인
    public Text title;
    public Text title_count;
    public Text owner;
    public Text password;

    //썸네일 선택창
    public GameObject thumbnail;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // 방이름(InputField)이 변경될때 호출되는 함수 등록
        inputRoomName.onValueChanged.AddListener(OnRoomNameValueChanged);
    }
    private void Update()
    {
        title_count.text = $"{inputRoomName.text.Length} / 20";
    }

    public void OnRoomNameValueChanged(string s)
    {
        //생성
        btnCreate.interactable = s.Length > 0;
    }

    //썸네일 누르면 목록 열림
    public void OntTumbnailList()
    {
        thumbnail.SetActive(!thumbnail.activeSelf);
    }

    public Image btn_creat;
    public Sprite createOff;
    public void offroomCo()
    {
        roomCompletion.SetActive(false);

        btn_creat.sprite = createOff;

    }

    public bool custom;
    public string path;
    byte[] byteTexture;

    //생성을 누를 경우
    public void OnClickCreate()
    {
        if (inputRoomName.text == "") return;
        //Thumbnails_sp = Thumbnails_.sprite;
        //만약 썸네일의 텍스쳐정보가 있다면
        if (Thumbnails_.texture != null)
        {
            if (!custom)
            {
                byteTexture = File.ReadAllBytes($"Assets\\Resources\\Thumbnails\\{Thumbnails_.texture.name}.jpg");
            }
            else
            {
                byteTexture = File.ReadAllBytes(path);
            }
        }
        //→넘을 경우 안내 문구 등장
        characterlimit.SetActive(inputRoomName.text.Length > 20);
        //→안 념을 경우 확인 창On
        roomCreate.SetActive(inputRoomName.text.Length > 20);
        roomCompletion.SetActive(!(inputRoomName.text.Length > 20));
        UserList[0] = PhotonNetwork.NickName;
        CreateRoom();
    }

    public void offRoommake()
    {
        roomCreate.SetActive(false);
        btn_creat.sprite = createOff;
    }

    public GameObject publ;
    public GameObject Unpubl;
    //방 공개 여부 클릭
    public void OnClickPublic()
    {
        public_ = !public_;
        publ.SetActive(!public_);
        Unpubl.SetActive(public_);
        publicText.SetActive(!public_);
    }

    // 방 옵션을 설정
    RoomOptions roomOptions = new RoomOptions();
    //방 생성
    public void CreateRoom()
    {
        custom = false;
        // 최대 인원 (0이면 최대인원)
        roomOptions.MaxPlayers = 20;
        // 룸 리스트에 보이지 않게? 보이게?
        roomOptions.IsVisible = true;
        // custom 정보를 셋팅
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash["room_name"] = inputRoomName.text;
        hash["owner"] = PhotonNetwork.NickName;
        hash["public"] = public_;
        hash["password"] = $"{Random.Range(0, 10)}{Random.Range(0, 10)}{Random.Range(0, 10)}{Random.Range(0, 10)}";
        //유저목록
        hash["UserList"] = UserList;
        //썸네일 파일 위치와 이름 
        hash["Thumbnail"] = byteTexture;
        roomOptions.CustomRoomProperties = hash;
        //커스텀 정보 공개 설정
        roomOptions.CustomRoomPropertiesForLobby = new string[] { "room_name", "owner", "public", "password", "UserList", "Thumbnail" };

        //방 정보 창에 띄우기
        OnRoomInfo();
    }

    public string[] UserList = new string[1];

    //방 정보 창에 띄우기
    public void OnRoomInfo()
    {
        title.text = roomOptions.CustomRoomProperties["room_name"].ToString();
        owner.text = roomOptions.CustomRoomProperties["owner"].ToString();
        password.text = roomOptions.CustomRoomProperties["password"].ToString();
        //만약 썸네일의 텍스쳐정보가 있다면
        if (Thumbnails_.texture != null)
        {
            Texture2D texture2D = new Texture2D(0, 0);
            if (((byte[])roomOptions.CustomRoomProperties["Thumbnail"]).Length > 0)//정보가 있으면->받아온 값이 있으면
            {
                texture2D.LoadImage((byte[])roomOptions.CustomRoomProperties["Thumbnail"]);
            }
            Thumbnails.sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0, 0));
        }
    }

    public void OnJoint()
    {
        // 방 생성 요청 (해당 옵션을 이용해서)
        PhotonNetwork.CreateRoom(inputRoomName.text, roomOptions);
    }

    //방이 생성되면 호출 되는 함수
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print("OnCreatedRoom");
    }

    //방 생성이 실패 될때 호출 되는 함수
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        print("OnCreateRoomFailed , " + returnCode + ", " + message);
    }

    //방 참가가 완료 되었을 때 호출 되는 함수
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("OnJoinedRoom");
        PhotonNetwork.LoadLevel("AvatarSympo");
    }
}
