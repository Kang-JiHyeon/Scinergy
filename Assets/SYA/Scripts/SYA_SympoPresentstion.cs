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
                //���� �̵� ��ư ����
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
                //���� �̵� ��ư ����
                pre.SetActive(false);
            }
        }
    }
}
