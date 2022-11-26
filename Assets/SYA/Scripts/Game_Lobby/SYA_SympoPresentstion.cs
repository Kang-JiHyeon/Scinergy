using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class SYA_SympoPresentstion : MonoBehaviour
{
    public GameObject pre;
    public GameObject window;
    public GameObject windowList;

    private void Update()
    {
        if (pre == null) Destroy(gameObject);
        GetComponent<Collider>().enabled = SceneManager.GetActiveScene().name.Contains("Sympo");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!SceneManager.GetActiveScene().name.Contains("Symposi")) return;

        //if (SYA_SymposiumManager.Instance.playerAuthority[PhotonNetwork.NickName].Contains("Presenter")
       if(SYA_SymposiumManager.Instance.playerAuthority[PhotonNetwork.NickName] == "Presenter" || SYA_SymposiumManager.Instance.playerAuthority[PhotonNetwork.NickName].Contains("Owner"))
        {
            if (other.gameObject.name.Contains("Player"))
            {
                if (!other.GetComponent<PhotonView>().IsMine) return;
                //���� �̵� ��ư ����
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
                //���� �̵� ��ư ����
                pre.SetActive(false);
                windowList.SetActive(false);
                window.SetActive(false);
            }
        }
    }
}
