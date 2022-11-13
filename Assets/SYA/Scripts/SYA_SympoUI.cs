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

        // [지현]
        // 현재 인원 / 최대 인원
        public Text text_user;

        private void Awake()
        {
            //transform.parent = GameObject.Find("Canvas_DontDestroy").transform.GetChild(0).transform;
        }

        private void Update()
        {
            roomName.text = SYA_SymposiumManager.Instance.roomName;
            roomOwner.text = SYA_SymposiumManager.Instance.roomOwner;
            roomPassward.text = SYA_SymposiumManager.Instance.roomCode;

            // 지현
            text_user.text = string.Format("{0:D2}", PhotonNetwork.PlayerList.Length) + "/20";

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

            // 지현
            // 기존의 플레이어 리스트를 clear
            ClearUserList(AudienceTran);
            ClearUserList(PresenterTran);

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
            List<string> player = SYA_SymposiumManager.Instance.playerName;
            for (int i = 0; i < player.Count; ++i)
            {
                if (!SYA_SymposiumManager.Instance.player[player[i]].IsMine)
                {
                    Destroy(SYA_SymposiumManager.Instance.playerObj[player[i]]);
                }
            }
            SceneManager.LoadScene("KJH_SolarSystemScene");
            if (PhotonNetwork.MasterClient.UserId != SYA_SymposiumManager.Instance.player[PhotonNetwork.NickName].Owner.UserId)
            {
                SYA_ChatManager.Instance.chatClient.Unsubscribe(new string[] { SYA_ChatManager.Instance.Constchannel, SYA_ChatManager.Instance.Lobbychannel });
                SYA_ChatManager.Instance.chatClient.Subscribe(new string[] { SYA_ChatManager.Instance.Allchannel, SYA_ChatManager.Instance.Solarchannel });
            }
            SYA_ChatManager.Instance.currentChannel = SYA_ChatManager.Instance.Allchannel;
            //OnOffChsnge();
            print("솔라 시스템으로 이동");
        }

        public void ConstellationChange()
        {
            List<string> player = SYA_SymposiumManager.Instance.playerName;
            for (int i=0; i< player.Count; ++i)
            {
                if(!SYA_SymposiumManager.Instance.player[player[i]].IsMine)
                {
                    SYA_SymposiumManager.Instance.playerObj[player[i]]./*transform.GetChild(0).gameObject.*/SetActive(false);
                }
            }
            PhotonNetwork.LoadLevel("KYG_Scene");
            if (PhotonNetwork.MasterClient.UserId != SYA_SymposiumManager.Instance.player[PhotonNetwork.NickName].Owner.UserId)
            {
                SYA_ChatManager.Instance.chatClient.Unsubscribe(new string[] { SYA_ChatManager.Instance.Lobbychannel, SYA_ChatManager.Instance.Solarchannel });
                SYA_ChatManager.Instance.chatClient.Subscribe(new string[] { SYA_ChatManager.Instance.Allchannel, SYA_ChatManager.Instance.Constchannel });
            }
            SYA_ChatManager.Instance.currentChannel = SYA_ChatManager.Instance.Allchannel;
            //OnOffChsnge();
            print("별자리로 이동");
        }

        public void SymposiumChsnge()
        {
            SceneManager.LoadScene("SymposiumScene");
            //OnOffChsnge();
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

        // UserList Clear
        void ClearUserList(Transform tr)
        {
            for(int i=0; i< tr.childCount; i++)
            {
                Destroy(tr.GetChild(i).gameObject);
            }
        }

        // 플레이어 나가면 player 정보 관련 리스트 및 딕셔너리 업데이트
        // 방장이 나가면.... 다른 사람이 권한 받잖아? 그사람이 발표자 권한 가지고 있어야 함
        // 이 경우에도 권한 딕셔너리를 초기화해야하는가?

        // 애초에 플레이어 관련 정보를 받는 방법을 바꾸는 방법은 어떠한가
        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            SYA_SymposiumManager.Instance.playerAuthority.Remove(otherPlayer.NickName);
            //SYA_SymposiumManager.Instance.playerName.Remove(SYA_SymposiumManager.Instance.playerName.Find(x => x == otherPlayer.NickName));

            // 나가는 사람이 마스터라면
            if (otherPlayer.IsMasterClient)
            {
                // 권한 딕셔너리에서 제거하고 감
                SYA_SymposiumManager.Instance.playerAuthority.Remove(otherPlayer.NickName);
            }
        }
        
        // 마스터가 변경된 후 호출되는 이벤트
        public override void OnMasterClientSwitched(Player otherPlayer)
        {
            SYA_SymposiumManager.Instance.playerAuthority[otherPlayer.NickName] = "Presenter";
        }
    }
}