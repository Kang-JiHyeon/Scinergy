using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SYA_SympoGoTo : MonoBehaviourPun
{
    public GameObject space;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
/*            if (SYA_SymposiumManager.Instance.player[PhotonNetwork.NickName].IsMine)
            {*/
                print("in");
                //공간 이동 버튼 띄우기
                space.SetActive(true);
                //SYA_SymposiumManager.Instance.player[PhotonNetwork.NickName].RPC("onoff", RpcTarget.All, !space.activeSelf);
            //}
        }
    }
    [PunRPC]
    public void onoff(bool g)
    {
        space.SetActive(g);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
/*            if (SYA_SymposiumManager.Instance.player[PhotonNetwork.NickName].IsMine)
            {*/

                print("out");
                //공간 이동 버튼 띄우기
                space.SetActive(false);
                //SYA_SymposiumManager.Instance.player[PhotonNetwork.NickName].RPC("onoff", RpcTarget.All, !space.activeSelf);
            //}
        }
    }
}
