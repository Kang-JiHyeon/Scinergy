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
            moveOnOff.SetActive(!moveOnOff.activeSelf);
        }

        public void UwcMoveOnOff()
        {
            window.GetComponent<SYA_SympoWindowsMoving>().enabled = !window.GetComponent<SYA_SympoWindowsMoving>().enabled;
            moveOnOffstr.text = $"MOVE : {window.GetComponent<SYA_SympoWindowsMoving>().enabled}";
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
            print("�ֶ� �ý������� �̵�");
        }

        public void ConstellationChange()
        {
            SceneManager.LoadScene("KYG_Scene");
            OnOffChsnge();
            print("���ڸ��� �̵�");
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