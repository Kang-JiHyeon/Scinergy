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
        //move버튼 On / Off
        public Text moveOnOffstr;
        public GameObject moveOnOff;

        public GameObject spaceButton;
        public GameObject userData;

        //씬 이동 버튼
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

        //누르면 플레이어의 리스트를 업데이트
        public Transform AudienceTran;
        public Transform PresenterTran;
        public void OnUserList()
        {
            userData.SetActive(!userData.activeSelf);
            foreach (KeyValuePair<string, string> userAuthority in SYA_SymposiumManager.Instance.playerAuthority)
            {
                if (userAuthority.Value == "Audience")//청중일 경우
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
                print("솔라 시스템으로 이동");
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
                print("별자리로 이동");
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
            //방 옵션 세팅
            RoomOptions roomOptions = new RoomOptions();

            //최대 인원(0명이면 최대인원)
            roomOptions.MaxPlayers = 10;
            //룸 목록에 보이냐? 보이지 않느냐?
            roomOptions.IsVisible = true;
            //방을 만든다.
            PhotonNetwork.CreateRoom("star" + currentRoomName, roomOptions, TypedLobby.Default);
        }
        //방 생성 완료
        public override void OnCreatedRoom()
        {
            base.OnCreatedRoom();
            print(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }
        //방 생성 실패
        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            base.OnCreateRoomFailed(returnCode, message);
            print("OnCreatedRoomFailed, " + returnCode + ", " + message);
        }
        //방입장 요청
        public void JoinRoom(string roomName)
        {

            PhotonNetwork.JoinRoom(roomName);

            //PhotonNetwork.JoinRoom : 선택한 방에 들어갈 때
            //PhotonNetwork.JoinOrCreateRoom : 방이름 설정해서 들어가려고 할 때 해당이름으로 된 방이 없다면 그 이름으로 방 생성 후 입장
            //PhotonNetwork.JoinRandomOrCreateRoom: 랜덤방을 들어가려고 할 때, 조건에 맞는 방이 없다면 내가 방을 생성후 입장
            //PhotonNetwork.JoinRandomRoom : 랜덤한방 들어갈때
        }

        //방 입장이 성공했을 때
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
        //버튼을 눌렀을 때,
        public void Recording()
        {
            //녹화중이라면 
            if (isRecording)
            {
                //VideoCapture.
                //녹화를 끝내고
                VideoCaptureCtrl.instance.StopCapture();
                recordeOnOff.text = "Off";
                //저장한다
                isRecording = false;
            }
            //녹화중이 아니라면
            else
            {
                //녹화를 시작한다
                VideoCaptureCtrl.instance.StartCapture();
                recordeOnOff.text = "On";
                isRecording = true;
            }

            /*        //녹화 시작
                    VideoCaptureCtrl.instance.StartCapture();

                    //녹화 종료
                    VideoCaptureCtrl.instance.StopCapture();

                    //일시 정지 및 이어 시작
                    VideoCaptureCtrl.instance.ToggleCapture();

                    //재생 준비
                    VideoPlayer.instance.SetRootFolder();
                    //재생
                    VideoPlayer.instance.PlayVideo();

                    VideoPlayer.instance.NextVideo();*/
        }

        //UserList
    }
}