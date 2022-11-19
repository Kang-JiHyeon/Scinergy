using Photon.Pun;
using SYA_UserInfoManagerSaveLoad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SYA_PlayerCreatSympo : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {
        //���� �г����� ����
        //�г��� �ߺ� �˻�
        //if (!photonView.IsMine) return;
        OverlapName(PhotonNetwork.NickName.Length);
    }



    //�˻��ϴ� �Լ�
    void OverlapName(int nameNum)
    {
        int num = 0;
        //���� �г����� �ִ��� �˻�
        foreach (string name in SYA_SymposiumManager.Instance.playerName)
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
        GameObject go = PhotonNetwork.Instantiate(SYA_UserInfoManager.Instance.Avatar, new Vector3(0, 5.5f, 1), Quaternion.identity);
        PhotonNetwork.AutomaticallySyncScene = false;
        SceneManager.LoadScene("SymposiumScene");
        print("ĳ���� ����"+go.name);
    }

    /*// Update is called once per frame
    void Update()
    {
        
    }*/
}
