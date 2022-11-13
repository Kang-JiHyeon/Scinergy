using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class SYA_SympoPresentstion : MonoBehaviour
{
    public GameObject pre;

    private void Update()
    {
        if (pre == null) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!SceneManager.GetActiveScene().name.Contains("Symposi")) return;

        print(PhotonNetwork.NickName);
        //if (SYA_SymposiumManager.Instance.playerAuthority[PhotonNetwork.NickName].Contains("Presenter")
       if( SYA_SymposiumManager.Instance.playerAuthority[PhotonNetwork.NickName].Contains("Owner"))
        {
            if (other.gameObject.name.Contains("Player"))
            {
                if (!other.GetComponent<PhotonView>().IsMine) return;
                print("in");
                //공간 이동 버튼 띄우기
                pre.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!SceneManager.GetActiveScene().name.Contains("Symposi")) return;

        if (SYA_SymposiumManager.Instance.playerAuthority[PhotonNetwork.NickName] == "Presenter" || SYA_SymposiumManager.Instance.playerAuthority[PhotonNetwork.NickName] == "Owner")
        {
            if (other.gameObject.name.Contains("Player"))
            {
                if (!other.GetComponent<PhotonView>().IsMine) return;
                print("out");
                //공간 이동 버튼 띄우기
                pre.SetActive(false);
            }
        }
    }
}
