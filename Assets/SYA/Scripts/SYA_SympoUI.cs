using RockVR.Video;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows.WebCam;
using Photon.Pun;

namespace SYA_UI
{
    public class SYA_SympoUI : MonoBehaviourPun
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
            moveOnOff.SetActive(!moveOnOff.activeSelf);
        }

        public void UwcMoveOnOff()
        {
            window.GetComponent<SYA_SympoWindowsMoving>().enabled = !window.GetComponent<SYA_SympoWindowsMoving>().enabled;
            moveOnOffstr.text = $"MOVE : {window.GetComponent<SYA_SympoWindowsMoving>().enabled}";
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
            else if (SceneManager.GetActiveScene().name == "KJH_RevolutionScene")
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
            SceneManager.LoadScene("KJH_RevolutionScene");
            OnOffChsnge();
            print("솔라 시스템으로 이동");
        }

        public void ConstellationChange()
        {
            SceneManager.LoadScene("KYG_Scene");
            OnOffChsnge();
            print("별자리로 이동");
        }

        public void SymposiumChsnge()
        {
            SceneManager.LoadScene("SymposiumScene");
            OnOffChsnge();
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