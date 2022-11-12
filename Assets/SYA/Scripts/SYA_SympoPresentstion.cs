using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SYA_SympoPresentstion : MonoBehaviour
{
    public GameObject pre;
    private void OnTriggerEnter(Collider other)
    {
        print(SYA_SymposiumManager.Instance.playerAuthority[PhotonNetwork.NickName]);
        if(SYA_SymposiumManager.Instance.playerAuthority[PhotonNetwork.NickName] == "Presenter")
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
        if (SYA_SymposiumManager.Instance.playerAuthority[PhotonNetwork.NickName] == "Presenter")
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
