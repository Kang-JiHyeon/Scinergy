using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class SYA_SympoGoTo : MonoBehaviourPun
{
    public GameObject space;
    PlayerMove playerMove;
    PlayerRot playerRot;

    private void Update()
    {
        if (space == null)Destroy(gameObject);
        GetComponent<Collider>().enabled = SceneManager.GetActiveScene().name.Contains("Sympo");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!SceneManager.GetActiveScene().name.Contains("Symposi")) return;
        if (other.gameObject.name.Contains("Player"))
        {
            if (!other.GetComponent<PhotonView>().IsMine) return;
            /*            if (SYA_SymposiumManager.Instance.player[PhotonNetwork.NickName].IsMine)
                        {*/
            print("in");
            //�÷��̾��� �̵��� ȭ�� ������ ����
            playerMove = other.GetComponent<PlayerMove>();
            playerRot = other.GetComponent<PlayerRot>();

            playerMove.enabled = false;
            playerRot.enabled = false;

            //���� �̵� ��ư ����
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

    /*private void OnTriggerExit(Collider other)
    {
        if (!SceneManager.GetActiveScene().name.Contains("Symposi")) return;
        if (other.gameObject.name.Contains("Player"))
        {
            if (!other.GetComponent<PhotonView>().IsMine) return;
            *//*            if (SYA_SymposiumManager.Instance.player[PhotonNetwork.NickName].IsMine)
                        {*//*

            print("out");
            //���� �̵� ��ư ����
            space.SetActive(false);
            //SYA_SymposiumManager.Instance.player[PhotonNetwork.NickName].RPC("onoff", RpcTarget.All, !space.activeSelf);
            //}
        }
    }*/

    public void OffGameObject()
    {
        space.SetActive(false);
        playerMove.enabled = true;
        playerRot.enabled = true;
    }
}
