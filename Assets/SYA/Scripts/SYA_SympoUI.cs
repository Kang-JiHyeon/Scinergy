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

        // [지현]
        // 현재인원/최대인원
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

            // ����
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

        //������ �÷��̾��� ����Ʈ�� ������Ʈ
        public Transform AudienceTran;
        public Transform PresenterTran;
        public Image btn_user;
        public Sprite userOn;
        public Sprite userOff;

        public void OnUserList()
        {

            if (!userData.activeSelf)
            {
                userData.SetActive(true);
                btn_user.sprite = userOn;
            }
            else
            {
                userData.SetActive(false);
                btn_user.sprite = userOff;
            }


            // ����
            // ������ �÷��̾� ����Ʈ�� clear
            ClearUserList(AudienceTran);
            ClearUserList(PresenterTran);

            foreach (KeyValuePair<string, string> userAuthority in SYA_SymposiumManager.Instance.playerAuthority)
            {
                if (userAuthority.Value == "Audience")//û���� ����
                {
                    //GameObject go = PhotonNetwork.Instantiate("UserListItem", new Vector2(0, -15), Quaternion.identity);
                    GameObject go = PhotonNetwork.Instantiate("UserListItem 1", new Vector2(0, -15), Quaternion.identity);
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
        bool goSympo;//영아


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
                goSympo = false;//영아
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
            else if(goSympo)//영아
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
            else if (goSympo)//영아
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
            //�� ���Ͽ� ���̳�? ������ �ʴ���?
            roomOptions.IsVisible = true;
            //���� ������.
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

            //PhotonNetwork.JoinRoom : ������ �濡 ��� ��
            //PhotonNetwork.JoinOrCreateRoom : ���̸� �����ؼ� ������� �� �� �ش��̸����� �� ���� ���ٸ� �� �̸����� �� ���� �� ����
            //PhotonNetwork.JoinRandomOrCreateRoom: �������� ������� �� ��, ���ǿ� �´� ���� ���ٸ� ���� ���� ������ ����
            //PhotonNetwork.JoinRandomRoom : �����ѹ� �����
        }

        //�� ������ �������� ��
        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            print("OnJoinedRoom");
            if (goStar)
                PhotonNetwork.LoadLevel("KYG_Scene");
            else if (goSympo)//영아
                PhotonNetwork.LoadLevel("AvatarSympo");
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
                goSympo = true;
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



        // UserList Clear
        void ClearUserList(Transform tr)
        {
            for (int i = 0; i < tr.childCount; i++)
            {
                Destroy(tr.GetChild(i).gameObject);
            }
        }

        // �÷��̾� ������ player ���� ���� ����Ʈ �� ���ųʸ� ������Ʈ
        // ������ ������.... �ٸ� ������ ���� ���ݾ�? �׻����� ��ǥ�� ���� ������ �־��� ��
        // �� ���쿡�� ���� ���ųʸ��� �ʱ�ȭ�ؾ��ϴ°�?

        // 플레이어 들어왔을 때 실행되는 이벤트 함수
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            text_user.text = string.Format("{0:D2}", PhotonNetwork.PlayerList.Length) + "/20";
        }

        // 플레이어 나갈 때 실행되는 이벤트 함수
        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            text_user.text = string.Format("{0:D2}", PhotonNetwork.PlayerList.Length) + "/20";

            SYA_SymposiumManager.Instance.playerAuthority.Remove(otherPlayer.NickName);
            //SYA_SymposiumManager.Instance.playerName.Remove(SYA_SymposiumManager.Instance.playerName.Find(x => x == otherPlayer.NickName));

            // ������ ������ �����Ͷ���
            if (otherPlayer.IsMasterClient)
            {
                // ���� ���ųʸ����� �����ϰ� ��
                SYA_SymposiumManager.Instance.playerAuthority.Remove(otherPlayer.NickName);
            }
        }

        // �����Ͱ� ������ �� ȣ���Ǵ� �̺�Ʈ
        public override void OnMasterClientSwitched(Player otherPlayer)
        {
            SYA_SymposiumManager.Instance.playerAuthority[otherPlayer.NickName] = "Presenter";
        }
    }
}
