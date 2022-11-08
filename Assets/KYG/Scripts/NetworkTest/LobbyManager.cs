using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class LobbyManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        CreateRoom();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //�����
    public void CreateRoom()
    {
        //�� �ɼ� ����
        RoomOptions roomOptions = new RoomOptions();

        //�ִ� �ο�(0���̸� �ִ��ο�)
        roomOptions.MaxPlayers = 10;
        //�� ��Ͽ� ���̳�? ������ �ʴ���?
        roomOptions.IsVisible = true;
        //���� �����.
        PhotonNetwork.CreateRoom("star", roomOptions, TypedLobby.Default);
        
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
    //������
    
}
