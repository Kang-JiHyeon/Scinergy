using Photon.Pun;
using SYA_UI;
using SYA_UserInfoManagerSaveLoad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SYA_PlayerCreatSympo : MonoBehaviourPun
{
    public List<string> playerNameList = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        //���� �г����� ����
        //�г��� �ߺ� �˻�
        //if (!photonView.IsMine) return;
        OverlapName(PhotonNetwork.NickName.Length);
    }

    void OverlapName(int nameNum)
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            if (SYA_SympoUI.Instance != null)
                if (!SYA_SympoUI.Instance.goSympo)
                {
                    var a = PhotonNetwork.CurrentRoom.CustomProperties;
                    string[] exul = (string[])a["UserList"];
                    int num = 0;
                    //���� �г����� �ִ��� �˻�
                    foreach (string name in exul)
                    {
                        //���� �ִٸ�
                        if (name == PhotonNetwork.NickName)
                        {
                            //��ȣ�� ���δ�
                            num++;
                            //���� �� �г��� + ���� �г��Ӱ� ���� ����� ����ŭ
                            PhotonNetwork.NickName = name.Substring(0, nameNum) + $"({num})";
                        }
                    }
                    print(exul.Length);
                    string[] ul = new string[exul.Length + 1];
                    for (int i = 0; i < exul.Length; i++)
                    {
                        ul[i] = exul[i];
                    }
                    ul[exul.Length] = PhotonNetwork.NickName;
                    a["UserList"] = ul;
                    PhotonNetwork.CurrentRoom.SetCustomProperties(a);
                }
        }
        GameObject go = PhotonNetwork.Instantiate(SYA_UserInfoManager.Instance.Avatar, new Vector3(0, 5.5f, 1), Quaternion.identity);
        PhotonNetwork.AutomaticallySyncScene = false;
        //SceneManager.LoadScene("SymposiumScene");
        print("ĳ���� ����" + go.name);
    }


    //�˻��ϴ� �Լ�


    /*// Update is called once per frame
    void Update()
    {
        
    }*/
}
