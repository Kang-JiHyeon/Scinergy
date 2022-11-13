using Photon.Pun;
using SYA_UserInfoManagerSaveLoad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SYA_PlayerCreatSympo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject go = PhotonNetwork.Instantiate(SYA_UserInfoManager.Instance.Avatar, new Vector3(0, 5.5f, 1), Quaternion.identity);
        PhotonNetwork.AutomaticallySyncScene = false;
        SceneManager.LoadScene("SymposiumScene");
    }

    /*// Update is called once per frame
    void Update()
    {
        
    }*/
}
