using Photon.Pun;
using SYA_UserInfoManagerSaveLoad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SYA_PlayerCreatSympo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject go = PhotonNetwork.Instantiate(SYA_UserInfoManager.Instance.Avatar, new Vector3(0, 5.5f, 1), Quaternion.identity);
        PhotonNetwork.AutomaticallySyncScene = false;
    }

    /*// Update is called once per frame
    void Update()
    {
        
    }*/
}
