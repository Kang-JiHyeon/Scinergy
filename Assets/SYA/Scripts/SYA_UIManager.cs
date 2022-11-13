using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

namespace SYA_UI
{
    public class SYA_UIManager : MonoBehaviourPun
    {
        public static SYA_UIManager Instance;

        public Transform btn_chat;
        public Transform btn_mic;

        private void Awake()
        {
            //if (!photonView.IsMine) return;
            if (Instance == null)
            {
                //�ν��Ͻ��� ���� �ְ�
                Instance = this;
                //���� ���� ��ȯ�� �Ǿ �ı����� �ʰ� �ϰڴ�

                DontDestroyOnLoad(gameObject);
            }
            //�׷��� ������
            else
            {
                Destroy(gameObject);
            }
        }

        //ä��â �¿���
        public GameObject chat;
        //�۾��� ��ǲ�ʵ�
        //���� ��ư
        public void ChatOnOff()
        {
            chat.SetActive(!chat.activeSelf);
            btn_chat.GetChild(0).gameObject.SetActive(!btn_chat.GetChild(0).gameObject.activeSelf);
            btn_chat.GetChild(1).gameObject.SetActive(!btn_chat.GetChild(1).gameObject.activeSelf);
        }

        //����ũ �¿���
        public GameObject micOn;
        public GameObject micOff;
        public void MicOnOff()
        {
            micOn.SetActive(!micOn.activeSelf);
            micOff.SetActive(!micOff.activeSelf);

            SYA_SymposiumManager.Instance.playerVoice[PhotonNetwork.NickName].enabled = micOn.activeSelf;
            // ����ũ Ű��
            // ������ �г����� ����� �ҽ��� ���� �Ҵ�
            //SYA_SymposiumManager.Instance.playerVoice[PhotonNetwork.NickName].enabled = micOn.activeSelf;
            //SYA_SymposiumManager.Instance.player[PhotonNetwork.NickName].RPC("RpcMicOnOff", RpcTarget.All, PhotonNetwork.NickName, micOn.activeSelf);
            //photonView.RPC("RpcMicOnOff", RpcTarget.All, PhotonNetwork.NickName, micOn.activeSelf);
        }

        [PunRPC]
        public void RpcMicOnOff(string name, bool micOn)
        {
            SYA_SymposiumManager.Instance.playerVoice[name].enabled = micOn;
        }

        //����â �¿���
        public GameObject option;
        public Transform btn_option;
        public void OptionOnOff()
        {
            option.SetActive(!option.activeSelf);
            btn_option.GetChild(0).gameObject.SetActive(!btn_option.GetChild(0).gameObject.activeSelf);
            btn_option.GetChild(1).gameObject.SetActive(!btn_option.GetChild(1).gameObject.activeSelf);
        }

        //������ â �¿���
        public GameObject quit;
        public void QuitOnOff()
        {
            if(SceneManager.GetActiveScene().name.Contains("Sympo"))
            {
            quit.SetActive(!quit.activeSelf);
            }
            else
            {
                lobbyOn();
            }
        }

        public GameObject lobbyBack;
        public void lobbyOn()
        {
            lobbyBack.SetActive(!lobbyBack.activeSelf);
        }

        public void lobbyBackScene()
        {
            GetComponentInChildren<SYA_SympoUI>().SymposiumChsnge();
        }
    }
}
