using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using SYA_UserInfoManagerSaveLoad;

public class SYA_LobbyManager : MonoBehaviourPunCallbacks
{
    //�κ��(�α��οϷ� ��)�� ���� 
    //���� ĳ���� ����� ����
    //��ܿ� �� �г��Ӱ� ȯ���ϴ� ����(2�� �� �����)
    //play��ư ������ 1ä�� 1�� �� ����

    public GameObject nicnameBox;
    public Text nicknameW;
    float currentTime = 0;
    public float nameFadeTime = 2;

    List<string> roomList = new List<string>();
    string roomName;

    // Start is called before the first frame update
    void Start()
    {
        //���� ĳ���� ����� ����

        //PhotonNetwork.Instantiate("DB�� ����� �ƹ�Ÿ ����", Vector3.zero, Quaternion.identity);
        nicknameW.text = "Signed in as " + PhotonNetwork.NickName;
        OnClickJoinRoom();
    }

    // Update is called once per frame
    void Update()
    {
        /*        if(GameObject.Find("Player"))
                {
                    PhotonNetwork.Instantiate("Player", new Vector3(-0.1f, 6.5f, 0), Quaternion.LookRotation(Camera.main.transform.position));
                }*/
        //��ܿ� �� �г��Ӱ� ȯ���ϴ� ����(2�� �� �����)
        currentTime += Time.deltaTime;
        if (currentTime >= nameFadeTime)
        {
            currentTime = 0;
            nicnameBox.SetActive(false);
        }
    }
    //play��ư ������ 1ä�� 1�� �� ����
    public void OnClickJoinRoom()
    {
        CreateRoom();
    }
    //�� ����
    public void CreateRoom()
    {
        // �� �ɼ��� ����
        RoomOptions roomOptions = new RoomOptions();
        // �ִ� �ο� (0�̸� �ִ��ο�)
        roomOptions.MaxPlayers = 1;
        // �� ����Ʈ�� ������ �ʰ�? ���̰�?
        roomOptions.IsVisible = true;
        roomName = "room" + roomList.Count;
        roomList.Add(roomName);
        // �� ���� ��û (�ش� �ɼ��� �̿��ؼ�)
        //PhotonNetwork.CreateRoom(roomName, roomOptions);
        PhotonNetwork.JoinRandomOrCreateRoom(null, 2, MatchmakingMode.FillRoom, null, null, roomName, roomOptions, null);
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

    //�� ���� ��û
    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    //�� ������ �Ϸ� �Ǿ��� �� ȣ�� �Ǵ� �Լ�
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("OnJoinedRoom");
        PhotonNetwork.Instantiate(SYA_UserInfoManager.Instance.Avatar, new Vector3(-0.1f, 6.5f, 0), Quaternion.LookRotation(Camera.main.transform.position));
        SYA_UserInfoSave.Instance.Save();

        //PhotonNetwork.LoadLevel("SYA_MatchingScene");
    }

    //�� ������ ���� �Ǿ��� �� ȣ�� �Ǵ� �Լ�
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        print("OnJoinRoomFailed, " + returnCode + ", " + message);
    }

    /*//�÷��̾����� �̵��ϱ�
    public void PlaySceneChange()
    {
        SYA_SceneChange.Instance.PlayScene();
    }*/
}
