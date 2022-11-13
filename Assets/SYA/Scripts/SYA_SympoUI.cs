using RockVR.Video;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows.WebCam;
using Photon.Pun;
using Photon.Realtime;


namespace SYA_UI
{
    public class SYA_SympoUI : MonoBehaviourPunCallbacks
    {

        public GameObject window;
        public GameObject windowList;
        //move��ư On / Off
        public Text moveOnOffstr;
        public GameObject moveOnOff;

        public GameObject spaceButton;
        public GameObject userData;

        //�� �̵� ��ư
        public GameObject solButton;
        public GameObject conButton;
        public GameObject solSympoButton;
        public GameObject conSympoButton;

        //UserList
        public Text roomName;
        public Text roomOwner;
        public Text roomPassward;

        private void Awake()
        {
            //transform.parent = GameObject.Find("Canvas_DontDestroy").transform.GetChild(0).transform;
        }

        private void Update()
        {
            roomName.text = SYA_SymposiumManager.Instance.roomName;
            roomOwner.text = SYA_SymposiumManager.Instance.roomOwner;
            roomPassward.text = SYA_SymposiumManager.Instance.roomCode;


        }

        public void UwcOnOff()
        {
            window.SetActive(!window.activeSelf);
            windowList.SetActive(!windowList.activeSelf);
            //moveOnOff.SetActive(!moveOnOff.activeSelf);
        }

        public void UwcMoveOnOff()
        {
            //window.GetComponent<SYA_SympoWindowsMoving>().enabled = !window.GetComponent<SYA_SympoWindowsMoving>().enabled;
            //moveOnOffstr.text = $"MOVE : {window.GetComponent<SYA_SympoWindowsMoving>().enabled}";
        }

        //������ �÷��̾��� ����Ʈ�� ������Ʈ
        public Transform AudienceTran;
        public Transform PresenterTran;
        public void OnUserList()
        {
            userData.SetActive(!userData.activeSelf);
            foreach (KeyValuePair<string, string> userAuthority in SYA_SymposiumManager.Instance.playerAuthority)
            {
                if (userAuthority.Value == "Audience")//û���� ���
                {
                    GameObject go = PhotonNetwork.Instantiate("UserListItem", new Vector2(0, -15), Quaternion.identity);
                    go.transform.parent = AudienceTran;
                    go.transform.localPosition = new Vector2(180, -15);
                    go.GetComponentInChildren<Text>().text = userAuthority.Key;
                }
                else
                {
                    GameObject go = PhotonNetwork.Instantiate("UserListItem", new Vector2(0, -15), Quaternion.identity);
                    go.transform.parent = PresenterTran;
                    go.transform.localPosition = new Vector2(180, -15);
                    go.GetComponentInChildren<Text>().text = userAuthority.Key;
                }
            }
        }

        public void OnSpaceChange()
        {
            spaceButton.SetActive(!spaceButton.activeSelf);
            if (SceneManager.GetActiveScene().name == "SymposiumScene")
            {
                solButton.SetActive(true);
                conButton.SetActive(true);
                solSympoButton.SetActive(false);
                conSympoButton.SetActive(false);
            }
            else if (SceneManager.GetActiveScene().name == "KJH_SolarSystemScene")
            {
                solButton.SetActive(false);
                conButton.SetActive(true);
                solSympoButton.SetActive(true);
                conSympoButton.SetActive(false);
            }
            else if (SceneManager.GetActiveScene().name == "KYG_Scene")
            {
                solButton.SetActive(true);
                conButton.SetActive(false);
                solSympoButton.SetActive(false);
                conSympoButton.SetActive(true);
            }
        }

        public void SolarSystemChange()
        {
            /*List<string> player = SYA_SymposiumManager.Instance.playerName;
            for (int i = 0; i < player.Count; ++i)
            {
                if (!SYA_SymposiumManager.Instance.player[player[i]].IsMine)
                {
                    Destroy(SYA_SymposiumManager.Instance.playerObj[player[i]]);
                }
            }*/
            if (SceneManager.GetActiveScene().name.Contains("Sympo"))
            {
                SceneManager.LoadScene("KJH_SolarSystemScene");
                if (PhotonNetwork.MasterClient.UserId != SYA_SymposiumManager.Instance.player[PhotonNetwork.NickName].Owner.UserId)
                {
                    SYA_ChatManager.Instance.chatClient.Unsubscribe(new string[] { SYA_ChatManager.Instance.Constchannel, SYA_ChatManager.Instance.Lobbychannel });
                    SYA_ChatManager.Instance.chatClient.Subscribe(new string[] { SYA_ChatManager.Instance.Allchannel, SYA_ChatManager.Instance.Solarchannel });
                }
                SYA_ChatManager.Instance.currentChannel = SYA_ChatManager.Instance.Allchannel;
                //OnOffChsnge();
                solButton.SetActive(false);
                print("�ֶ� �ý������� �̵�");
            }
        }

        bool goStar;

        public void ConstellationChange()
        {
            /*            List<string> player = SYA_SymposiumManager.Instance.playerName;
                        for (int i=0; i< player.Count; ++i)
                        {
                            if(!SYA_SymposiumManager.Instance.player[player[i]].IsMine)
                            {
                                SYA_SymposiumManager.Instance.playerObj[player[i]].*//*transform.GetChild(0).gameObject.*//*SetActive(false);
                            }
                        }*/
            if (SceneManager.GetActiveScene().name.Contains("Sympo"))
            {
                goStar = true;
                currentRoomName = PhotonNetwork.CurrentRoom.Name;
                PhotonNetwork.LeaveRoom();
                if (PhotonNetwork.MasterClient.UserId != SYA_SymposiumManager.Instance.player[PhotonNetwork.NickName].Owner.UserId)
                {
                    SYA_ChatManager.Instance.chatClient.Unsubscribe(new string[] { SYA_ChatManager.Instance.Lobbychannel, SYA_ChatManager.Instance.Solarchannel });
                    SYA_ChatManager.Instance.chatClient.Subscribe(new string[] { SYA_ChatManager.Instance.Allchannel, SYA_ChatManager.Instance.Constchannel });
                }
                SYA_ChatManager.Instance.currentChannel = SYA_ChatManager.Instance.Allchannel;
                //OnOffChsnge();
                conButton.SetActive(false);
                print("���ڸ��� �̵�");
            }
        }

        string currentRoomName;

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
            if (goStar)
            {
                TypedLobby typed = new TypedLobby("C2", LobbyType.Default);
                PhotonNetwork.JoinLobby(typed);
            }
            else
            {
                TypedLobby typed = new TypedLobby("C1", LobbyType.Default);
                PhotonNetwork.JoinLobby(typed);
            }
        }

        public override void OnJoinedLobby()
        {
            base.OnJoinedLobby();
            if (goStar)
            {
                JoinRoom("star" + currentRoomName);
            }
            else
            {
                JoinRoom(currentRoomName);
            }
        }

        void CreateRoom()
        {
            //�� �ɼ� ����
            RoomOptions roomOptions = new RoomOptions();

            //�ִ� �ο�(0���̸� �ִ��ο�)
            roomOptions.MaxPlayers = 10;
            //�� ��Ͽ� ���̳�? ������ �ʴ���?
            roomOptions.IsVisible = true;
            //���� �����.
            PhotonNetwork.CreateRoom("star" + currentRoomName, roomOptions, TypedLobby.Default);
        }
        //�� ���� �Ϸ�
        public override void OnCreatedRoom()
        {
            base.OnCreatedRoom();
            print(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }
        //�� ���� ����
        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            base.OnCreateRoomFailed(returnCode, message);
            print("OnCreatedRoomFailed, " + returnCode + ", " + message);
        }
        //������ ��û
        public void JoinRoom(string roomName)
        {

            PhotonNetwork.JoinRoom(roomName);

            //PhotonNetwork.JoinRoom : ������ �濡 �� ��
            //PhotonNetwork.JoinOrCreateRoom : ���̸� �����ؼ� ������ �� �� �ش��̸����� �� ���� ���ٸ� �� �̸����� �� ���� �� ����
            //PhotonNetwork.JoinRandomOrCreateRoom: �������� ������ �� ��, ���ǿ� �´� ���� ���ٸ� ���� ���� ������ ����
            //PhotonNetwork.JoinRandomRoom : �����ѹ� ����
        }

        //�� ������ �������� ��
        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            print("OnJoinedRoom");
            if (goStar)
                PhotonNetwork.LoadLevel("KYG_Scene");
            else
                PhotonNetwork.LoadLevel("SymposiumScene");
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            //base.OnJoinRoomFailed(returnCode, message);
            //print("OnJoinRoomFailed, " + returnCode + ", " + message);
            CreateRoom();
        }

        public void SymposiumChsnge()
        {
            if (SceneManager.GetActiveScene().name.Contains("KYG"))
            {
                goStar = false;
                PhotonNetwork.LeaveRoom();
            }
            else
            {
                SceneManager.LoadScene("SymposiumScene");
            }

            if (PhotonNetwork.MasterClient.UserId != SYA_SymposiumManager.Instance.player[PhotonNetwork.NickName].Owner.UserId)
            {
                SYA_ChatManager.Instance.chatClient.Unsubscribe(new string[] { SYA_ChatManager.Instance.Constchannel, SYA_ChatManager.Instance.Solarchannel });
                SYA_ChatManager.Instance.chatClient.Subscribe(new string[] { SYA_ChatManager.Instance.Allchannel, SYA_ChatManager.Instance.Lobbychannel });
            }
            SYA_ChatManager.Instance.currentChannel = SYA_ChatManager.Instance.Allchannel;

        }

        void OnOffChsnge()
        {
            spaceButton.SetActive(!spaceButton.activeSelf);
        }

        bool isRecording;
        public Text recordeOnOff;
        //��ư�� ������ ��,
        public void Recording()
        {
            //��ȭ���̶�� 
            if (isRecording)
            {
                //VideoCapture.
                //��ȭ�� ������
                VideoCaptureCtrl.instance.StopCapture();
                recordeOnOff.text = "Off";
                //�����Ѵ�
                isRecording = false;
            }
            //��ȭ���� �ƴ϶��
            else
            {
                //��ȭ�� �����Ѵ�
                VideoCaptureCtrl.instance.StartCapture();
                recordeOnOff.text = "On";
                isRecording = true;
            }

            /*        //��ȭ ����
                    VideoCaptureCtrl.instance.StartCapture();

                    //��ȭ ����
                    VideoCaptureCtrl.instance.StopCapture();

                    //�Ͻ� ���� �� �̾� ����
                    VideoCaptureCtrl.instance.ToggleCapture();

                    //��� �غ�
                    VideoPlayer.instance.SetRootFolder();
                    //���
                    VideoPlayer.instance.PlayVideo();

                    VideoPlayer.instance.NextVideo();*/
        }

        //UserList
    }
}