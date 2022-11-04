using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using SYA_UserInfoManagerSaveLoad;

public class SYA_PlaySceneManager : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate(SYA_UserInfoManager.Instance.Avatar, new Vector3(0, 5.5f, 1), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
