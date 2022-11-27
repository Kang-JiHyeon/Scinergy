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
        //원래 닉네임의 길이
        //닉네임 중복 검사
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
                    //같은 닉네임이 있는지 검사
                    foreach (string name in exul)
                    {
                        //만약 있다면
                        if (name == PhotonNetwork.NickName)
                        {
                            //번호를 붙인다
                            num++;
                            //원래 내 닉네임 + 원래 닉네임과 같은 사람의 수만큼
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
        print("캐릭터 생성" + go.name);
    }


    //검사하는 함수


    /*// Update is called once per frame
    void Update()
    {
        
    }*/
}
