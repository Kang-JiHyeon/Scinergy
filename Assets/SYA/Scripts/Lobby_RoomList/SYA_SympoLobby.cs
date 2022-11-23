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

    //Thumbnail
    public Image Thumbnails;
    //����� �ҽ� ����
    public RawImage Thumbnails_;
    public Texture2D Thumbnails_sp;

    //�� ����� �� ���� Ȯ��
    public Text title;
    public Text title_count;
    public Text owner;
    public Text password;

    //����� ����â
    public GameObject thumbnail;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // ���̸�(InputField)�� ����ɶ� ȣ��Ǵ� �Լ� ���
        inputRoomName.onValueChanged.AddListener(OnRoomNameValueChanged);
    }
    private void Update()
    {
        title_count.text = $"{inputRoomName.text.Length} / 20";
    }

    public void OnRoomNameValueChanged(string s)
    {
        //����
        btnCreate.interactable = s.Length > 0;
    }

    //����� ������ ��� ����
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

    //������ ���� ���
    public void OnClickCreate()
    {
        if (inputRoomName.text == "") return;
        //Thumbnails_sp = Thumbnails_.sprite;
        //���� ������� �ؽ��������� �ִٸ�
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
        //����� ��� �ȳ� ���� ����
        characterlimit.SetActive(inputRoomName.text.Length > 20);
        //��� ���� ��� Ȯ�� âOn
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
    //�� ���� ���� Ŭ��
    public void OnClickPublic()
    {
        public_ = !public_;
        publ.SetActive(!public_);
        Unpubl.SetActive(public_);
        publicText.SetActive(!public_);
    }

    // �� �ɼ��� ����
    RoomOptions roomOptions = new RoomOptions();
    //�� ����
    public void CreateRoom()
    {
        custom = false;
        // �ִ� �ο� (0�̸� �ִ��ο�)
        roomOptions.MaxPlayers = 20;
        // �� ����Ʈ�� ������ �ʰ�? ���̰�?
        roomOptions.IsVisible = true;
        // custom ������ ����
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash["room_name"] = inputRoomName.text;
        hash["owner"] = PhotonNetwork.NickName;
        hash["public"] = public_;
        hash["password"] = $"{Random.Range(0, 10)}{Random.Range(0, 10)}{Random.Range(0, 10)}{Random.Range(0, 10)}";
        //�������
        hash["UserList"] = UserList;
        //����� ���� ��ġ�� �̸� 
        hash["Thumbnail"] = byteTexture;
        roomOptions.CustomRoomProperties = hash;
        //Ŀ���� ���� ���� ����
        roomOptions.CustomRoomPropertiesForLobby = new string[] { "room_name", "owner", "public", "password", "UserList", "Thumbnail" };

        //�� ���� â�� ����
        OnRoomInfo();
    }

    public string[] UserList = new string[1];

    //�� ���� â�� ����
    public void OnRoomInfo()
    {
        title.text = roomOptions.CustomRoomProperties["room_name"].ToString();
        owner.text = roomOptions.CustomRoomProperties["owner"].ToString();
        password.text = roomOptions.CustomRoomProperties["password"].ToString();
        //���� ������� �ؽ��������� �ִٸ�
        if (Thumbnails_.texture != null)
        {
            Texture2D texture2D = new Texture2D(0, 0);
            if (((byte[])roomOptions.CustomRoomProperties["Thumbnail"]).Length > 0)//������ ������->�޾ƿ� ���� ������
            {
                texture2D.LoadImage((byte[])roomOptions.CustomRoomProperties["Thumbnail"]);
            }
            Thumbnails.sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0, 0));
        }
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

    //�� ������ �Ϸ� �Ǿ��� �� ȣ�� �Ǵ� �Լ�
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("OnJoinedRoom");
        PhotonNetwork.LoadLevel("AvatarSympo");
    }
}
